using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplicationSystem.Models
{
    public class Jobseeker
    {
        [Key]
        public int JobseekerID { get; set; }
        [Required]
        public string JobseekerName { get; set; }

        [Required]
        public string JobseekerEmail { get; set; }

        

        public int JobPostingID { get; set; }
        [ForeignKey("JobPostingID")]

        public JobPosting JobPosting { get; set; }  
    }
}
