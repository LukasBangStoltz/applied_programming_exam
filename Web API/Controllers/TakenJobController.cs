using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web_API.Entities;
using static Web_API.Entities.TakenJob;


namespace Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TakenJobController : ControllerBase
    {
        private readonly ILogger<TakenJobController> _logger;
        private readonly TakenJobRepository _repository; // Assuming the repository is called TakenJobRepository

        public TakenJobController(ILogger<TakenJobController> logger, TakenJobRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: Job/
        [HttpGet]
        public ActionResult<List<TakenJob>> GetAllTakenJobs()
        {
            try
            {

                var takenjobs = _repository.GetTakenJobs();
                return Ok(takenjobs);
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get taken jobs: {Message}", ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }






    }
}
