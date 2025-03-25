public class InsufficientFundsException : Exception
{
    public InsufficientFundsException() : base("Недостаточно средств на счете") { }

    public InsufficientFundsException(string message) : base(message) { }
}

public class BankAccount
{
    private decimal balance {  get; set; }
    public BankAccount(decimal initialbalance)
    {
        balance = initialbalance;
    }

    public decimal Balance
    {
        get { return balance; }
    }

    public void Withdraw(decimal amount)
    {
        if (amount > balance)
        {
            throw new InsufficientFundsException("Попытка снять: " + amount + ". Доступно: " + balance);
        }
        balance -= amount;
    }
}

class Program
{
    static void Main(string[] args)
    {
        BankAccount account = new BankAccount(100.00m);
        try
        {
            account.Withdraw(150.00m);
        }
        catch (InsufficientFundsException ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.WriteLine("Баланс на счете: " + account.Balance);
    }
}