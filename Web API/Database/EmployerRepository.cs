using System;
using System.Collections.Generic;
using Npgsql;
using NpgsqlTypes;
using Web_API.Entities; // Make sure to include this namespace for the Employer model

public class EmployeRepository : BaseRepository, IEmployerRepository
{
    private const string ConnectionString = "Host=localhost:5432;Username=mylogin;Password=mypass;Database=ExamDatabase";
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
                        FirstName = data["firstname"].ToString(),
                        LastName = data["lastname"].ToString(),
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
                    FirstName = data["firstname"].ToString(),
                    LastName = data["lastname"].ToString(),
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
            cmd.CommandText = @"insert into employer(firstname, lastname, organisation, email) values(@firstname, @lastname, @organisation, @email)";

            cmd.Parameters.AddWithValue("@firstname", NpgsqlDbType.Text, e.FirstName);
            cmd.Parameters.AddWithValue("@lastname", NpgsqlDbType.Text, e.LastName);
            cmd.Parameters.AddWithValue("@organisation", NpgsqlDbType.Text, e.Organisation);
            cmd.Parameters.AddWithValue("@email", NpgsqlDbType.Text, e.Email);

            return InsertData(dbConn, cmd);
        }
    }

    // Update an employer
    public bool UpdateEmployer(Employer employer)
    {
        using (var dbConn = new NpgsqlConnection(ConnectionString))
        {
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"update employer set firstname=@firstname, lastname=@lastname, organisation=@organisation, email=@email where id=@id";

            cmd.Parameters.AddWithValue("@firstname", NpgsqlDbType.Text, e.FirstName);
            cmd.Parameters.AddWithValue("@lastname", NpgsqlDbType.Text, e.LastName);
            cmd.Parameters.AddWithValue("@organisation", NpgsqlDbType.Text, e.Organisation);
            cmd.Parameters.AddWithValue("@email", NpgsqlDbType.Text, e.Email);
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, e.Id);

            return UpdateData(dbConn, cmd);
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
