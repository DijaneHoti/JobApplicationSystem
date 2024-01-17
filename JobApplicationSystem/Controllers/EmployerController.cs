using JobApplicationSystem.Data;
using JobApplicationSystem.Models;
using JobApplicationSystem.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace JobApplicationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployerController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private ApiResponse _response;

        public EmployerController(ApplicationDbContext db)
        {
            _db = db;
            _response = new ApiResponse();
        }



        [HttpGet]
        public async Task<IActionResult> GetEmployers()
        {
            _response.Result = _db.Employers;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployer(int id)
        {
            if (id == 0)
            {

                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            Employer employer = _db.Employers.FirstOrDefault(u => u.EmployerID == id);

            if (employer == null)
            {

                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }

            _response.Result = employer;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);


        }


        //CREATE

        [HttpPost]

        public async Task<ActionResult<ApiResponse>> CreateEmployer([FromForm] EmployerCreateDTO employerCreateDTO)
        {
            try
            {


                Employer employer = new()
                {
                    CompanyID = employerCreateDTO.CompanyID,
                    Name = employerCreateDTO.Name,
                    Field = employerCreateDTO.Field,
                    Phone = employerCreateDTO.Phone,
                    Email = employerCreateDTO.Email,
                    Password = employerCreateDTO.Password,
                    Address = employerCreateDTO.Address,

                };

                if (ModelState.IsValid)
                {
                    _db.Employers.Add(employer);
                    _db.SaveChanges();
                    _response.Result = employer;
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
        public async Task<ActionResult<ApiResponse>> UpdateEmployer(int id, [FromForm] EmployerUpdateDTO employerUpdateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = ModelState.Values
                        .SelectMany(x => x.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(_response);
                }

                Employer employer = _db.Employers.FirstOrDefault(u => u.EmployerID == id);

                if (employer == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }

                
                var employerExists = _db.Employers.Any(a => a.EmployerID == employerUpdateDTO.EmployerID);
                if (!employerExists)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string> { "EmployerID nuk ekziston." };
                    return BadRequest(_response);
                }


                employer.Name = employerUpdateDTO.Name;
                employer.Field = employerUpdateDTO.Field;
                employer.Phone = employerUpdateDTO.Phone;
                employer.Email = employerUpdateDTO.Email;
                employer.Password = employerUpdateDTO.Password;
                employer.Address = employerUpdateDTO.Address;
                employer.CompanyID = employerUpdateDTO.CompanyID;

                _db.SaveChanges();
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                // Log the exception for debugging
                Console.WriteLine(ex.ToString());
            }
            return _response;
        }





        // te delete po ma qet 204 edhe 200 response nSwagger amo kshtu pe fshin mire

        [HttpDelete("{id:int}")]


        public async Task<IActionResult> DeleteEmployer(int id)
        {


            if (id == 0)
            {
                // if the item is wrong
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            Employer employer = _db.Employers.FirstOrDefault(u => u.EmployerID == id);


            if (employer == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }

            _db.Employers.Remove(employer);
            await _db.SaveChangesAsync();

            _response.StatusCode = HttpStatusCode.NoContent;
            return NoContent();


        }
    }
}
