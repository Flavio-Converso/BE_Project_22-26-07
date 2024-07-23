using System.Data.SqlClient;

public abstract class BaseService
{
    private readonly string _connectionString;

    protected BaseService(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected T ExecuteScalar<T>(string commandText, Action<SqlCommand> parameterAction = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(commandText, connection))
            {
                parameterAction?.Invoke(command);
                return (T)command.ExecuteScalar();
            }
        }
    }

    protected List<T> ExecuteReader<T>(string commandText, Action<SqlCommand> parameterAction, Func<SqlDataReader, T> readAction)
    {
        var result = new List<T>();
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(commandText, connection))
            {
                parameterAction?.Invoke(command); 
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(readAction(reader));
                    }
                }
            }
        }
        return result;
    }
}