using AutoMapper;
using CenterEnglishManagement.Dto.ModelDto.OtherModelDto;
using CenterEnglishManagement.Models.OtherModels;
using CenterEnglishManagement.Models.UserModels;
using Microsoft.AspNetCore.Components.Forms;

namespace CenterEnglishManagement.Extentions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.DOB, opt => opt.MapFrom(src => src.DOB.HasValue ? src.DOB.Value.ToString("yyyy-MM-dd") : null));
            CreateMap<UserDto, User>()
            .ForMember(dest => dest.DOB, opt => opt.MapFrom(src => ConvertDateFormat(src.DOB)));


            CreateMap<StudentAttendance, StudentAttendanceDto>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd")));
            CreateMap<StudentAttendanceDto, StudentAttendance>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => ConvertDateFormatNotNull(src.Date)));

            CreateMap<Payment, PaymentDto>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("yyyy-MM-dd")));
            CreateMap<PaymentDto, Payment>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => ConvertDateFormatNotNull(src.Date)));
        }
        private static DateTime? ConvertDateFormat(string inputDate)
        {
            if (string.IsNullOrEmpty(inputDate))
                return null;

            // Parse the input date string to a DateTime object
            DateTime date = DateTime.SpecifyKind(DateTime.ParseExact(inputDate, "yyyy-MM-dd", null), DateTimeKind.Utc);

            // Return the DateTime object
            return date;
        }
        private static DateTime ConvertDateFormatNotNull(string inputDate)
        {
            // Parse the input date string to a DateTime object
            DateTime date = DateTime.SpecifyKind(DateTime.ParseExact(inputDate, "yyyy-MM-dd", null), DateTimeKind.Utc);

            // Return the DateTime object
            return date;
        }
    }
}
