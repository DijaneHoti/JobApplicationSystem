using Azure;
using JobApplicationSystem.Data;
using JobApplicationSystem.Models;
using JobApplicationSystem.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetHiringManager(int? id)
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

            _response.Result = hiringManager;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }


        //CREATE

        [HttpPost]
        public async Task<IActionResult> CreateHiringManager([FromForm] HiringManagerCreateDTO hiringManagerCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(ModelState);
            }

            var hiringManager = new HiringManager
            {
                Name = hiringManagerCreateDTO.Name,
                Specialization = hiringManagerCreateDTO.Specialization,
                Email = hiringManagerCreateDTO.Email,
               
            };

            _db.HiringManagers.Add(hiringManager);
            await _db.SaveChangesAsync();

            _response.Result = hiringManager;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
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
