using System;
using System.Collections.Generic;
using Npgsql;
using NpgsqlTypes;
using Web_API.Controllers;
using Web_API.Entities; // Make sure to include this namespace for the Job model

public class JobRepository : BaseRepository, IJobRepository
{
    private const string ConnectionString = "Host=localhost:5432;Username=postgres;Password=P@r1s2027!;Database=postgres";

    public object TakenJobs { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    // Get a list of jobs
    public List<Job> GetJobs()
    {
        var jobs = new List<Job>();
        using (var dbConn = new NpgsqlConnection(ConnectionString))
        {
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "select * from job";

            var data = GetData(dbConn, cmd);

            if (data != null)
            {
                while (data.Read())
                {
                   
                    var job = new Job
                    {
                        Id = Convert.ToInt32(data["id"]),
                        Title = data["title"].ToString(),
                        Type = data["type"].ToString(),
                        Organisation = data["organisation"].ToString(),
                        Email = data["email"].ToString(),
                        EmployerId = Convert.ToInt32(data["employer_id"]),

                    };

                    jobs.Add(job);
                }

                return jobs;
            }

            return null;
        }
    }

    // Get a single job by Id
    public Job GetJobById(int id)
    {
        using (var dbConn = new NpgsqlConnection(ConnectionString))
        {
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = $"select * from job where id = {id}";

            var data = GetData(dbConn, cmd);

            if (data != null && data.Read())
            {
                var job = new Job
                {
                    Id = Convert.ToInt32(data["id"]),
                    Title = data["title"].ToString(),
                    Type = data["type"].ToString(),
                    Organisation = data["organisation"].ToString(),
                    Email = data["email"].ToString(),
                    EmployerId = Convert.ToInt32(data["employer_id"]),
                };

                return job;
            }

            return null;
        }
    }

    // Add a new job
    public bool InsertJob(Job job)
    {
        using (var dbConn = new NpgsqlConnection(ConnectionString))
        {
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"insert into job(type, title, organisation, email, employer_id) values(@type, @title, @organisation, @email, @employer_id)";

            cmd.Parameters.AddWithValue("@type", NpgsqlDbType.Text, job.Type);
            cmd.Parameters.AddWithValue("@title", NpgsqlDbType.Text, job.Title);
            cmd.Parameters.AddWithValue("@organisation", NpgsqlDbType.Text, job.Organisation);
            cmd.Parameters.AddWithValue("@email", NpgsqlDbType.Text, job.Email);
            cmd.Parameters.AddWithValue("@employer_id", NpgsqlDbType.Integer, job.EmployerId);

            return InsertData(dbConn, cmd);
        }
    }

    // Update an job
    public bool UpdateJob(Job job)
    {
        using (var dbConn = new NpgsqlConnection(ConnectionString))
        {
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"update job set type=@type, title=@title, organisation=@organisation, email=@email, employer_id=@employer_id where id=@id";

            cmd.Parameters.AddWithValue("@type", NpgsqlDbType.Text, job.Type);
            cmd.Parameters.AddWithValue("@title", NpgsqlDbType.Text, job.Title);
            cmd.Parameters.AddWithValue("@organisation", NpgsqlDbType.Text, job.Organisation);
            cmd.Parameters.AddWithValue("@email", NpgsqlDbType.Text, job.Email);
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, job.Id);
            cmd.Parameters.AddWithValue("@employer_id", NpgsqlDbType.Integer, job.EmployerId);


            return UpdateData(dbConn, cmd);
        }
    }

    // Delete an job
    public bool DeleteJob(int id)
    {
        using (var dbConn = new NpgsqlConnection(ConnectionString))
        {
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"delete from job where id = @id";
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);

            return DeleteData(dbConn, cmd);
        }
    }
}
