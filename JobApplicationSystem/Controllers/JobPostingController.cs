using JobApplicationSystem.Data;
using JobApplicationSystem.Models.Dto;
using JobApplicationSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using JobApplicationSystem.Repository;
using JobApplicationSystem.UnitofWork;

namespace JobApplicationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPostingController : ControllerBase
    {

        private ApiResponse _response;
        private readonly IUnitOfwork _unitOfWork;
        IRepository<JobPosting> jobPostingRepository;
        private readonly ApplicationDbContext _context;

        public JobPostingController(IUnitOfwork unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            jobPostingRepository = new JobPostingRepository(_unitOfWork);
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobPosting>>> Get()
        {
            var actionResult = await jobPostingRepository.Get();
            List<JobPosting> jobPostings = new List<JobPosting>();
            if (actionResult.Result is OkObjectResult okResult)
            {
                jobPostings = okResult.Value as List<JobPosting>;
                // Now you can work with your companies list.
            }
            var jobPostingDto = jobPostings.Select(c => new GetJobPostingDTO
            {
                JobPostingID = c.JobPostingID,
                JobPostingTitle = c.JobPostingTitle,
                JobPostingDescription = c.JobPostingDescription,

            });

            return Ok(jobPostingDto);
        }

        //CREATE

        [HttpPost]
        public async Task<ActionResult<JobPosting>> CreateJobPosting(JobPostingCreateDTO jobPostingDto)
        {


            //ti parameter ja qon kompanindto qe e ka veq ni emer
            //ama ktu te metoda duhet me i shku objekt Company
            //tash na e krijojm ni objekt t that Company edhe e mbushim me te dhena te viewmodelit

            JobPosting jobPosting = new JobPosting()
            {
                JobPostingTitle = jobPostingDto.JobPostingTitle,
                JobPostingDescription = jobPostingDto.JobPostingDescription,
                JobID = jobPostingDto.JobID,

            };
            var jobPostings = await jobPostingRepository.Create(jobPosting);
            return Ok(jobPostings);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJobPosting(int id, JobPostingUpdateDTO jobPostingDto)
        {

            JobPosting jobPosting = new JobPosting()
            {
                JobPostingID = id,
                JobPostingTitle = jobPostingDto.JobPostingTitle,
                JobPostingDescription = jobPostingDto.JobPostingDescription,
                JobID = jobPostingDto.JobID

            };
            var jobPostings = await jobPostingRepository.Update(id, jobPosting);
            return jobPostings;
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobPosting(int id)
        {
            var jobPostings = await jobPostingRepository.Delete(id);
            return jobPostings;
        }



        //---------------------------------------------- Metoda SUBMIT

        //[HttpPost("SubmitJobPosting")]
        //[RequireHttps]
        //public async Task<ActionResult<ApiResponse>> SubmitJobPosting([FromForm] JobPostingCreateDTO jobPostingCreateDTO)
        //{
        //    try
        //    {
        //        // Check if all fields are filled
        //        if (string.IsNullOrEmpty(jobPostingCreateDTO.JobPostingTitle) ||
        //            string.IsNullOrEmpty(jobPostingCreateDTO.JobPostingDescription))
        //        {
        //            _response.StatusCode = HttpStatusCode.BadRequest;
        //            _response.IsSuccess = false;
        //            _response.ErrorMessages = new List<string> { "All fields must be filled." };
        //            return BadRequest(_response);
        //        }

        //        // Set the response properties
        //        _response.Result = "Job posting submitted successfully!";
        //        _response.StatusCode = HttpStatusCode.OK;

        //        // Return the response
        //        return Ok(_response);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exceptions
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages = new List<string> { ex.ToString() };
        //        Console.WriteLine(ex.ToString());
        //    }

        //    // Return the response in case of an error
        //    return _response;
        //}


        //------------------------------------------------ Edited

        [HttpPost("SubmitJobPosting")]
        [RequireHttps]
        public async Task<ActionResult<ApiResponse>> SubmitJobPosting([FromForm] JobPostingCreateDTO jobPostingCreateDTO)
        {
            try
            {
                // Check if all fields are filled
                if (string.IsNullOrEmpty(jobPostingCreateDTO.JobPostingTitle) ||
                    string.IsNullOrEmpty(jobPostingCreateDTO.JobPostingDescription))
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "All fields must be filled." };
                    return BadRequest(_response);
                }

                // If there's an object that may be null, check and handle it
                // Example: Assuming _response is null, initialize it before using
                if (_response == null)
                {
                    _response = new ApiResponse(); // Adjust this based on your actual ApiResponse class
                }

                // Set the response properties
                _response.Result = "Job posting submitted successfully!";
                _response.StatusCode = HttpStatusCode.OK;

                // Return the response
                return Ok(_response);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred: {ex}");

                // Handle exceptions
                _response = new ApiResponse(); // Adjust this based on your actual ApiResponse class
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "An error occurred while processing the request." };

                // Return the response in case of an error
                return BadRequest(_response);
            }
        }
































        //private readonly ApplicationDbContext _db;
        //private ApiResponse _response;

        //public JobPostingController(ApplicationDbContext db)
        //{
        //    _db = db;
        //   _response = new ApiResponse();
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetJobPostings()
        //{
        //    _response.Result = _db.JobPostings;
        //    _response.StatusCode = HttpStatusCode.OK;
        //    return Ok(_response);
        //}

        //[HttpGet("{id:int}")]
        //public async Task<IActionResult> GetJobPosting(int id)
        //{
        //    if (id == 0)
        //    {

        //        _response.StatusCode = HttpStatusCode.BadRequest;
        //        _response.IsSuccess = false;
        //        return BadRequest(_response);
        //    }

        //    JobPosting jobPosting = _db.JobPostings.FirstOrDefault(u => u.JobPostingID == id);

        //    if (jobPosting == null)
        //    {

        //        _response.StatusCode = HttpStatusCode.NotFound;
        //        _response.IsSuccess = false;
        //        return NotFound(_response);
        //    }

        //    _response.Result = jobPosting;
        //    _response.StatusCode = HttpStatusCode.OK;
        //    return Ok(_response);


        //}


        ////CREATE

        //[HttpPost]

        //public async Task<ActionResult<ApiResponse>> CreateJobPosting([FromForm] JobPostingCreateDTO jobPostingCreateDTO)
        //{
        //    try
        //    {


        //        JobPosting jobPosting = new()
        //        {
        //            JobID = jobPostingCreateDTO.JobID,
        //            JobPostingTitle = jobPostingCreateDTO.JobPostingTitle,
        //            JobPostingDescription = jobPostingCreateDTO.JobPostingDescription

        //        };

        //        if (ModelState.IsValid)
        //        {
        //            _db.JobPostings.Add(jobPosting);
        //            _db.SaveChanges();
        //            _response.Result = jobPosting;
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
        //public async Task<ActionResult<ApiResponse>> UpdateJobPosting(int id, [FromForm] JobPostingUpdateDTO jobPostingUpdateDTO)
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

        //        JobPosting jobPosting = _db.JobPostings.FirstOrDefault(u => u.JobPostingID == id);

        //        if (jobPosting == null)
        //        {
        //            _response.StatusCode = HttpStatusCode.NotFound;
        //            _response.IsSuccess = false;
        //            return NotFound(_response);
        //        }


        //        var jobPostingExists = _db.JobPostings.Any(a => a.JobPostingID == jobPostingUpdateDTO.JobPostingID);
        //        if (!jobPostingExists)
        //        {
        //            _response.IsSuccess = false;
        //            _response.StatusCode = HttpStatusCode.BadRequest;
        //            _response.ErrorMessages = new List<string> { "JobPostingID nuk ekziston." };
        //            return BadRequest(_response);
        //        }


        //        jobPosting.JobPostingTitle = jobPostingUpdateDTO.JobPostingTitle;
        //        jobPosting.JobPostingDescription = jobPostingUpdateDTO.JobPostingDescription;
        //        jobPosting.JobID = jobPostingUpdateDTO.JobID;

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


        //public async Task<IActionResult> DeleteJobPosting(int id)
        //{


        //    if (id == 0)
        //    {
        //        // if the item is wrong
        //        _response.StatusCode = HttpStatusCode.BadRequest;
        //        _response.IsSuccess = false;
        //        return BadRequest(_response);
        //    }

        //    JobPosting jobPosting = _db.JobPostings.FirstOrDefault(u => u.JobPostingID == id);


        //    if (jobPosting == null)
        //    {
        //        _response.StatusCode = HttpStatusCode.NotFound;
        //        _response.IsSuccess = false;
        //        return NotFound(_response);
        //    }

        //    _db.JobPostings.Remove(jobPosting);
        //    await _db.SaveChangesAsync();

        //    _response.StatusCode = HttpStatusCode.NoContent;
        //    return NoContent();


        //}






    }
}
