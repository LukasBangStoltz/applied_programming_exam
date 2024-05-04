using Microsoft.AspNetCore.Mvc;
using Web_API.Entities; // Make sure the Employeer class is referenced
using System.Linq;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeerController : ControllerBase
    {
        private readonly ILogger<EmployeerController> _logger;
        private readonly IEmployerRepository _repository; // Assuming the repository is called EmployerRepository

        public EmployeerController(ILogger<EmployeerController> logger, IEmployerRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: Employeer/
        [HttpGet]
        public ActionResult<List<Employer>> GetAllEmployers()
        {
            try
            {
                var employeers = _repository.GetEmployers();
                return Ok(employeers);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get employeers: {Message}", ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: Employeer/{id}
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
                _logger.LogError("Failed to get employeer: {Message}", ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: Employeer/
        [HttpPost]
        public ActionResult CreateEmployer([FromBody] Employer employer)
        {
            try
            {
                if (_repository.InsertEmployer(employer))
                {
                    return StatusCode(201, "Created");
                }
                return BadRequest("Failed to create employeer");
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to create employeer: {Message}", ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: Employeer/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateEmployer(int id, [FromBody] Employer employer)
        {
            if (employer.Id != id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                if (_repository.UpdateEmployer(employer))
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to update employer: {Message}", ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: Employeer/{id}
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
