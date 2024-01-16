using System.ComponentModel.DataAnnotations;

namespace JobApplicationSystem.Models.Dto
{
    public class HiringManagerUpdateDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Specializimi { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
