using System.ComponentModel.DataAnnotations;

namespace JobApplicationSystem.Models.Dto
{
    public class HiringManagerUpdateDTO
    {
        [Required]
        //public int ManagerID { get; set;}
        //[Required]
        public string Name { get; set; }
        [Required]
        public string Specialization { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int CompanyID { get; set; }

    }
}
