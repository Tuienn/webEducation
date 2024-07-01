using CenterEnglishManagement.Context;
using CenterEnglishManagement.Models.OtherModels;
using CenterEnglishManagement.Service.IService.IOtherServices;

namespace CenterEnglishManagement.Service.OtherServices
{
    public class SalaryServices:CommonServices<Salary>,ISalaryServices
    {
        private readonly ApplicationDbContext _context;
        public SalaryServices(ApplicationDbContext context): base(context)
        {
            _context = context;
        }
    }
}
