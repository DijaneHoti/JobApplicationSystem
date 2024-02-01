using Azure;
using JobApplicationSystem.Data;
using JobApplicationSystem.Data.Services;
using JobApplicationSystem.Models;
using JobApplicationSystem.Models.Dto;
using JobApplicationSystem.Repository;
using JobApplicationSystem.UnitofWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Net;

namespace JobApplicationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HiringManagerController : ControllerBase
    {
        private readonly IHiringManagerService _managerService;
        private ApiResponse _response;
        private readonly IUnitOfwork _unitOfWork;
        IRepository<HiringManager> hiringManagerRepository;
        private readonly ApplicationDbContext _context;

        public HiringManagerController(IUnitOfwork unitOfWork, ApplicationDbContext context, IHiringManagerService managerService)
        {
            _unitOfWork = unitOfWork;
            hiringManagerRepository = new HiringManagerRepository(_unitOfWork);
            _context = context;
            _managerService = managerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HiringManager>>> GetHiringManagers()
        {
            var actionResult = await hiringManagerRepository.Get();
            List<HiringManager> hiringManagers = new List<HiringManager>();
            if (actionResult.Result is OkObjectResult okResult)
            {
                hiringManagers = okResult.Value as List<HiringManager>;
                // Now you can work with your companies list.
            }
            var hiringManagerDto = hiringManagers.Select(c => new GetHiringManagerDTO
            {
                ManagerID = c.ManagerID,
                Name = c.Name,
                Specialization = c.Specialization,
                Email = c.Email
            });

            return Ok(hiringManagerDto);
        }




        [HttpPost]
        public async Task<ActionResult<HiringManager>> CreateHiringManger(HiringManagerCreateDTO hiringManagerDto)
        {
            

            hiringManagerDto.Validate();

            HiringManager hiringManager = new HiringManager()
            {
               Name = hiringManagerDto.Name,
               Specialization = hiringManagerDto.Specialization,
               Email = hiringManagerDto.Email,
               CompanyID = hiringManagerDto.CompanyID,

            };
            var hiringManagers = await hiringManagerRepository.Create(hiringManager);
            return hiringManagers;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHiringManager(int id, HiringManagerUpdateDTO hiringManagerDto)
        {

            HiringManager hiringManager = new HiringManager()
            {
                ManagerID = id,
                Name = hiringManagerDto.Name,
                Specialization = hiringManagerDto.Specialization,
                Email= hiringManagerDto.Email,
                CompanyID= hiringManagerDto.CompanyID

            };
            var hiringManagers = await hiringManagerRepository.Update(id, hiringManager);
            return hiringManagers;
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHiringManager(int id)
        {
            var hiringManagers = await hiringManagerRepository.Delete(id);
            return hiringManagers;
        }


        [HttpGet("GetHiringManagersBySpecialization")]
        public async Task<ActionResult<IEnumerable<HiringManager>>> GetHiringManagersBySpecialization([FromQuery] string specialization)
        {
            if (string.IsNullOrEmpty(specialization))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            var hiringManagers = await hiringManagerRepository.GetBySpecialization(specialization);
            List<HiringManager> managers = new List<HiringManager>();

            if (hiringManagers.Result is OkObjectResult okResult)
            {
                managers = okResult.Value as List<HiringManager>;

            }

            var managerDto = managers.Select(c => new GetHiringManagerDTO
            {
             ManagerID = c.ManagerID,
             Name = c.Name,
             Specialization = c.Specialization,
             Email = c.Email,
             Company = c.Company.CompanyName,

            });

            return Ok(managerDto);
        }

        [HttpGet("GetHiringManagersByCompanyId")]
        public async Task<IActionResult> GetHiringManagersByCompanyId([FromQuery] int companyId)
        {
            if(companyId <= 0)
            {
                return BadRequest("CompanyID is required!");
            }

            return Ok(await _managerService.GetManagersByCompanyId(companyId));
            
        }

        


    }
}
