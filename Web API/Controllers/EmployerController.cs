using Microsoft.AspNetCore.Mvc;
using Web_API.Entities; // Make sure the Employeer class is referenced
using System.Linq;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployerController : ControllerBase
    {
        private readonly ILogger<EmployerController> _logger;
        private readonly IEmployerRepository _repository; // Assuming the repository is called EmployerRepository

        public EmployerController(ILogger<EmployerController> logger, IEmployerRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: Employer/
        [HttpGet]
        public ActionResult<List<Employer>> GetAllEmployers()
        {
            try
            {

                var employers = _repository.GetEmployers();
                return Ok(employers);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get employers: {Message}", ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: Employer/{id}
        [HttpGet("{id}")]
        public ActionResult<Employer> GetEmployerById(int id)
        {
            try
            {
                var employeer = _repository.GetEmployerById(id);
                if (employeer != null)
                {
                    return Ok(employeer);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get employer: {Message}", ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: Employer/
        [HttpPost]
        public ActionResult CreateEmployer([FromBody] Employer employer)
        {
            try
            {
                if (_repository.InsertEmployer(employer))
                {
                    return StatusCode(201, "Created");
                }
                return BadRequest("Failed to create employer");
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to create employer: {Message}", ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

    
        // PUT api/values/5
        [HttpPut()]
        public ActionResult UpdateEmployer([FromBody] Employer employer)
        {
            if (employer == null)
            {
                return BadRequest("Employer info not correct");
            }

            Employer existinEmployer = _repository.GetEmployerById(employer.Id);
            if (existinEmployer == null)
            {
                return NotFound($"Employer with id {employer.Id} not found");
            }

            bool status = _repository.UpdateEmployer(employer);
            if (status)
            {
                return Ok();
            }

            return BadRequest("Something went wrong");
        }

        // DELETE: Employer/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteEmployer(int id)
        {
            try
            {
                if (_repository.DeleteEmployer(id))
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to delete employer: {Message}", ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
