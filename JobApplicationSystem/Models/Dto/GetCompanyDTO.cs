using System.ComponentModel.DataAnnotations;

namespace JobApplicationSystem.Models.Dto
{
    public class GetCompanyDTO
    {
        public int Id { get; set; }
     
        public string CompanyName { get; set; }

    }
}
