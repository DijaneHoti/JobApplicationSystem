using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace JobApplicationSystem.Models.Dto
{
    public class Job
    {
        [Key]
        public int JobID { get; set; }

        [Required]
        public string PositionName { get; set; }


        [Required]
        public string Description { get; set; }


        public int CompanyID { get; set; }
        [ForeignKey("CompanyID")]

        public Company Company { get; set; }
        public List<JobPosting> JobPosts { get; set; }
    }
}
