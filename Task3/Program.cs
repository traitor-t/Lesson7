public class EmptyStringException : Exception
{
    public EmptyStringException() : base("Входная строка не может быть пустой или null") { }
    public EmptyStringException(string message) : base(message) { }
}

public class StringProcessor
{
    public void ValidateInput(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new EmptyStringException("Проверка строки: строка пуста или равна null");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        StringProcessor processor = new StringProcessor();
        string input = null;

        try
        {
            processor.ValidateInput(input);
            Console.WriteLine("Строка успешно прошла проверку");
        }

        catch (EmptyStringException ex)
        {
            Console.WriteLine("Ошибка: " +ex.Message);
        }
    }
}