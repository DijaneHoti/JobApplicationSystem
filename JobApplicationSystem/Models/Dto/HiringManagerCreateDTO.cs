using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace JobApplicationSystem.Models.Dto
{
    public class HiringManagerCreateDTO
    {
       
        
        public string Name { get; set; }
        
        public string Specialization { get; set; }
        
        public string Email { get; set; }
        
        public int CompanyID { get; set; }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentException("Name is required", nameof(Name));
            if (string.IsNullOrWhiteSpace(Specialization))
                throw new ArgumentException("Specialization is required", nameof(Specialization));
            if (string.IsNullOrWhiteSpace(Email))
                throw new ArgumentException("Email is required", nameof(Email));
            int atCount = Email.Count(c => c == '@');
            if (atCount != 1)
                throw new ArgumentException("Email must contain exactly one '@' symbol", nameof(Email));
            if (CompanyID <= 0)
                throw new ArgumentException("Company is required", nameof(CompanyID));

        }
    }
}
