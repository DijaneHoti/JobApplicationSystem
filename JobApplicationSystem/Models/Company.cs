using System.ComponentModel.DataAnnotations;

namespace JobApplicationSystem.Models
{
    public class Company
    {
        [Key]
        public int CompanyID { get; set; }
        [Required]
        public string CompanyName { get; set;}

        [Required]
        public List<HiringManager> hiringManager{ get; set; }
    }
}
