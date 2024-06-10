using System;
using System.Collections.Generic;
using Npgsql;
using NpgsqlTypes;
using Web_API.Controllers;
using Web_API.Entities; // Correctly included for the Volunteer model

public class VolunteerRepository : BaseRepository, IVolunteerRepository
{
    private const string ConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=Liverpool999;Database=ExamDatabase";

    // Get a list of volunteers
    public List<Volunteer> GetVolunteers()
    {
        var volunteers = new List<Volunteer>();
        using (var dbConn = new NpgsqlConnection(ConnectionString))
        {
            dbConn.Open();
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "SELECT * FROM volunteer";

            using (var data = cmd.ExecuteReader())
            {
                while (data.Read())
                {
                    var volunteer = new Volunteer
                    {
                        Id = Convert.ToInt32(data["id"]),
                        FirstName = data["first_name"].ToString(),
                        LastName = data["last_name"].ToString(),
                        Email = data["email"].ToString(),
                        Age = Convert.ToInt32(data["age"]),
                        FieldOfInterest = data["field_of_interest"].ToString()


                    };

                    volunteers.Add(volunteer);
                }
            }
        }

        return volunteers;
    }

    // Get a single volunteer by Id
    public Volunteer GetVolunteerById(int id)
    {
        using (var dbConn = new NpgsqlConnection(ConnectionString))
        {
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = $"select * from volunteer where id = {id}";

            var data = GetData(dbConn, cmd);

            if (data != null && data.Read())
            {
                var volunteer = new Volunteer
                {
                    Id = Convert.ToInt32(data["id"]),
                    FirstName = data["first_name"].ToString(),
                    LastName = data["last_name"].ToString(),
                    Email = data["email"].ToString(),
                    Age = Convert.ToInt32(data["age"]),
                    FieldOfInterest = data["field_of_interest"].ToString()
                };

                return volunteer;
            }

            return null;
        }
    }

    // Add a new volunteer
    public bool InsertVolunteer(Volunteer volunteer)
    {
        using (var dbConn = new NpgsqlConnection(ConnectionString))
        {
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"INSERT INTO volunteer(first_name, last_name, email, age, field_of_interest) VALUES(@firstname, @lastname, @email, @age, @fieldofinterest)";
            cmd.Parameters.AddWithValue("@firstname", NpgsqlDbType.Text, volunteer.FirstName);
            cmd.Parameters.AddWithValue("@lastname", NpgsqlDbType.Text, volunteer.LastName);
            cmd.Parameters.AddWithValue("@email", NpgsqlDbType.Text, volunteer.Email);
            cmd.Parameters.AddWithValue("@age", NpgsqlDbType.Integer, volunteer.Age);
            cmd.Parameters.AddWithValue("@fieldofinterest", NpgsqlDbType.Text, volunteer.FieldOfInterest);

            return InsertData(dbConn, cmd);

        }
    }

    // Update a volunteer
    public bool UpdateVolunteer(Volunteer volunteer)
    {
        using (var dbConn = new NpgsqlConnection(ConnectionString))
        {
            dbConn.Open();
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "UPDATE volunteer SET first_name = @firstname, last_name = @lastname, email = @email, age = @age, field_of_interest = @field_of_interest WHERE id = @id";
            cmd.Parameters.AddWithValue("@firstname", NpgsqlDbType.Text, volunteer.FirstName);
            cmd.Parameters.AddWithValue("@lastname", NpgsqlDbType.Text, volunteer.LastName);
            cmd.Parameters.AddWithValue("@email", NpgsqlDbType.Text, volunteer.Email);
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, volunteer.Id);
            cmd.Parameters.AddWithValue("@age", NpgsqlDbType.Text, volunteer.Age);
            cmd.Parameters.AddWithValue("@field_of_interest", NpgsqlDbType.Text, volunteer.FieldOfInterest);

            return UpdateData(dbConn, cmd);
        }
    }

    // Delete a volunteer
    public bool DeleteVolunteer(int id)
    {
        using (var dbConn = new NpgsqlConnection(ConnectionString))
        {
            dbConn.Open();
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "DELETE FROM volunteer WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);

            return cmd.ExecuteNonQuery() == 1;
        }
    }

    Employer IVolunteerRepository.GetVolunteerById(int id)
    {
        throw new NotImplementedException();
    }
}