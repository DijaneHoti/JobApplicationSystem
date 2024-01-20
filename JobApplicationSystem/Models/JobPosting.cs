using JobApplicationSystem.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplicationSystem.Models
{
    public class JobPosting
    {
        [Key]
        public int JobPostingID { get; set; }
        [Required]
        public string JobPostingTitle { get; set; }
        [Required]
        public string JobPostingDescription { get; set; }
        
        //public int EmployerID { get; set; }
        //[ForeignKey("EmployerID")]

        //public Employer Employer { get; set; }

        public int JobID { get; set; }
        [ForeignKey("JobID")]

        public  Job Job { get; set; }

        public List<JobPostSeekers> JobPostSeekers { get; set; }

    }
}
