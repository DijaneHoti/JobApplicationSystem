using JobApplicationSystem.Models;
using JobApplicationSystem.UnitofWork;

namespace JobApplicationSystem.Repository
{
    public class JobPostingRepository : RepositoryBase<JobPosting>
    {
        public JobPostingRepository(IUnitOfwork unitOfwork) : base(unitOfwork)
        {
        }
    }
}
