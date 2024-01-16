using System.ComponentModel.DataAnnotations;

namespace JobApplicationSystem.Models.Dto
{
    public class JobCreateDTO
    {
        [Required]
        public int JobID { get; set; }

        [Required]
        public string PositionName { get; set; }


        [Required]
        public string Description { get; set; }

    }
}
