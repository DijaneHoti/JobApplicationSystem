using JobApplicationSystem.Models.Dto;

namespace JobApplicationSystem.Data.Services
{
    public interface IHiringManagerService
    {
        Task<List<GetHiringManagerDTO>> GetManagersByCompanyId(int compId);
    }
}
