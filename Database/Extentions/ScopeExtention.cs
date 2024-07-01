using CenterEnglishManagement.Service;
using CenterEnglishManagement.Service.IService;
using CenterEnglishManagement.Service.IService.IOtherServices;
using CenterEnglishManagement.Service.IService.IRelativeTableServices;
using CenterEnglishManagement.Service.IService.IUserServices;
using CenterEnglishManagement.Service.OtherServices;
using CenterEnglishManagement.Service.RelativeTableServices;

namespace CenterEnglishManagement.Extentions
{
    public static class ScopeExtention
    {
        public static void AddScopeService(this IServiceCollection services) {
            /*services.AddScoped<Interface,Service>*/
            //user scope
            services.AddScoped(typeof(ICommonServices<>), typeof(CommonServices<>));
            services.AddScoped<IUserService, UserServices>();
            //other scope
            services.AddScoped<IClassServices, ClassServices>();
            services.AddScoped<IPaymentServices, PaymentServices>();
            services.AddScoped<ISalaryServices, SalaryServices>();
            services.AddScoped<IScheduleServices, ScheduleServices>();
            services.AddScoped<IStudentAttendanceServices, StudentAttendanceServices>();
            services.AddScoped<ITuitionFeeServices, TuitionFeeServices>();
            //ralativve Scope
            services.AddScoped<IUserClassServices, UserClassServices>();
            services.AddScoped<IStudentParentServices, StudentParentServices>();
        }
    }
}
