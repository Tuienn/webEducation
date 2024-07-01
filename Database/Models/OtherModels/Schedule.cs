using Microsoft.AspNetCore.Routing.Constraints;
using System.ComponentModel.DataAnnotations;

namespace CenterEnglishManagement.Models.OtherModels
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        public bool DateType { get; set; }
        // true: monday,wednesday,friday
        //false: tueday,thursday,Saturdayc
        public int Shift {  get; set; }
        //Shift: 1:7h,2:14h,3: 18h
        public DateTime StartTime { get; set; }
        public int NumOfSession {  get; set; }
        //NumOfSession=36/72
        public int ClassId {  get; set; }
        public Class? Class { get; set; }

    }
}
