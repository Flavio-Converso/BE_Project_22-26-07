using System.Data.SqlClient;

public abstract class BaseService
{
    protected readonly string _connectionString;

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
    protected List<T> ExecuteReader<T>(string commandText, Action<SqlCommand> parameterAction, Func<SqlDataReader, T>? readAction)
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

    // async
    protected async Task<List<T>> ExecuteReaderAsync<T>(string commandText, Action<SqlCommand> parameterAction, Func<SqlDataReader, T> readAction)
    {
        var result = new List<T>();
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand(commandText, connection))
            {
                parameterAction?.Invoke(command);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        result.Add(readAction(reader));
                    }
                }
            }
        }
        return result;
    }

    protected void ExecuteNonQuery(string commandText, Action<SqlCommand> parameterAction = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using (var command = new SqlCommand(commandText, connection))
            {
                parameterAction?.Invoke(command);
                command.ExecuteNonQuery();
            }
        }
    }

    protected async Task<T> ExecuteScalarAsync<T>(string commandText, Action<SqlCommand> parameterAction = null)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            using (var command = new SqlCommand(commandText, connection))
            {
                parameterAction?.Invoke(command);
                return (T)await command.ExecuteScalarAsync();
            }
        }
    }

}
