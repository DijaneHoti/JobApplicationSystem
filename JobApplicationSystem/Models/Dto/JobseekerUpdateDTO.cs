using System.ComponentModel.DataAnnotations;

namespace JobApplicationSystem.Models.Dto
{
    public class JobseekerUpdateDTO
    {
        //[Required]
        //public int JobseekerID { get; set; }
        [Required]
        public string JobseekerName { get; set; }

        [Required]
        public string JobseekerEmail { get; set; }

        [Required]
        public int JobPostingID { get; set; }
    }
}
