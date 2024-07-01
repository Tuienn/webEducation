namespace CenterEnglishManagement.Dto.ModelDto.OtherModelDto
{
    public class ClassDto
    {
        public bool IsActive { get; set; }
        public string? UrlImg {  get; set; }
        public string Grade { get; set; }
        public int Year { get; set; }
        public string ClassName { get; set; }
        public int TuitionFeeAmount { get; set; }
        public int TeacherId { get; set; }
        public ScheduleDto ScheduleDto { get; set; }
    
    }
}
