using JobApplicationSystem.Models;
using JobApplicationSystem.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationSystem.Data.Services
{
    public class HiringManagerService : IHiringManagerService
    {
        private readonly ApplicationDbContext _context;
        public HiringManagerService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<GetHiringManagerDTO>> GetManagersByCompanyId(int compId)
        {
            var managers = await _context.HiringManagers.Where(x => x.CompanyID == compId).Include(x=>x.Company).ToListAsync();

            List<GetHiringManagerDTO> list = new List<GetHiringManagerDTO>();

            list = managers.Select(x => new GetHiringManagerDTO
            {
                ManagerID = x.ManagerID,
                Name = x.Name,
                Specialization = x.Specialization,
                Email = x.Email,
                Company = x.Company.CompanyName

            }).ToList();
            return list;
        }
    }
}
