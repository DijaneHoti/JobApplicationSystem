using JobApplicationSystem.Models;
using JobApplicationSystem.UnitofWork;

namespace JobApplicationSystem.Repository
{
    public class HiringManagerRepository : RepositoryBase<HiringManager>
    {
        public HiringManagerRepository(IUnitOfwork unitOfwork) : base(unitOfwork)
        {
        }
    }
}
