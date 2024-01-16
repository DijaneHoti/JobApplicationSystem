using System.ComponentModel.DataAnnotations;

namespace JobApplicationSystem.Models.Dto
{
    public class CompanyUpdateDTO
    {
        [Required]
        public string CompanyName { get; set; }

        [Required]
        public List<HiringManager> hiringManager { get; set; }
    }
}
