using CenterEnglishManagement.Context;
using CenterEnglishManagement.Dto.ModelDto.OtherModelDto;
using CenterEnglishManagement.Extentions;
using CenterEnglishManagement.Models.OtherModels;
using CenterEnglishManagement.Models.UserModels;
using CenterEnglishManagement.Service.IService;
using CenterEnglishManagement.Service.IService.IUserServices;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace CenterEnglishManagement.Service
{
    public class UserServices : CommonServices<User>, IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserServices(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllStudentAsync(int pageIndex, int pageSize, string sortBy, bool sortDesc)
        {
            try
            {

                IQueryable<User> query = _context.Users.Where(u => u.Role == UserRole.Student);

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
        public async Task<IEnumerable<User>> GetAllByRoleAsync(int pageIndex, int pageSize, string sortBy, bool sortDesc, UserRole role)
        {
            try
            {

                IQueryable<User> query = _context.Users.Where(u => u.Role == role);

                if (!string.IsNullOrEmpty(sortBy))
                {
                    query = sortDesc ? query.OrderByDescending(e => EF.Property<object>(e, sortBy))
                                     : query.OrderBy(e => EF.Property<object>(e, sortBy));
                }

                var users = await query.Skip((pageIndex - 1) * pageSize)
                                  .Take(pageSize)
                                  .ToListAsync();
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
        public async Task<User> ValidateUser(string email, string password)
        {
            if (email == null|| password==null)
            {
                return null;
            }
            User user = await _context.Users.FirstOrDefaultAsync(e => e.Email == email&&e.Password==password);
            if (user == null)
            {
                return null;
            }

            return user;
        }
        public List<int> PaymentandTuitionTotal()
        {
            int paymentTotal = CalculateTotalPayments();
            int tuitionTotal = CalculateTotalTuitionFees();
            return new List<int> { paymentTotal, tuitionTotal };
        }
        public async Task<IEnumerable<Class>> GetTeacherClassesAsync(int teacherId)
        {

            var classes = await _context.Classes
                            .Where(c => c.TeacherId == teacherId)
                            .ToListAsync();


                return classes;
            
        }


        /// <summary>
        /// sub method
        /// </summary>
        /// <returns></returns>
        public int CalculateTotalPayments()
        {
            return _context.Payments.Sum(p => p.Amount);
        }
        public int CalculateTotalTuitionFees()
        {
            var totalFees = (from attendance in _context.StudentAttendances
                             where attendance.IsPresent
                             join userClass in _context.UserClasses on attendance.UserId equals userClass.UserId
                             join tuitionFee in _context.TuitionFees on userClass.ClassId equals tuitionFee.ClassId
                             select tuitionFee.Amount).Sum();

            return totalFees;
        }
        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
