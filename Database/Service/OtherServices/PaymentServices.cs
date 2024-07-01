using CenterEnglishManagement.Context;
using CenterEnglishManagement.Models.OtherModels;
using CenterEnglishManagement.Service.IService.IOtherServices;

namespace CenterEnglishManagement.Service.OtherServices
{
    public class PaymentServices:CommonServices<Payment>,IPaymentServices
    {
        private readonly ApplicationDbContext _context;
        public PaymentServices(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        public int CalculateTotalPayments()
        {
            return _context.Payments.Sum(p => p.Amount);
        }

        // Calculate total payment amount for a specific student by their Id
        public int CalculateTotalPaymentsForStudent(int studentId)
        {
            return _context.Payments.Where(p => p.StudentId == studentId).Sum(p => p.Amount);
        }
    }
}
