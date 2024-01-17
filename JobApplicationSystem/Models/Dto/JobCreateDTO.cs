using System.ComponentModel.DataAnnotations;

namespace JobApplicationSystem.Models.Dto
{
    public class JobCreateDTO
    {
        [Required]
        public string PositionName { get; set; }


        [Required]
        public string Description { get; set; }

        [Required]
        public int CompanyID { get; set; }

    }
}
