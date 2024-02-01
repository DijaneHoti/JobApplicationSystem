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



            Job job = new Job()
            {
                PositionName = jobDto.PositionName,
                Description = jobDto.Description,
                CompanyID = jobDto.CompanyID,

            };
            var jobs = await jobRepository.Create(job);
            return Ok(jobs);
        }


        [HttpPost("Konkuro")]
        public async Task<IActionResult> Konkuro(JobPostSeekerDTO model)
        {
            JobPostSeekers jobPostSeeker = new JobPostSeekers()
            {
                JobPostingID = model.JobPostingID,
                JobSeekerID = model.JobSeekerID,
            };
            await _context.JobPostSeekers.AddAsync(jobPostSeeker);
            await _context.SaveChangesAsync();
            return Ok(jobPostSeeker);
        }

        [HttpGet("GetSeekersData")]
        public async Task<IActionResult> GetSeekersData(){
               
            //qekjo i merr prej databaze komplet tabelen jobpostseekers
            //i bona include job posting edhe jobseeker se mu dasht me iteru neper qato tabela per
            //me jau marr emrin edhe kolonat tjera qa mu dashten
            var seekers =  await _context.JobPostSeekers
                .Include(x=>x.JobPosting)
                .Include(x=>x.Jobseeker)
                .ToListAsync();

            //e kom kriju ni liste te zbrazet te qesej dto qe e kom shkru qa na interesojn mu pa ne front
            List<GetJobPostSeekersDTO> seekerslist = new List<GetJobPostSeekersDTO>();

            //tash qet listen e zbrazet (DTO) e mbushi me te dhena nga lista gjenerale e jobseekers edhe marr vec
            //cka du
            seekerslist = seekers.Select(seekers => new GetJobPostSeekersDTO()
            {
                Seeker = seekers.Jobseeker.JobseekerName,
                PostTitle = seekers.JobPosting.JobPostingTitle,
                PostDescription = seekers.JobPosting.JobPostingDescription
            }).ToList();
            
            return Ok(seekerslist);
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






    }
}
