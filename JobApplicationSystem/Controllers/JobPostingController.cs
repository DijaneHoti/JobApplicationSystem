using JobApplicationSystem.Data;
using JobApplicationSystem.Models.Dto;
using JobApplicationSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JobApplicationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPostingController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private ApiResponse _response;

        public JobPostingController(ApplicationDbContext db)
        {
            _db = db;
            _response = new ApiResponse();
        }



        [HttpGet]
        public async Task<IActionResult> GetJobPostings()
        {
            _response.Result = _db.JobPostings;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetJobPosting(int id)
        {
            if (id == 0)
            {

                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            JobPosting jobPosting = _db.JobPostings.FirstOrDefault(u => u.JobPostingID == id);

            if (jobPosting == null)
            {

                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }

            _response.Result = jobPosting;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);


        }


        //CREATE

        [HttpPost]

        public async Task<ActionResult<ApiResponse>> CreateJobPosting([FromForm] JobPostingCreateDTO jobPostingCreateDTO)
        {
            try
            {


                JobPosting jobPosting = new()
                {
                    JobID = jobPostingCreateDTO.JobID,
                    JobPostingTitle = jobPostingCreateDTO.JobPostingTitle,
                    JobPostingDescription = jobPostingCreateDTO.JobPostingDescription

                };

                if (ModelState.IsValid)
                {
                    _db.JobPostings.Add(jobPosting);
                    _db.SaveChanges();
                    _response.Result = jobPosting;
                    _response.StatusCode = HttpStatusCode.Created;
                    return Ok(_response);

                }
            }

            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }
            return _response;



        }



        //UPDATE 

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse>> UpdateJobPosting(int id, [FromForm] JobPostingUpdateDTO jobPostingUpdateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = ModelState.Values
                        .SelectMany(x => x.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    return BadRequest(_response);
                }

                JobPosting jobPosting = _db.JobPostings.FirstOrDefault(u => u.JobPostingID == id);

                if (jobPosting == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }


                var jobPostingExists = _db.JobPostings.Any(a => a.JobPostingID == jobPostingUpdateDTO.JobPostingID);
                if (!jobPostingExists)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string> { "JobPostingID nuk ekziston." };
                    return BadRequest(_response);
                }


                jobPosting.JobPostingTitle = jobPostingUpdateDTO.JobPostingTitle;
                jobPosting.JobPostingDescription = jobPostingUpdateDTO.JobPostingDescription;
                jobPosting.JobID = jobPostingUpdateDTO.JobID;

                _db.SaveChanges();
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
                // Log the exception for debugging
                Console.WriteLine(ex.ToString());
            }
            return _response;
        }





        // te delete po ma qet 204 edhe 200 response nSwagger amo kshtu pe fshin mire

        [HttpDelete("{id:int}")]


        public async Task<IActionResult> DeleteJobPosting(int id)
        {


            if (id == 0)
            {
                // if the item is wrong
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            JobPosting jobPosting = _db.JobPostings.FirstOrDefault(u => u.JobPostingID == id);


            if (jobPosting == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                return NotFound(_response);
            }

            _db.JobPostings.Remove(jobPosting);
            await _db.SaveChangesAsync();

            _response.StatusCode = HttpStatusCode.NoContent;
            return NoContent();


        }
    }
}
