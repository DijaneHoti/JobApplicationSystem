using JobApplicationSystem.Models;
using JobApplicationSystem.Models.Dto;
using JobApplicationSystem.UnitofWork;

namespace JobApplicationSystem.Repository
{
    public class CompanyRepository : RepositoryBase<Company>
    {
        public CompanyRepository(IUnitOfwork unitOfwork) : base(unitOfwork)
        {
        }
    }
}
