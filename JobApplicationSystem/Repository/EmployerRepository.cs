using JobApplicationSystem.Models;
using JobApplicationSystem.UnitofWork;

namespace JobApplicationSystem.Repository
{
    public class EmployerRepository : RepositoryBase<Employer>
    {
        public EmployerRepository(IUnitOfwork unitOfwork) : base(unitOfwork)
        {
        }
    }
}
