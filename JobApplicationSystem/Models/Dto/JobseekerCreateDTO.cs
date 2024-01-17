using System.ComponentModel.DataAnnotations;

namespace JobApplicationSystem.Models.Dto
{
    public class JobseekerCreateDTO
    {
        [Required]
        public string JobseekerName { get; set; }

        [Required]
        public string JobseekerEmail { get; set; }
        [Required]
        public int JobPostingID { get; set; }


    }
}
