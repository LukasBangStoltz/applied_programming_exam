using System;
using System.Collections.Generic;
using Npgsql;
using NpgsqlTypes;
using Web_API.Entities; // Make sure to include this namespace for the Employer model

public class EmployeRepository : BaseRepository
{
    private const string ConnectionString = "Host=localhost:5432;Username=mylogin;Password=mypass;Database=ExamDatabase";
    // Get a list of employers
    public List<Employer> Getemployers()
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
    public Employer GetemployerById(int id)
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
    public bool Insertemployer(Employer e)
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
    public bool Updateemployer(Employer e)
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
    public bool Deleteemployer(int id)
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
