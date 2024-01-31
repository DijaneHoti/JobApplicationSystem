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

        //private readonly ApplicationDbContext _db;
        //private ApiResponse _response;

        //public HiringManagerController(ApplicationDbContext db)
        //{
        //    _db = db;
        //    _response = new ApiResponse();
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetHiringManagers()
        //{
        //    _response.Result = _db.HiringManagers.ToList();
        //    _response.StatusCode = HttpStatusCode.OK;
        //    return Ok(_response);

        //}

        ////[HttpGet("{id:int}")]
        ////public async Task<IActionResult> GetHiringManager(int? id)
        ////{
        ////    if (id == 0)
        ////    {
        ////        _response.StatusCode = HttpStatusCode.BadRequest;
        ////        _response.IsSuccess = false;
        ////        return BadRequest(_response);
        ////    }

        ////    HiringManager hiringManager = _db.HiringManagers.FirstOrDefault(u => u.ManagerID == id);

        ////    if (hiringManager == null)
        ////    {
        ////        _response.StatusCode = HttpStatusCode.NotFound;
        ////        _response.IsSuccess = false;
        ////        return NotFound(_response);
        ////    }

        ////    _response.Result = hiringManager;
        ////    _response.StatusCode = HttpStatusCode.OK;
        ////    return Ok(_response);
        ////}


        //[HttpGet("{id:int}")]
        //public async Task<IActionResult> GetHiringManager(int id)
        //{
        //    if (id == 0)
        //    {
        //        // _response.StatusCode = HttpStatusCode.BadRequest;
        //        // _response.IsSuccess = false;
        //        // return BadRequest(_response);
        //        return BadRequest();
        //    }

        //    var hiringManager = await _db.HiringManagers.Include(x => x.Company).Where(u => u.ManagerID == id).FirstOrDefaultAsync();

        //    if (hiringManager == null)
        //    {
        //        // _response.StatusCode = HttpStatusCode.NotFound;
        //        // _response.IsSuccess = false;
        //        // return NotFound(_response);
        //        return NotFound();
        //    }

        //    var result = new GetHiringManagerDTO() { 
        //        ManagerID = hiringManager.ManagerID,
        //        Name = hiringManager.Name,
        //        Specialization = hiringManager.Specialization,
        //        Email = hiringManager.Email,
        //        Company = hiringManager.Company.CompanyName
        //    };
        //    return Ok(result);
        //}
















        ////CREATE

        //[HttpPost]

        //public async Task<ActionResult<ApiResponse>> CreateHiringManager([FromForm] HiringManagerCreateDTO hiringManagerCreateDTO)
        //{
        //    try
        //    {


        //        HiringManager hiringManager = new()
        //        {
        //            CompanyID = hiringManagerCreateDTO.CompanyID,
        //            Name = hiringManagerCreateDTO.Name,
        //            Specialization = hiringManagerCreateDTO.Specialization,
        //            Email = hiringManagerCreateDTO.Email,

        //        };

        //        if (ModelState.IsValid)
        //        {
        //            _db.HiringManagers.Add(hiringManager);
        //            _db.SaveChanges();
        //            _response.Result = hiringManager;
        //            _response.StatusCode = HttpStatusCode.Created;
        //            return Ok(_response);

        //        }
        //    }

        //    catch (Exception ex)
        //    {

        //        _response.IsSuccess = false;
        //        _response.ErrorMessages = new List<string>() { ex.ToString() };

        //    }
        //    return _response;



        //}


        ////UPDATE

        //[HttpPut("{id:int}")]
        //public async Task<ActionResult<ApiResponse>> UpdateHiringManager(int id, [FromForm] HiringManagerUpdateDTO hiringManagerUpdateDTO)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            _response.IsSuccess = false;
        //            _response.StatusCode = HttpStatusCode.BadRequest;
        //            _response.ErrorMessages = ModelState.Values
        //                .SelectMany(x => x.Errors)
        //                .Select(e => e.ErrorMessage)
        //                .ToList();
        //            return BadRequest(_response);
        //        }

        //        HiringManager hiringManager = _db.HiringManagers.FirstOrDefault(u => u.ManagerID == id);

        //        if (hiringManager == null)
        //        {
        //            _response.StatusCode = HttpStatusCode.NotFound;
        //            _response.IsSuccess = false;
        //            return NotFound(_response);
        //        }


        //        var hiringManagerExists = _db.HiringManagers.Any(a => a.ManagerID == hiringManagerUpdateDTO.ManagerID);
        //        if (!hiringManagerExists)
        //        {
        //            _response.IsSuccess = false;
        //            _response.StatusCode = HttpStatusCode.BadRequest;
        //            _response.ErrorMessages = new List<string> { "ManagerID nuk ekziston." };
        //            return BadRequest(_response);
        //        }

        //        hiringManager.Name = hiringManagerUpdateDTO.Name;
        //        hiringManager.Specialization = hiringManagerUpdateDTO.Specialization;
        //        hiringManager.Email = hiringManagerUpdateDTO.Email;
        //        hiringManager.CompanyID = hiringManagerUpdateDTO.CompanyID;

        //        _db.SaveChanges();
        //        _response.StatusCode = HttpStatusCode.OK;
        //        _response.IsSuccess = true;
        //        return Ok(_response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages = new List<string> { ex.ToString() };
        //        // Log the exception for debugging
        //        Console.WriteLine(ex.ToString());
        //    }
        //    return _response;
        //}




        ////DELETE


        //// te delete po ma qet 204 edhe 200 response nSwagger amo kshtu pe fshin mire


        //[HttpDelete("{id:int}")]
        //public async Task<IActionResult> DeleteHiringManager(int id)
        //{
        //    if (id == 0)
        //    {
        //        _response.StatusCode = HttpStatusCode.BadRequest;
        //        _response.IsSuccess = false;
        //        return BadRequest(_response);
        //    }

        //    HiringManager hiringManager = _db.HiringManagers.FirstOrDefault(u => u.ManagerID == id);

        //    if (hiringManager == null)
        //    {
        //        _response.StatusCode = HttpStatusCode.NotFound;
        //        _response.IsSuccess = false;
        //        return NotFound(_response);
        //    }

        //    _db.HiringManagers.Remove(hiringManager);
        //    await _db.SaveChangesAsync();

        //    _response.StatusCode = HttpStatusCode.NoContent;
        //    return NoContent();
        //}







        //[HttpGet("GetHiringManagersBySpecialization")]
        //public IActionResult GetHiringManagersBySpecialization([FromQuery] string specialization)
        //{
        //    if (string.IsNullOrEmpty(specialization))
        //    {
        //        _response.StatusCode = HttpStatusCode.BadRequest;
        //        _response.IsSuccess = false;
        //        return BadRequest(_response);
        //    }

        //    var hiringManagers = _db.HiringManagers
        //        .Where(h => h.Specialization == specialization)
        //        .ToList();

        //    if (hiringManagers.Count == 0)
        //    {
        //        _response.StatusCode = HttpStatusCode.NotFound;
        //        _response.IsSuccess = false;
        //        return NotFound(_response);
        //    }

        //    _response.Result = hiringManagers;
        //    _response.StatusCode = HttpStatusCode.OK;
        //    return Ok(_response);
        //}


    }
}
