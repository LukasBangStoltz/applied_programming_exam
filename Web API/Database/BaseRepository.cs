using Npgsql;

public class BaseRepository
{
    protected const string ConnectionString = "Host=localhost:5432;Username=postgres;Password=P@r1s2027!;Database=postgres";

    protected NpgsqlDataReader? GetData(NpgsqlConnection conn, NpgsqlCommand cmd)
    {
        conn.Open();
        return cmd.ExecuteReader();
    }

    protected bool InsertData(NpgsqlConnection conn, NpgsqlCommand cmd)
    {
        conn.Open();
        cmd.ExecuteNonQuery();
        return true;
    }

    protected bool UpdateData(NpgsqlConnection conn, NpgsqlCommand cmd)
    {
        conn.Open();
        cmd.ExecuteNonQuery();
        return true;
    }

    protected bool DeleteData(NpgsqlConnection conn, NpgsqlCommand cmd)
    {
        conn.Open();
        var result = cmd.ExecuteNonQuery();

        if (result > 0)
        {
            return true;
        }
        return false;
    }
}