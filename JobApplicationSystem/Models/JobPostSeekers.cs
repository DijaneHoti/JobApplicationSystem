using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplicationSystem.Models
{
    public class JobPostSeekers
    {
        public int Id { get; set; }
        [ForeignKey("JobSeekerID")]
        public int JobSeekerID { get; set; }
        public Jobseeker Jobseeker { get; set; }

        [ForeignKey("JobPostingID")]
        public int JobPostingID { get; set; }
        public JobPosting JobPosting { get; set; }
    }
}
