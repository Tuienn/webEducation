namespace CenterEnglishManagement.Dto.ModelDto
{
    public class MonthlyStudentStatisticDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int StudentCount { get; set; }
    }
    public class QuarterlyStudentStatisticDto
    {
        public int Year { get; set; }
        public int Quarter { get; set; }
        public int StudentCount { get; set; }
    }

    public class YearlyStudentStatisticDto
    {
        public int Year { get; set; }
        public int StudentCount { get; set; }
    }
}
