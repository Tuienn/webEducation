using CenterEnglishManagement.Context;
using CenterEnglishManagement.Models.OtherModels;
using CenterEnglishManagement.Service.IService.IOtherServices;

namespace CenterEnglishManagement.Service.OtherServices
{
    public class TuitionFeeServices:CommonServices<TuitionFee>,ITuitionFeeServices
    {
        private readonly ApplicationDbContext _context;
        public TuitionFeeServices(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        public int GetTuitionFeeAmountByClassId(int classId)
        {
        var tuitionFee = _context.TuitionFees
            .FirstOrDefault(tf => tf.ClassId == classId);

        return tuitionFee.Amount;
        }
        public decimal CalculateTotalTuitionFees()
        {
            var totalFees = (from attendance in _context.StudentAttendances
                             where attendance.IsPresent
                             join userClass in _context.UserClasses on attendance.UserId equals userClass.UserId
                             join tuitionFee in _context.TuitionFees on userClass.ClassId equals tuitionFee.ClassId
                             select tuitionFee.Amount).Sum();

            return totalFees;
        }
        public decimal CalculateTotalTuitionFeeForUser(int userId)
        {
            var totalFee = (from attendance in _context.StudentAttendances
                            where attendance.IsPresent && attendance.UserId == userId
                            join userClass in _context.UserClasses on attendance.UserId equals userClass.UserId
                            join tuitionFee in _context.TuitionFees on userClass.ClassId equals tuitionFee.ClassId
                            select tuitionFee.Amount).Sum();

            return totalFee;
        }
    }
}
