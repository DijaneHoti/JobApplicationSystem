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
                if (_response == null)
                {
                    _response = new ApiResponse();
                }


                _response.Result = "Job posting submitted successfully!";
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred: {ex}");

                // Handle exceptions
                _response = new ApiResponse();
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "An error occurred while processing the request." };

                return BadRequest(_response);
            }
        }







    }
}
