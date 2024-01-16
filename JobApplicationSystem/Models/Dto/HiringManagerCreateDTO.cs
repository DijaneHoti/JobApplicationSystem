using System.ComponentModel.DataAnnotations;

namespace JobApplicationSystem.Models.Dto
{
    public class HiringManagerCreateDTO
    {
        [Required]
        public int ManagerID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Specializimi { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
