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


            
            // 1. pre condition && invariant e validon a jon ne rregull inputat qysh i kem cek
            //qe duhen mu kon ne dto
            employerDto.Validate();

            var employees = await _context.Employers.ToListAsync();
            
            //2. permban invariance
            if (employees.Any(x=>x.Email == employerDto.Email))
            {
                return BadRequest("Employee with this email" + employerDto.Email +" exists!!");
            }

            Employer employer = new Employer()
            {
                Name = employerDto.Name,
                Field = employerDto.Field,
                Phone = employerDto.Phone,
                Email = employerDto.Email,
                Password = employerDto.Password,
                Address = employerDto.Address,
                CompanyID = employerDto.CompanyID,
                InsertedDate = employerDto.InsertedDate,

            };


            var employers = await employerRepository.Create(employer);
            
            //3. post condition e kthen Ok me json nese o successful
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






    }
}
