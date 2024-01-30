
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using JobApplicationSystem.Models;
using JobApplicationSystem.Data;
using JobApplicationSystem.Models.Dto;
using JobApplicationSystem.Repository;
using JobApplicationSystem.UnitofWork;

namespace JobseekerApplicationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobseekerController : ControllerBase
    {


        private ApiResponse _response;
        private readonly IUnitOfwork _unitOfWork;
        IRepository<Jobseeker> jobseekerRepository;
        private readonly ApplicationDbContext _context;

        public JobseekerController(IUnitOfwork unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            jobseekerRepository = new JobseekerRepository(_unitOfWork);
            _context = context;
        }


        [HttpGet("Index")]
        public async Task<ActionResult<IEnumerable<Jobseeker>>> Get()
        {
            var actionResult = await jobseekerRepository.Get();
            List<Jobseeker> jobseekers = new List<Jobseeker>();
            if (actionResult.Result is OkObjectResult okResult)
            {
                jobseekers = okResult.Value as List<Jobseeker>;
                // Now you can work with your companies list.
            }
            var jobseekerDto = jobseekers.Select(c => new GetJobseekerDTO
            {
                JobseekerID = c.JobseekerID,
                JobseekerName = c.JobseekerName,
                JobseekerEmail = c.JobseekerEmail,

            });

            return Ok(jobseekerDto);
        }


        //CREATE

        [HttpPost]
        public async Task<ActionResult<Jobseeker>> CreateJobseeker(JobseekerCreateDTO jobseekerDto)
        {



            Jobseeker jobseeker = new Jobseeker()
            {
                JobseekerName = jobseekerDto.JobseekerName,
                JobseekerEmail = jobseekerDto.JobseekerEmail,
                //JobPostingID = jobseekerDto.JobPostingID,

            };
            var jobseekers = await jobseekerRepository.Create(jobseeker);
            return Ok(jobseekers);
        }




        //UPDATE


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobseeker(int id, JobseekerUpdateDTO jobseekerDto)
        {

            Jobseeker jobseeker = new Jobseeker()
            {
                JobseekerID = id,
                JobseekerName = jobseekerDto.JobseekerName,
                JobseekerEmail = jobseekerDto.JobseekerEmail,
                //JobPostingID = jobseekerDto.JobPostingID

            };
            var jobseekers = await jobseekerRepository.Update(id, jobseeker);
            return jobseekers;
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobseeker(int id)
        {
            var jobseekers = await jobseekerRepository.Delete(id);
            return jobseekers;
        }


























        //private readonly ApplicationDbContext _db;
        //private ApiResponse _response;

        //public JobseekerController(ApplicationDbContext db)
        //{
        //    _db = db;
        //    _response = new ApiResponse();
        //}



        //[HttpGet]
        //public async Task<IActionResult> GetJobseekers()
        //{
        //    _response.Result = _db.Jobseekers;
        //    _response.StatusCode = HttpStatusCode.OK;
        //    return Ok(_response);
        //}

        //[HttpGet("{id:int}")]
        //public async Task<IActionResult> GetJobseeker(int id)
        //{
        //    if (id == 0)
        //    {

        //        _response.StatusCode = HttpStatusCode.BadRequest;
        //        _response.IsSuccess = false;
        //        return BadRequest(_response);
        //    }

        //    Jobseeker jobseeker = _db.Jobseekers.FirstOrDefault(u => u.JobseekerID == id);

        //    if (jobseeker == null)
        //    {

        //        _response.StatusCode = HttpStatusCode.NotFound;
        //        _response.IsSuccess = false;
        //        return NotFound(_response);
        //    }

        //    _response.Result = jobseeker;
        //    _response.StatusCode = HttpStatusCode.OK;
        //    return Ok(_response);


        //}


        ////CREATE

        //[HttpPost]

        //public async Task<ActionResult<ApiResponse>> CreateJobseeker([FromForm] JobseekerCreateDTO jobseekerCreateDTO)
        //{
        //    try
        //    {


        //        Jobseeker jobseeker = new()
        //        {
        //            JobPostingID = jobseekerCreateDTO.JobPostingID,
        //            JobseekerName = jobseekerCreateDTO.JobseekerName,
        //            JobseekerEmail = jobseekerCreateDTO.JobseekerEmail,

        //        };

        //        if (ModelState.IsValid)
        //        {
        //            _db.Jobseekers.Add(jobseeker);
        //            _db.SaveChanges();
        //            _response.Result = jobseeker;
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
        //public async Task<ActionResult<ApiResponse>> UpdateJobseeker(int id, [FromForm] JobseekerUpdateDTO jobseekerUpdateDTO)
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

        //        Jobseeker jobseeker = _db.Jobseekers.FirstOrDefault(u => u.JobseekerID == id);

        //        if (jobseeker == null)
        //        {
        //            _response.StatusCode = HttpStatusCode.NotFound;
        //            _response.IsSuccess = false;
        //            return NotFound(_response);
        //        }


        //        var jobseekerExists = _db.Jobseekers.Any(a => a.JobseekerID == jobseekerUpdateDTO.JobseekerID);
        //        if (!jobseekerExists)
        //        {
        //            _response.IsSuccess = false;
        //            _response.StatusCode = HttpStatusCode.BadRequest;
        //            _response.ErrorMessages = new List<string> { "JobseekerID nuk ekziston." };
        //            return BadRequest(_response);
        //        }


        //        jobseeker.JobseekerName = jobseekerUpdateDTO.JobseekerName;
        //        jobseeker.JobseekerEmail = jobseekerUpdateDTO.JobseekerEmail;
        //        jobseeker.JobPostingID = jobseekerUpdateDTO.JobPostingID;

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


        //public async Task<IActionResult> DeleteJobseeker(int id)
        //{


        //    if (id == 0)
        //    {
        //        // if the item is wrong
        //        _response.StatusCode = HttpStatusCode.BadRequest;
        //        _response.IsSuccess = false;
        //        return BadRequest(_response);
        //    }

        //    Jobseeker jobseeker = _db.Jobseekers.FirstOrDefault(u => u.JobseekerID == id);


        //    if (jobseeker == null)
        //    {
        //        _response.StatusCode = HttpStatusCode.NotFound;
        //        _response.IsSuccess = false;
        //        return NotFound(_response);
        //    }

        //    _db.Jobseekers.Remove(jobseeker);
        //    await _db.SaveChangesAsync();

        //    _response.StatusCode = HttpStatusCode.NoContent;
        //    return NoContent();


        //}
    }
}
