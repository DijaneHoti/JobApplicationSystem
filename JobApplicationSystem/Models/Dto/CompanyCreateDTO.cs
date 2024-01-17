using System.ComponentModel.DataAnnotations;

namespace JobApplicationSystem.Models.Dto
{
    public class CompanyCreateDTO
    {
      
        [Required]
        public string CompanyName { get; set; }

        [Required]
        public List<HiringManager> hiringManager { get; set; }
    }
}
