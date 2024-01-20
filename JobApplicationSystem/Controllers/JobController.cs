using JobApplicationSystem.Data;
using JobApplicationSystem.Models;
using JobApplicationSystem.Models.Dto;
using JobApplicationSystem.Repository;
using JobApplicationSystem.UnitofWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace JobApplicationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private ApiResponse _response;
        private readonly IUnitOfwork _unitOfWork;
        IRepository<Job> jobRepository;
        private readonly ApplicationDbContext _context;

        public JobController(IUnitOfwork unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            jobRepository = new JobRepository(_unitOfWork);
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> Get()
        {
            var actionResult = await jobRepository.Get();
            List<Job> jobs = new List<Job>();
            if (actionResult.Result is OkObjectResult okResult)
            {
                jobs = okResult.Value as List<Job>;
                // Now you can work with your companies list.
            }
            var jobDto = jobs.Select(c => new GetJobDTO
            {
                JobID = c.JobID,
                PositionName = c.PositionName,
                Description = c.Description,
              
            });

            return Ok(jobDto);
        }


        //CREATE

        [HttpPost]
        public async Task<ActionResult<Job>> CreateJob(JobCreateDTO jobDto)
        {


            //ti parameter ja qon kompanindto qe e ka veq ni emer
            //ama ktu te metoda duhet me i shku objekt Company
            //tash na e krijojm ni objekt t that Company edhe e mbushim me te dhena te viewmodelit

            Job job = new Job()
            {
                PositionName = jobDto.PositionName,
                Description = jobDto.Description,
                CompanyID = jobDto.CompanyID,

            };
            var jobs = await jobRepository.Create(job);
            return Ok(jobs);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJob(int id, JobUpdateDTO jobDto)
        {

            Job job = new Job()
            {
                JobID = id,
                PositionName = jobDto.PositionName,
                Description = jobDto.Description,
                CompanyID = jobDto.CompanyID

            };
            var jobs = await jobRepository.Update(id, job);
            return jobs;
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var jobs = await jobRepository.Delete(id);
            return jobs;
        }























        //private readonly ApplicationDbContext _db;
        //private ApiResponse _response;

        //public JobController(ApplicationDbContext db)
        //{
        //    _db = db;
        //    _response = new ApiResponse();
        //}



        //[HttpGet]
        //public async Task<IActionResult> GetJobs()
        //{
        //    _response.Result = _db.Jobs;
        //    _response.StatusCode = HttpStatusCode.OK;
        //    return Ok(_response);
        //}

        //[HttpGet("{id:int}")]
        //public async Task<IActionResult> GetJob(int id)
        //{
        //    if (id == 0)
        //    {

        //        _response.StatusCode = HttpStatusCode.BadRequest;
        //        _response.IsSuccess = false;
        //        return BadRequest(_response);
        //    }

        //    Job job = _db.Jobs.FirstOrDefault(u => u.JobID == id);

        //    if (job == null)
        //    {

        //        _response.StatusCode = HttpStatusCode.NotFound;
        //        _response.IsSuccess = false;
        //        return NotFound(_response);
        //    }

        //    _response.Result = job;
        //    _response.StatusCode = HttpStatusCode.OK;
        //    return Ok(_response);


        //}


        ////CREATE

        //[HttpPost]

        //public async Task<ActionResult<ApiResponse>> CreateJob([FromForm] JobCreateDTO jobCreateDTO)
        //{
        //    try
        //    {


        //        Job job = new()
        //        {
        //            CompanyID = jobCreateDTO.CompanyID,
        //            PositionName = jobCreateDTO.PositionName,
        //            Description = jobCreateDTO.Description,

        //        };

        //        if (ModelState.IsValid)
        //        {
        //            _db.Jobs.Add(job);
        //            _db.SaveChanges();
        //            _response.Result = job;
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
        //public async Task<ActionResult<ApiResponse>> UpdateJob(int id, [FromForm] JobUpdateDTO jobUpdateDTO)
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

        //        Job job = _db.Jobs.FirstOrDefault(u => u.JobID == id);

        //        if (job == null)
        //        {
        //            _response.StatusCode = HttpStatusCode.NotFound;
        //            _response.IsSuccess = false;
        //            return NotFound(_response);
        //        }


        //        var jobExists = _db.Jobs.Any(a => a.JobID == jobUpdateDTO.JobID);
        //        if (!jobExists)
        //        {
        //            _response.IsSuccess = false;
        //            _response.StatusCode = HttpStatusCode.BadRequest;
        //            _response.ErrorMessages = new List<string> { "JobID nuk ekziston." };
        //            return BadRequest(_response);
        //        }


        //        job.PositionName = jobUpdateDTO.PositionName;
        //        job.Description = jobUpdateDTO.Description;
        //        job.CompanyID = jobUpdateDTO.CompanyID;

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





        //// te delete po ma qet 204 edhe 200 response nSwagger amo kshtu pe fshin mire

        //[HttpDelete("{id:int}")]


        //public async Task<IActionResult> DeleteJob(int id)
        //{


        //    if (id == 0)
        //    {
        //        // if the item is wrong
        //        _response.StatusCode = HttpStatusCode.BadRequest;
        //        _response.IsSuccess = false;
        //        return BadRequest(_response);
        //    }

        //    Job job = _db.Jobs.FirstOrDefault(u => u.JobID == id);


        //    if (job == null)
        //    {
        //        _response.StatusCode = HttpStatusCode.NotFound;
        //        _response.IsSuccess = false;
        //        return NotFound(_response);
        //    }

        //    _db.Jobs.Remove(job);
        //    await _db.SaveChangesAsync();

        //    _response.StatusCode = HttpStatusCode.NoContent;
        //    return NoContent();


        //}


        //[HttpGet("GetJobByPositionName")]
        //public IActionResult GetJobByPositionName([FromQuery] string positionName)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(positionName))
        //        {
        //            _response.StatusCode = HttpStatusCode.BadRequest;
        //            _response.IsSuccess = false;
        //            return BadRequest(_response);
        //        }

        //        var jobs = _db.Jobs
        //            .Where(j => j.PositionName == positionName)
        //            .ToList();

        //        if (jobs.Count == 0)
        //        {
        //            _response.StatusCode = HttpStatusCode.NotFound;
        //            _response.IsSuccess = false;
        //            return NotFound(_response);
        //        }

        //        _response.Result = jobs;
        //        _response.StatusCode = HttpStatusCode.OK;
        //        return Ok(_response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages = new List<string> { ex.ToString() };
        //        // Log the exception for debugging
        //        Console.WriteLine(ex.ToString());
        //        return Ok(_response);
        //    }
        //}
    }
}
