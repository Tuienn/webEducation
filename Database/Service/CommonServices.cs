using CenterEnglishManagement.Context;
using CenterEnglishManagement.Models.UserModels;
using CenterEnglishManagement.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace CenterEnglishManagement.Service
{
    public class CommonServices<T>: ICommonServices<T> where T: class
    {
        private readonly ApplicationDbContext _context;
        public CommonServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T> CreateAsync(T entity)
        {
            try
            {
                var emailProperty = typeof(T).GetProperty("Email");

                // Nếu đối tượng có thuộc tính Email
                if (emailProperty != null)
                {
                    var emailValue = emailProperty.GetValue(entity)?.ToString();

                    // Kiểm tra giá trị Email
                    if (string.IsNullOrWhiteSpace(emailValue))
                    {
                        throw new ArgumentException("Email property cannot be null or empty.");
                    }

                    // Kiểm tra trùng email trong các bảng khác nhau
                    var isDuplicate = await _context.Set<User>().AnyAsync(a => a.Email == emailValue);

                    if (isDuplicate)
                    {
                        throw new InvalidOperationException("Email already exists.");
                    }
                }
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var entity = _context.Set<T>().Find(id);
                if (entity != null)
                {
                    _context.Set<T>().Remove(entity);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(int pageIndex, int pageSize, string sortBy, bool sortDesc)
        {
            try
            {
                IQueryable<T> query = _context.Set<T>();

                if (!string.IsNullOrEmpty(sortBy))
                {
                    query = sortDesc ? query.OrderByDescending(e => EF.Property<object>(e, sortBy))
                                     : query.OrderBy(e => EF.Property<object>(e, sortBy));
                }

                return await query.Skip((pageIndex - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task<T> GetbyIdAsync(int id)
        {
            try
            {
                T entity = await _context.Set<T>().FindAsync(id);
                if (entity == null)
                {
                    return null;
                }
                return entity;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task UpdateAsync(int id, T entity)
        {

            try
            {
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}

