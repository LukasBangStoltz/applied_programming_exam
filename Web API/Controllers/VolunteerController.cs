using Microsoft.AspNetCore.Mvc;
using Web_API.Entities; // Make sure the Volunteer class is referenced
using System.Linq;


namespace Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VolunteerController : ControllerBase
    {
        private readonly ILogger<VolunteerController> _logger;
        private readonly IVolunteerRepository _repository; // Assuming the repository is called VolunteerRepository

        public VolunteerController(ILogger<VolunteerController> logger, IVolunteerRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: Volunteers/
        [HttpGet]
        public ActionResult<List<Volunteer>> GetAllVolunteers()
        {
            try
            {

                var volunteers = _repository.GetVolunteers();
                return Ok(volunteers);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get volunteers: {Message}", ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: Volunteer/{id}
        [HttpGet("{id}")]
        public ActionResult<Volunteer> GetVolunteerById(int id)
        {
            try
            {
                var volunteer = _repository.GetVolunteerById(id);
                if (volunteer != null)
                {
                    return Ok(volunteer);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get volunteer: {Message}", ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        // POST: Volunteer/
        [HttpPost]
        public ActionResult CreateVolunteer([FromBody] Volunteer volunteer)
        {
            try
            {
                if (_repository.InsertVolunteer(volunteer))
                {
                    return StatusCode(201, "Created");
                }
                return BadRequest("Failed to create volunteer");
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to create volunteer: {Message}", ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT: Volunteer/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateVolunteer(int id, [FromBody] Volunteer volunteer)
        {
            if (volunteer.Id != id)
            {
                return BadRequest("ID mismatch");
            }

            try
            {
                if (_repository.UpdateVolunteer(volunteer))
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to update volunteer: {Message}", ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: Volunteer/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteVolunteer(int id)
        {
            try
            {
                if (_repository.DeleteVolunteer(id))
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to delete volunteer: {Message}", ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
