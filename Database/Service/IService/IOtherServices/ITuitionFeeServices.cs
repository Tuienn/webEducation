using CenterEnglishManagement.Models.OtherModels;

namespace CenterEnglishManagement.Service.IService.IOtherServices
{
    public interface ITuitionFeeServices:ICommonServices<TuitionFee>
    {
        int GetTuitionFeeAmountByClassId(int classId);
        public decimal CalculateTotalTuitionFees();
        public decimal CalculateTotalTuitionFeeForUser(int userId);
    }
}
