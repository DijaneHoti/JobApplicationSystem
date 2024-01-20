using JobApplicationSystem.Data;
using JobApplicationSystem.Models;
using JobApplicationSystem.Models.Dto;
using JobApplicationSystem.Repository;
using JobApplicationSystem.UnitofWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.RegularExpressions;

namespace JobApplicationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployerController : ControllerBase
    {
        private ApiResponse _response;
        private readonly IUnitOfwork _unitOfWork;
        IRepository<Employer> employerRepository;
        private readonly ApplicationDbContext _context;

        //public EmployerController(ApplicationDbContext db)
        //{
        //    _db = db;
        //    _response = new ApiResponse();
        //}


        public EmployerController(IUnitOfwork unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            employerRepository = new EmployerRepository(_unitOfWork);
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employer>>> Get()
        {
            var actionResult = await employerRepository.Get();
            List<Employer> employers = new List<Employer>();
            if (actionResult.Result is OkObjectResult okResult)
            {
                employers = okResult.Value as List<Employer>;
                // Now you can work with your companies list.
            }
            var employerDto = employers.Select(c => new GetEmployerDTO
            {
                EmployerID = c.EmployerID,
                Name = c.Name,
                Field = c.Field,
                Phone = c.Phone,
                Email = c.Email,
                Password = c.Password ,
                Address = c.Address
            });

            return Ok(employerDto);
        }

        //CREATE

        [HttpPost]
        public async Task<ActionResult<Employer>> CreateEmployer(EmployerCreateDTO employerDto)
        {
            

            //ti parameter ja qon kompanindto qe e ka veq ni emer
            //ama ktu te metoda duhet me i shku objekt Company
            //tash na e krijojm ni objekt t that Company edhe e mbushim me te dhena te viewmodelit

            Employer employer = new Employer()
            {
                Name = employerDto.Name,
                Field = employerDto.Field,
                Phone = employerDto.Phone,
                Email = employerDto.Email,
                Password = employerDto.Password,
                Address = employerDto.Address,
                CompanyID = employerDto.CompanyID,

            };
            var employers = await employerRepository.Create(employer);
            return Ok(employers);
        }
       

        //UPDATE

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployer(int id, EmployerUpdateDTO employerDto)
        {

            Employer employer = new Employer()
            {
                EmployerID = id,
                Name = employerDto.Name,
                Field = employerDto.Field,
                Phone = employerDto.Phone,
                Email = employerDto.Email,
                Password = employerDto.Password,
                Address = employerDto.Address,
                CompanyID = employerDto.CompanyID

            };
            var employers = await employerRepository.Update(id, employer);
            return employers;
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployer(int id)
        {
            var employers = await employerRepository.Delete(id);
            return employers;
        }





        //-----------------------------------------------------ERROR 500

        //[HttpGet("GetEmployersByField")]
        //public IActionResult GetEmployersByField([FromQuery] string field)
        //{
        //    if (string.IsNullOrEmpty(field))
        //    {
        //        _response.StatusCode = HttpStatusCode.BadRequest;
        //        _response.IsSuccess = false;
        //        return BadRequest(_response);
        //    }

        //    var employers = _context.Employers
        //        .Where(e => e.Field == field)
        //        .Select(e => new GetEmployerByFieldDTO
        //        {
        //            EmployerID = e.EmployerID,
        //            Name = e.Name,
        //            Email = e.Email
        //            // Include other properties you want to expose in the DTO
        //        })
        //        .ToList();

        //    if (employers.Count == 0)
        //    {
        //        _response.StatusCode = HttpStatusCode.NotFound;
        //        _response.IsSuccess = false;
        //        return NotFound(_response);
        //    }

        //    _response.Result = employers;
        //    _response.StatusCode = HttpStatusCode.OK;
        //    return Ok(_response);
        //}










        //--------------------------------------------- me patterna

        //[HttpGet("GetEmployersByField")]
        //public async Task<ActionResult<IEnumerable<Employer>>> GetEmployersByField([FromQuery] string field)
        //{
        //    if (string.IsNullOrEmpty(field))
        //    {
        //        _response.StatusCode = HttpStatusCode.BadRequest;
        //        _response.IsSuccess = false;
        //        return BadRequest(_response);
        //    }

        //    var employers = await employerRepository.GetByField(field);
        //    List<Employer> employers1 = new List<Employer>();

        //    if (employers.Result is OkObjectResult okResult)
        //    {
        //        employers1 = okResult.Value as List<Employer>;

        //    }

        //    var employerDto = employers1.Select(c => new GetEmployerDTO
        //    {
        //        EmployerID = c.EmployerID,
        //        Name = c.Name,
        //        Field = c.Field,
        //        Phone = c.Phone,
        //        Email = c.Email,
        //        Password = c.Password,
        //        Address = c.Address,
        //        Company = c.Company.CompanyName,

        //    });

        //    return Ok(employerDto);
        //}



        //---------------------------------------------------------------------



        //[HttpGet]
        //public async Task<IActionResult> GetEmployers()
        //{
        //    _response.Result = _db.Employers;
        //    _response.StatusCode = HttpStatusCode.OK;
        //    return Ok(_response);
        //}

        //[HttpGet("{id:int}")]
        //public async Task<IActionResult> GetEmployer(int id)
        //{
        //    if (id == 0)
        //    {

        //        _response.StatusCode = HttpStatusCode.BadRequest;
        //        _response.IsSuccess = false;
        //        return BadRequest(_response);
        //    }

        //    Employer employer = _db.Employers.FirstOrDefault(u => u.EmployerID == id);

        //    if (employer == null)
        //    {

        //        _response.StatusCode = HttpStatusCode.NotFound;
        //        _response.IsSuccess = false;
        //        return NotFound(_response);
        //    }

        //    _response.Result = employer;
        //    _response.StatusCode = HttpStatusCode.OK;
        //    return Ok(_response);


        //}


        ////CREATE

        //[HttpPost]

        //public async Task<ActionResult<ApiResponse>> CreateEmployer([FromForm] EmployerCreateDTO employerCreateDTO)
        //{
        //    try
        //    {


        //        Employer employer = new()
        //        {
        //            CompanyID = employerCreateDTO.CompanyID,
        //            Name = employerCreateDTO.Name,
        //            Field = employerCreateDTO.Field,
        //            Phone = employerCreateDTO.Phone,
        //            Email = employerCreateDTO.Email,
        //            Password = employerCreateDTO.Password,
        //            Address = employerCreateDTO.Address,

        //        };

        //        if (ModelState.IsValid)
        //        {
        //            _db.Employers.Add(employer);
        //            _db.SaveChanges();
        //            _response.Result = employer;
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
        //public async Task<ActionResult<ApiResponse>> UpdateEmployer(int id, [FromForm] EmployerUpdateDTO employerUpdateDTO)
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

        //        Employer employer = _db.Employers.FirstOrDefault(u => u.EmployerID == id);

        //        if (employer == null)
        //        {
        //            _response.StatusCode = HttpStatusCode.NotFound;
        //            _response.IsSuccess = false;
        //            return NotFound(_response);
        //        }


        //        var employerExists = _db.Employers.Any(a => a.EmployerID == employerUpdateDTO.EmployerID);
        //        if (!employerExists)
        //        {
        //            _response.IsSuccess = false;
        //            _response.StatusCode = HttpStatusCode.BadRequest;
        //            _response.ErrorMessages = new List<string> { "EmployerID nuk ekziston." };
        //            return BadRequest(_response);
        //        }


        //        employer.Name = employerUpdateDTO.Name;
        //        employer.Field = employerUpdateDTO.Field;
        //        employer.Phone = employerUpdateDTO.Phone;
        //        employer.Email = employerUpdateDTO.Email;
        //        employer.Password = employerUpdateDTO.Password;
        //        employer.Address = employerUpdateDTO.Address;
        //        employer.CompanyID = employerUpdateDTO.CompanyID;

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






        //[HttpDelete("{id:int}")]


        //public async Task<IActionResult> DeleteEmployer(int id)
        //{


        //    if (id == 0)
        //    {
        //        // if the item is wrong
        //        _response.StatusCode = HttpStatusCode.BadRequest;
        //        _response.IsSuccess = false;
        //        return BadRequest(_response);
        //    }

        //    Employer employer = _db.Employers.FirstOrDefault(u => u.EmployerID == id);


        //    if (employer == null)
        //    {
        //        _response.StatusCode = HttpStatusCode.NotFound;
        //        _response.IsSuccess = false;
        //        return NotFound(_response);
        //    }

        //    _db.Employers.Remove(employer);
        //    await _db.SaveChangesAsync();

        //    _response.StatusCode = HttpStatusCode.NoContent;
        //    return NoContent();


        //}





    }
}
