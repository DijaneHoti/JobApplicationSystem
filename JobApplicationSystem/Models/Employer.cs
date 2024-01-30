using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobApplicationSystem.Models
{
    public class Employer
    {
        [Key]
        public int EmployerID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Field { get; set; }
        
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
       
        [Required]
        public string Address { get; set; }

        public DateTime InsertedDate { get; set; }
        public int CompanyID { get; set; }
        [ForeignKey("CompanyID")]

        public Company Company { get; set; }

    }
}
