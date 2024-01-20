using JobApplicationSystem.Models;
using JobApplicationSystem.UnitofWork;

namespace JobApplicationSystem.Repository
{
    public class JobseekerRepository : RepositoryBase<Jobseeker>
    {
        public JobseekerRepository(IUnitOfwork unitOfwork) : base(unitOfwork)
        {
        }
    }
}
