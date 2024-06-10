using Web_API.Entities;
using static Web_API.Entities.TakenJob;

namespace Web_API.Controllers
{
    public interface ITakenJobRepository
    {
        List<TakenJob> GetTakenJob(); // Method to get all taken jobs
        bool InsertTakenJob(TakenJob takenjob); // Method to insert a taken job

    }
}