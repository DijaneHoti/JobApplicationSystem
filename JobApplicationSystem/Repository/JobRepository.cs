using JobApplicationSystem.Models;
using JobApplicationSystem.Models.Dto;
using JobApplicationSystem.UnitofWork;

namespace JobApplicationSystem.Repository
{
    public class JobRepository : RepositoryBase<Job>
    {
        public JobRepository(IUnitOfwork unitOfwork) : base(unitOfwork)
        {
        }
    }
}
