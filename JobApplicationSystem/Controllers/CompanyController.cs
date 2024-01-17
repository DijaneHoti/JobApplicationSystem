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
    public class CompanyController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private ApiResponse _response;

        public CompanyController(ApplicationDbContext db)
        {
            _db = db;
            _response = new ApiResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
             _response.Result = _db.Companies.ToList();
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
         
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCompany(int? id)
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            Company company = _db.Companies.FirstOrDefault(u => u.CompanyID == id);

            if (company == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }

            _response.Result = company;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }


        //CREATE

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromForm] CompanyCreateDTO companyCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(ModelState);
            }

            var company = new Company
            {
                CompanyName = companyCreateDTO.CompanyName,
                hiringManager = companyCreateDTO.hiringManager
                //qito hiringManager spo di a osht mire se osht si list atje te atributet e klases Company
            };

            _db.Companies.Add(company);
            await _db.SaveChangesAsync();

            _response.Result = company;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }


        //UPDATE

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromForm] CompanyUpdateDTO companyUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(ModelState);
            }

            Company company = await _db.Companies.FindAsync(id);

            if (company == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }

            company.CompanyName = companyUpdateDTO.CompanyName;
            company.hiringManager = companyUpdateDTO.hiringManager;
            //qito hiringManager spo di a osht mire se osht si list atje te atributet e klases Company

            await _db.SaveChangesAsync();

            _response.Result = company;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }


        //DELETE


        // te delete po ma qet 204 edhe 200 response nSwagger amo kshtu pe fshin mire


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            Company company = _db.Companies.FirstOrDefault(u => u.CompanyID == id);

            if (company == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }

            _db.Companies.Remove(company);
            await _db.SaveChangesAsync();

            _response.StatusCode = HttpStatusCode.NoContent;
            return NoContent();
        }



    }
}
