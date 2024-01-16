using System.ComponentModel.DataAnnotations;

namespace JobApplicationSystem.Models
{
    public class HiringManager
    {
        [Key]
        public int ManagerID { get; set; }
        public string Name { get; set; }
        public string Specializimi { get; set; }
        public string Email { get; set; }
    }
}
