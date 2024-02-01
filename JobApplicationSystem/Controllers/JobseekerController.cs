
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




    }
}
