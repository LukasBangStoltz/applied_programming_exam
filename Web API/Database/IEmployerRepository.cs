using System.Collections.Generic;
using Web_API.Entities;

public interface IEmployerRepository
{
    List<Employer> GetEmployers(); // Method to get all employers
    Employer GetEmployerById(int id); // Method to get a single employer by ID
    bool InsertEmployer(Employer employer); // Method to insert a new employer
    bool UpdateEmployer(Employer employer); // Method to update an existing employer
    bool DeleteEmployer(int id); // Method to delete an employer
}
