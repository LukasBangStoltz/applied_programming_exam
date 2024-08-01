using Microsoft.AspNetCore.Mvc;
using Web_API.Entities; // Make sure the Job class is referenced
using System.Linq;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private readonly ILogger<JobController> _logger;
        private readonly IJobRepository _repository; // Assuming the repository is called JobRepository

        public JobController(ILogger<JobController> logger, IJobRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: Job/
        [HttpGet]
        public ActionResult<List<Job>> GetAllJobs()
        {
            try
            {

                var jobs = _repository.GetJobs();
                return Ok(jobs);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get jobs: {Message}", ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: Job/{id}
        [HttpGet("{id}")]
        public ActionResult<Job> GetJobById(int id)
        {
            try
            {
                var job = _repository.GetJobById(id);
                if (job != null)
                {
                    return Ok(job);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get job: {Message}", ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: Job/
        [HttpPost]
        public ActionResult CreateJob([FromBody] Job job)
        {
            try
            {
                if (_repository.InsertJob(job))
                {
                    return StatusCode(201, new { message = "Created" });
                }
                return BadRequest("Failed to create employeer");
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to create employeer: {Message}", ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: Job/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateJob(int id, [FromBody] Job job)
        {
            if (job.Id != id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                if (_repository.UpdateJob(job))
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to update job: {Message}", ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: Job/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteJob(int id)
        {
            try
            {
                if (_repository.DeleteJob(id))
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to delete job: {Message}", ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

     
    }
}
