using System.Globalization;
using BankSystem.Services.Generators;
using BankSystem.Services.Helpers;

namespace BankSystem.Services.Models.Accounts;

public abstract class BankAccount
{
    private readonly List<AccountCashOperation> operations = new();

    public string Number { get; set; }
    public decimal Balance { get; set; }
    public string CurrencyCode { get; set; }
    public AccountOwner AccountOwner { get; set; }
    public int BonusPoints { get; set; }
    protected abstract decimal Overdraft { get; }

    protected BankAccount(AccountOwner owner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator)
        : this(owner, currencyCode, ToNumberGenerator(uniqueNumberGenerator))
    {
    }

    protected BankAccount(AccountOwner accountOwner, string currencyCode, Func<string> uniqueNumberGenerator)
    {
        ArgumentNullException.ThrowIfNull(accountOwner);
        ArgumentNullException.ThrowIfNull(uniqueNumberGenerator);

        if (string.IsNullOrWhiteSpace(currencyCode) || !currencyCode.IsCurrencyValid())
        {
            throw new ArgumentException("Currency code is not a valid ISO currency code.", nameof(currencyCode));
        }

        this.AccountOwner = accountOwner;
        this.CurrencyCode = currencyCode;
        this.Number = uniqueNumberGenerator();

        accountOwner.Add(this);
    }

    protected BankAccount(AccountOwner accountOwner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator,
        decimal initialBalance) : this(accountOwner, currencyCode, uniqueNumberGenerator)
    {
        this.Deposit(initialBalance, DateTime.Now, "Initial balance");
    }

    protected BankAccount(AccountOwner accountOwner, string currencyCode, Func<string> uniqueNumberGenerator,
        decimal initialBalance) : this(accountOwner, currencyCode, uniqueNumberGenerator)
    {
        this.Deposit(initialBalance, DateTime.Now, "Initial balance");
    }

    public void Deposit(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Deposit amount must be positive.");
        }

        this.Balance += amount;
        this.operations.Add(new AccountCashOperation(amount, date, note));
        this.BonusPoints += this.CalculateDepositRewardPoints(amount);
    }

    public void Withdraw(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Withdrawal amount must be positive.");
        }

        if (amount > this.Balance + this.Overdraft)
        {
            throw new InvalidOperationException("Withdrawal amount exceeds balance and overdraft limit.");
        }

        this.Balance -= amount;
        this.operations.Add(new AccountCashOperation(-amount, date, note));
        this.BonusPoints += this.CalculateWithdrawRewardPoints(amount);
    }

    public IList<AccountCashOperation> GetAllOperations()
    {
        return this.operations.ToList();
    }

    public override string ToString()
    {
        return string.Create(
            CultureInfo.InvariantCulture,
            $"{this.Overdraft} {this.AccountOwner} No:{this.Number}. Balance: {this.Balance}{this.CurrencyCode}.\n");
    }

    protected abstract int CalculateDepositRewardPoints(decimal amount);

    protected abstract int CalculateWithdrawRewardPoints(decimal amount);

    private static Func<string> ToNumberGenerator(IUniqueNumberGenerator uniqueNumberGenerator)
    {
        ArgumentNullException.ThrowIfNull(uniqueNumberGenerator);

        return uniqueNumberGenerator.Generate;
    }
}
