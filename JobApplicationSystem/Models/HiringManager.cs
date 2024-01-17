using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplicationSystem.Models
{
    public class HiringManager
    {
        [Key]
        public int ManagerID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Specialization { get; set; }
        [Required]
        public string Email { get; set; }

       
        public int CompanyID { get; set; }
        [ForeignKey("CompanyID")]
        public Company Company { get; set; }

    }
}
