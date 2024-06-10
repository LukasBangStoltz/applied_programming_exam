using Web_API.Entities;

namespace Web_API.Controllers
{
    public interface IVolunteerRepository
    {
        List<Volunteer> GetVolunteers(); // Method to get all employers
        Employer GetVolunteerById(int id); // Method to get a single employer by ID
        bool InsertVolunteer(Volunteer volunteer); // Method to insert a new employer
        bool UpdateVolunteer(Volunteer volunteer); // Method to update an existing employer
        bool DeleteVolunteer(int id); // Method to delete an employer
    }
}
