using System.ComponentModel.DataAnnotations;

namespace JobApplicationSystem.Models.Dto
{
    public class EmployerCreateDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Field { get; set; }

        [Required]
        public int Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        public string Address { get; set; }

    }
}
