using CenterEnglishManagement.Context;
using CenterEnglishManagement.Dto.ModelDto.OtherModelDto;
using CenterEnglishManagement.Models.RelateTable;
using CenterEnglishManagement.Models.UserModels;
using CenterEnglishManagement.Service.IService;
using CenterEnglishManagement.Service.IService.IOtherServices;
using CenterEnglishManagement.Service.IService.IRelativeTableServices;
using Microsoft.EntityFrameworkCore;

namespace CenterEnglishManagement.Service.RelativeTableServices
{
    public class StudentParentServices : CommonServices<StudentParent>,IStudentParentServices
    {
        private readonly ApplicationDbContext _context;
        public StudentParentServices(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        public async  Task<IEnumerable<User>> GetStudentsByParentIdAsync(int parentId)
        {
            
                var students= await  _context.StudentParents
                              .Where(sp => sp.ParentId == parentId)
                              .Select(sp => sp.Student)
                              .ToListAsync();

            return students;
        }
        public async Task<User> GetParentByStudentIdAsync(int studentId)
        { 
            var parent= await _context.StudentParents.Where(sp=>sp.StudentId==studentId).Select(sp=>sp.Parent).FirstOrDefaultAsync();
            return parent;
        }
        public async Task<IEnumerable<TuitionPaymentDto>> GetPaymentInforByParentIdAsync(int parentId)
        {
            var students = await GetStudentsByParentIdAsync(parentId);
            var paymentInforByChildrenTasks = students.Select(async sp => new TuitionPaymentDto
            {
                Name = sp.Name,
                StudentId = sp.Id,
                Total =  CalculateTotalTuitionFeeForUserAsync(sp.Id),
                Paid =  CalculateTotalPaymentsForStudentAsync(sp.Id)
            });

            var paymentInforByChildren = await Task.WhenAll(paymentInforByChildrenTasks);
            return paymentInforByChildren;
        }


        public  int CalculateTotalTuitionFeeForUserAsync(int userId)
        {
            var totalFee =  (from attendance in _context.StudentAttendances
                            where attendance.IsPresent && attendance.UserId == userId
                            join userClass in _context.UserClasses on attendance.UserId equals userClass.UserId
                            join tuitionFee in _context.TuitionFees on userClass.ClassId equals tuitionFee.ClassId
                            select tuitionFee.Amount).Sum();

            return totalFee;
        }
        public int CalculateTotalPaymentsForStudentAsync(int studentId)
        {
            return  _context.Payments.Where(p => p.StudentId == studentId).Sum(p => p.Amount);
        }
    }

}
