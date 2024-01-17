using Azure;
using JobApplicationSystem.Data;
using JobApplicationSystem.Models;
using JobApplicationSystem.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace JobApplicationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HiringManagerController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private ApiResponse _response;

        public HiringManagerController(ApplicationDbContext db)
        {
            _db = db;
            _response = new ApiResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetHiringManagers()
        {
            _response.Result = _db.HiringManagers.ToList();
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);

        }

        //[HttpGet("{id:int}")]
        //public async Task<IActionResult> GetHiringManager(int? id)
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

        //    _response.Result = hiringManager;
        //    _response.StatusCode = HttpStatusCode.OK;
        //    return Ok(_response);
        //}


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetHiringManager(int id)
        {
            if (id == 0)
            {
                // _response.StatusCode = HttpStatusCode.BadRequest;
                // _response.IsSuccess = false;
                // return BadRequest(_response);
                return BadRequest();
            }

            var hiringManager = await _db.HiringManagers.Include(x => x.Company).Where(u => u.ManagerID == id).FirstOrDefaultAsync();

            if (hiringManager == null)
            {
                // _response.StatusCode = HttpStatusCode.NotFound;
                // _response.IsSuccess = false;
                // return NotFound(_response);
                return NotFound();
            }

            var result = new GetHiringManagerDTO() { 
                ManagerID = hiringManager.ManagerID,
                Name = hiringManager.Name,
                Specialization = hiringManager.Specialization,
                Email = hiringManager.Email,
                Company = hiringManager.Company.CompanyName
            };
            return Ok(result);
        }
















        //CREATE

        [HttpPost]

        public async Task<ActionResult<ApiResponse>> CreateHiringManager([FromForm] HiringManagerCreateDTO hiringManagerCreateDTO)
        {
            try
            {


                HiringManager hiringManager = new()
                {
                    CompanyID = hiringManagerCreateDTO.CompanyID,
                    Name = hiringManagerCreateDTO.Name,
                    Specialization = hiringManagerCreateDTO.Specialization,
                    Email = hiringManagerCreateDTO.Email,
                  
                };

                if (ModelState.IsValid)
                {
                    _db.HiringManagers.Add(hiringManager);
                    _db.SaveChanges();
                    _response.Result = hiringManager;
                    _response.StatusCode = HttpStatusCode.Created;
                    return Ok(_response);

                }
            }

            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;



        }


        //UPDATE

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateHiringManager(int id, [FromForm] HiringManagerUpdateDTO hiringManagerUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(ModelState);
            }

            HiringManager hiringManager = await _db.HiringManagers.FindAsync(id);

            if (hiringManager == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }

            hiringManager.Name = hiringManagerUpdateDTO.Name;
            hiringManager.Specialization = hiringManagerUpdateDTO.Specialization;
            hiringManager.Email = hiringManagerUpdateDTO.Email;

          

            await _db.SaveChangesAsync();

            _response.Result = hiringManager;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }


        //DELETE


        // te delete po ma qet 204 edhe 200 response nSwagger amo kshtu pe fshin mire


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteHiringManager(int id)
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            HiringManager hiringManager = _db.HiringManagers.FirstOrDefault(u => u.ManagerID == id);

            if (hiringManager == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }

            _db.HiringManagers.Remove(hiringManager);
            await _db.SaveChangesAsync();

            _response.StatusCode = HttpStatusCode.NoContent;
            return NoContent();
        }



    }
}
