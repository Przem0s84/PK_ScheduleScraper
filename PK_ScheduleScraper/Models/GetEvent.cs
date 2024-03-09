using System.ComponentModel.DataAnnotations;

namespace PK_ScheduleScraper.Models
{
    public class GetEvent
    {
        [Required]
        public string TeamName { get; set; }

        [Required]
        [RegularExpression(@"^L0[0-9]$",ErrorMessage ="Invalid pattern ex.L01 L02 L03")]
        public string LabGropName { get; set; }

        [Required]
        [RegularExpression(@"^P0[0-9]$", ErrorMessage = "Invalid pattern ex.P01 P02 P03")]
        public string ProjGroupName { get; set; }

        [Required]
        [RegularExpression(@"^K0[0-9]$", ErrorMessage ="Invalid pattern ex.K01 K02 K03")]
        public string CompLabGroupName { get; set; }

        [Required]
        [RegularExpression(@"^[NP]$", ErrorMessage = "Invalid pattern ex. N or P")]
        public string WeekType { get; set; }
    }
}
