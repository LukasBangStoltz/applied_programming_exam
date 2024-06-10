using System;
using System.Collections.Generic;
using Npgsql;
using NpgsqlTypes;
using Web_API.Entities; // Make sure to include this namespace for the Employer model

public class EmployerRepository : BaseRepository, IEmployerRepository
{
    private const string ConnectionString = "Host=localhost:5432;Username=postgres;Password=Liverpool999;Database=ExamDatabase";
    // Get a list of employers
    public List<Employer> GetEmployers()
    {
        var employers = new List<Employer>();
        using (var dbConn = new NpgsqlConnection(ConnectionString))
        {
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "select * from employer";

            var data = GetData(dbConn, cmd);

            if (data != null)
            {
                while (data.Read())
                {
                   
                    var employer = new Employer
                    { 
                        Id = Convert.ToInt32(data["id"]),
                        FirstName = data["first_name"].ToString(),
                        LastName = data["last_name"].ToString(),
                        Organisation = data["organisation"].ToString(),
                        Email = data["email"].ToString()
                    };

                    employers.Add(employer);
                }

                return employers;
            }

            return null;
        }
    }

    // Get a single employer by Id
    public Employer GetEmployerById(int id)
    {
        using (var dbConn = new NpgsqlConnection(ConnectionString))
        {
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = $"select * from employer where id = {id}";

            var data = GetData(dbConn, cmd);

            if (data != null && data.Read())
            {
                var employer = new Employer
                {
                    Id = Convert.ToInt32(data["id"]),
                    FirstName = data["first_name"].ToString(),
                    LastName = data["last_name"].ToString(),
                    Organisation = data["organisation"].ToString(),
                    Email = data["email"].ToString()
                };

                return employer;
            }

            return null;
        }
    }

    // Add a new employer
    public bool InsertEmployer(Employer employer)
    {
        using (var dbConn = new NpgsqlConnection(ConnectionString))
        {
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"insert into employer(first_name, last_name, organisation, email) values(@first_name, @last_name, @organisation, @email)";

            cmd.Parameters.AddWithValue("@first_name", NpgsqlDbType.Text, employer.FirstName);
            cmd.Parameters.AddWithValue("@last_name", NpgsqlDbType.Text, employer.LastName);
            cmd.Parameters.AddWithValue("@organisation", NpgsqlDbType.Text, employer.Organisation);
            cmd.Parameters.AddWithValue("@email", NpgsqlDbType.Text, employer.Email);

            return InsertData(dbConn, cmd);
        }
    }

    // Update an employer
    public bool UpdateEmployer(Employer employer)
    {
        using (var dbConn = new NpgsqlConnection(ConnectionString))
        {
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"update employer set first_name=@first_name, last_name=@last_name, organisation=@organisation, email=@email where id=@id";

            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, employer.Id);
            cmd.Parameters.AddWithValue("@first_name", NpgsqlDbType.Text, employer.FirstName);
            cmd.Parameters.AddWithValue("@last_name", NpgsqlDbType.Text, employer.LastName);
            cmd.Parameters.AddWithValue("@organisation", NpgsqlDbType.Text, employer.Organisation);
            cmd.Parameters.AddWithValue("@email", NpgsqlDbType.Text, employer.Email);

            bool result = UpdateData(dbConn, cmd);
            return result;
        }
    }

    // Delete an employer
    public bool DeleteEmployer(int id)
    {
        using (var dbConn = new NpgsqlConnection(ConnectionString))
        {
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"delete from employer where id = @id";
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);

            return DeleteData(dbConn, cmd);
        }
    }
}
