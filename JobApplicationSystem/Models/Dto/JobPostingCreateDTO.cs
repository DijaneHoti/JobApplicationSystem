using System.ComponentModel.DataAnnotations;

namespace JobApplicationSystem.Models.Dto
{
    public class JobPostingCreateDTO
    {
        [Required]
        public string JobPostingTitle { get; set; }
        [Required]
        public string JobPostingDescription { get; set; }
        [Required]
        public int JobID { get; set; }

    }
}
