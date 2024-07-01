using System.ComponentModel.DataAnnotations;

namespace CenterEnglishManagement.Models.OtherModels
{
    public class TuitionFee
    {
        [Key]
        public int Id { get; set; }
        public int Amount { get; set; }//monthly 200.000,300.000,400.000
        public int ClassId { get; set; }
        public Class? Class { get; set; }

    }
}
