using CenterEnglishManagement.Context;
using CenterEnglishManagement.Dto.ModelDto.OtherModelDto;
using CenterEnglishManagement.Models.OtherModels;
using CenterEnglishManagement.Models.RelateTable;
using CenterEnglishManagement.Models.UserModels;
using CenterEnglishManagement.Service.IService.IRelativeTableServices;
using Microsoft.EntityFrameworkCore;

namespace CenterEnglishManagement.Service.RelativeTableServices
{
    public class UserClassServices: CommonServices<UserClass>,IUserClassServices
    {
        private readonly ApplicationDbContext _context;
        public UserClassServices(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Class>> GetClassesByUserIdAsync(int userId)
        {
            
            return await _context.UserClasses
                .Where(uc => uc.UserId == userId)
                .Select(uc => uc.Class!)
                .ToListAsync();
        }
        public async Task<IEnumerable<UserDto>> GetUsersByClassIdAsync(int classId)
        {
            return await _context.UserClasses
                .Where(uc => uc.ClassId == classId)
                .Select(uc => new UserDto
                {
                    Id = uc.User!.Id,
                    Name = uc.User.Name,
                    Email = uc.User.Email,
                    IsActive = uc.User.IsActive,
                    Mobile = uc.User.Mobile,
                    DOB = uc.User.DOB.HasValue ? uc.User.DOB.Value.ToString("yyyy-MM-dd") : null,

                    Gender = uc.User.Gender,
                    Address = uc.User.Address
                })
                .ToListAsync();
        }
        public async Task<bool> RemoveUserFromClassAsync(int classId,int studentId)
        {

            var userClass = await _context.UserClasses
            .FirstOrDefaultAsync(uc => uc.UserId == studentId && uc.ClassId == classId);

            if (userClass == null)
            {
                return false; // or throw an exception if preferred
            }

            _context.UserClasses.Remove(userClass);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
