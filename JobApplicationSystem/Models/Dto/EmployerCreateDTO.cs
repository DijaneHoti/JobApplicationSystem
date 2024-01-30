using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace JobApplicationSystem.Models.Dto
{
    public class EmployerCreateDTO
    {
        
        public string Name { get; set; }

        
        public string Field { get; set; }

        
        public string Phone { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }

        
        public string Address { get; set; }

        public DateTime InsertedDate { get; set; }

        
        public int CompanyID { get; set; }

        public void Validate()
        {
           
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentException("Name is required", nameof(Name));
            if (string.IsNullOrWhiteSpace(Field))
                throw new ArgumentException("Position is required", nameof(Field));
            if (string.IsNullOrWhiteSpace(Phone))
                throw new ArgumentException("Position is required", nameof(Phone));
            if (string.IsNullOrWhiteSpace(Email))
                throw new ArgumentException("Position is required", nameof(Email));
            if (string.IsNullOrWhiteSpace(Password))
                throw new ArgumentException("Position is required", nameof(Field));
            var passwordPattern = @"^(?=.*[A-Z])(?=.*\d).+$";
            if (!Regex.IsMatch(Password, passwordPattern))
                throw new ArgumentException("Password must contain at least one uppercase letter and at least one number", nameof(Password));
            
            if (InsertedDate > DateTime.Now)
                throw new ArgumentException("Inserted date cannot be in the future", nameof(InsertedDate));
        }
    }
}
