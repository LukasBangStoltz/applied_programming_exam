using Web_API.Entities;

namespace Web_API.Controllers
{
    public interface IJobRepository
    {
        object TakenJobs { get; set; }

        List<Job> GetJobs(); // Method to get all jobs
        Job GetJobById(int id); // Method to get a single job by ID
        bool InsertJob(Job job); // Method to insert a new job
        bool UpdateJob(Job job); // Method to update an existing job
        bool DeleteJob(int id); // Method to delete a job
    }
}