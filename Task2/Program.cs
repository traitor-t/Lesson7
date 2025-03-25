using System;
using System.Data.SqlClient;

public class DatabaseConnectException : Exception
{
    public DatabaseConnectException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

public class DatabaseConnector
{
    public void Connect(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new ArgumentException("Строка подключения не может быть пустой", nameof(connectionString));
        }

        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Подключение успешно установлено");
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Ошибка подключения к базе данных: {ex.Message}");
            throw; 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Неожиданная ошибка: {ex.Message}");
            throw;
        }
    }
}

public class DatabaseManager
{
    private readonly DatabaseConnector _connector;

    public DatabaseManager(DatabaseConnector connector)
    {
        _connector = connector ?? throw new ArgumentNullException(nameof(connector));
    }

    public void EstablishConnection(string connectionString)
    {
        try
        {
            _connector.Connect(connectionString);
        }
        catch (SqlException ex)
        {
            throw new DatabaseConnectException(
                $"Не удалось установить подключение к базе данных. Проверьте параметры подключения. Ошибка: {ex.Message}",
                ex);
        }
        catch (ArgumentException ex)
        {
            throw new DatabaseConnectException(
                $"Некорректные параметры подключения: {ex.Message}",
                ex);
        }
    }
}

class Program
{
    static void Main()
    {
        var connector = new DatabaseConnector();
        var manager = new DatabaseManager(connector);

        string invalidConnectionString = "Server=invalid;Database=test;Uid=root;";

        try
        {
            manager.EstablishConnection(invalidConnectionString);
        }
        catch (DatabaseConnectException ex)
        {
            Console.WriteLine($"Ошибка подключения к БД: {ex.Message}");
            Console.WriteLine($"Подробности: {ex.InnerException?.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Неожиданная ошибка: {ex.Message}");
        }
    }
}