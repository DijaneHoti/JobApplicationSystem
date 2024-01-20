using Azure;
using JobApplicationSystem.Data;
using JobApplicationSystem.Models;
using JobApplicationSystem.Models.Dto;
using JobApplicationSystem.Repository;
using JobApplicationSystem.UnitofWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JobApplicationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        //private readonly ApplicationDbContext _db;
        private ApiResponse _response;
        private readonly IUnitOfwork _unitOfWork;
        IRepository<Company> companyRepository;

        public CompanyController(IUnitOfwork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            companyRepository = new CompanyRepository(_unitOfWork);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            var actionResult = await companyRepository.Get();
            List<Company> companies = new List<Company>();
            if (actionResult.Result is OkObjectResult okResult)
            {
                 companies = okResult.Value as List<Company>;
                // Now you can work with your companies list.
            }
            var companyDto = companies.Select(c => new GetCompanyDTO
            {
                Id = c.CompanyID,
                CompanyName = c.CompanyName
            });

            return Ok(companyDto);
        }
        [HttpPost]
        public async Task<ActionResult<Company>> CreateCompany(CompanyCreateDTO companyDto)
        {
            //ti parameter ja qon kompanindto qe e ka veq ni emer
            //ama ktu te metoda duhet me i shku objekt Company
            //tash na e krijojm ni objekt t that Company edhe e mbushim me te dhena te viewmodelit

            Company company = new Company(){
                CompanyName = companyDto.CompanyName
            };
            var companies = await companyRepository.Create(company);
            return companies;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, CompanyUpdateDTO companyDto)
        {

            Company company = new Company()
            {
                CompanyID = id,
                CompanyName = companyDto.CompanyName
            };
            var companies = await companyRepository.Update(id, company);
            return companies;
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var companies = await companyRepository.Delete(id);
            return companies;
        }






        //[HttpGet("{id:int}")]
        //public async Task<IActionResult> GetCompany(int? id)
        //{
        //    if (id == 0)
        //    {
        //        _response.StatusCode = HttpStatusCode.BadRequest;
        //        _response.IsSuccess = false;
        //        return BadRequest(_response);
        //    }

        //    Company company = _db.Companies.FirstOrDefault(u => u.CompanyID == id);

        //    if (company == null)
        //    {
        //        _response.StatusCode = HttpStatusCode.NotFound;
        //        _response.IsSuccess = false;
        //        return NotFound(_response);
        //    }

        //    _response.Result = company;
        //    _response.StatusCode = HttpStatusCode.OK;
        //    return Ok(_response);
        //}


        //CREATE

        //[HttpPost]
        //public async Task<IActionResult> CreateCompany([FromForm] CompanyCreateDTO companyCreateDTO)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        _response.StatusCode = HttpStatusCode.BadRequest;
        //        _response.IsSuccess = false;
        //        return BadRequest(ModelState);
        //    }

        //    var company = new Company
        //    {
        //        CompanyName = companyCreateDTO.CompanyName,
        //       // hiringManager = companyCreateDTO.hiringManager

        //    };

        //    _db.Companies.Add(company);
        //    await _db.SaveChangesAsync();

        //    _response.Result = company;
        //    _response.StatusCode = HttpStatusCode.OK;
        //    return Ok(_response);
        //}


        //UPDATE

        //[HttpPut("{id:int}")]
        //public async Task<IActionResult> UpdateCompany(int id, [FromForm] CompanyUpdateDTO companyUpdateDTO)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        _response.StatusCode = HttpStatusCode.BadRequest;
        //        _response.IsSuccess = false;
        //        return BadRequest(ModelState);
        //    }

        //    Company company = await _db.Companies.FindAsync(id);

        //    if (company == null)
        //    {
        //        _response.StatusCode = HttpStatusCode.NotFound;
        //        _response.IsSuccess = false;
        //        return NotFound(_response);
        //    }

        //    company.CompanyName = companyUpdateDTO.CompanyName;
        //    //company.hiringManager = companyUpdateDTO.hiringManager;
        //    //qito hiringManager spo di a osht mire se osht si list atje te atributet e klases Company

        //    await _db.SaveChangesAsync();

        //    _response.Result = company;
        //    _response.StatusCode = HttpStatusCode.OK;
        //    return Ok(_response);
        //}


        //DELETE


        // te delete po ma qet 204 edhe 200 response nSwagger amo kshtu pe fshin mire


        //[HttpDelete("{id:int}")]
        //public async Task<IActionResult> DeleteCompany(int id)
        //{
        //    if (id == 0)
        //    {
        //        _response.StatusCode = HttpStatusCode.BadRequest;
        //        _response.IsSuccess = false;
        //        return BadRequest(_response);
        //    }

        //    Company company = _db.Companies.FirstOrDefault(u => u.CompanyID == id);

        //    if (company == null)
        //    {
        //        _response.StatusCode = HttpStatusCode.NotFound;
        //        _response.IsSuccess = false;
        //        return NotFound(_response);
        //    }

        //    _db.Companies.Remove(company);
        //    await _db.SaveChangesAsync();

        //    _response.StatusCode = HttpStatusCode.NoContent;
        //    return NoContent();
        //}



    }
}
