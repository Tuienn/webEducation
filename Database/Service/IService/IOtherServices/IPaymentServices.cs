using CenterEnglishManagement.Models.OtherModels;

namespace CenterEnglishManagement.Service.IService.IOtherServices
{
    public interface IPaymentServices:ICommonServices<Payment>
    {
        public int CalculateTotalPayments();
        public int CalculateTotalPaymentsForStudent(int studentId);
    }
}
