using System;
using System.Collections.Generic;
using System.Diagnostics;
using Npgsql;
using NpgsqlTypes;
using Web_API.Controllers;
using Web_API.Entities; // Make sure to include this namespace for the TakenJob model
using static Web_API.Entities.TakenJob;

public class TakenJobRepository : BaseRepository, ITakenJobRepository
{
    private const string ConnectionString = "Host=localhost:5432;Username=postgres;Password=P@r1s2027!;Database=postgres";

    public List<TakenJob> GetTakenJob()
    {
        throw new NotImplementedException();
    }

    // Get a list of takenjobs
    public List<TakenJob> GetTakenJobs()
    {
        var takenjobs = new List<TakenJob>();
        using (var dbConn = new NpgsqlConnection(ConnectionString))
        {
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "select * from takenjob";

            var data = GetData(dbConn, cmd);

            if (data != null)
            {
                while (data.Read())
                {
                   
                    var takenjob = new TakenJob
                    {
                        VolunteerId = Convert.ToInt32(data["volunteerid"]),
                        JobId = Convert.ToInt32(data["jobid"]),
                    };

                    takenjobs.Add(takenjob);
                }

                return takenjobs;
            }

            return null;
        }
    }

  

    // Add a new takenjob
    public bool InsertTakenJob(TakenJob takenjob)
    {
        using (var dbConn = new NpgsqlConnection(ConnectionString))
        {
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"insert into takenjob(volunteerid, jobid) values(@volunteerid, @jobid)";

            cmd.Parameters.AddWithValue("@volunteerid", NpgsqlDbType.Text, takenjob.VolunteerId);
            cmd.Parameters.AddWithValue("@jobid", NpgsqlDbType.Text, takenjob.JobId);


            return InsertData(dbConn, cmd);
        }
    }

    public bool TakeJob(int volunteerId, int jobId)
    {
        using (var dbConn = new NpgsqlConnection(ConnectionString))
        {
            // Open the connection
            dbConn.Open();

            // Check if the job is already taken
            var checkCmd = dbConn.CreateCommand();
            checkCmd.CommandText = $"SELECT 1 FROM takenjobs WHERE job_id = {jobId}";
            var exists = checkCmd.ExecuteScalar() != null;

            if (!exists)
            {
                // If not taken, insert the new job assignment
                var cmd = dbConn.CreateCommand();
                cmd.CommandText = $"INSERT INTO takenjobs (volunteer_id, job_id) VALUES ({volunteerId}, {jobId})";
                cmd.ExecuteNonQuery();
                return true; // Job was successfully taken
            }
            return false; // Job is already taken, no insertion done
        }
    }
}
