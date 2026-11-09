using BankSystem.Services.Generators;

namespace BankSystem.Services.Models.Accounts;

public class StandardAccount : BankAccount
{
    private const decimal StandardBalanceCostPerPoint = 100;

    public StandardAccount(AccountOwner accountOwner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator)
        : base(accountOwner, currencyCode, uniqueNumberGenerator)
    {
    }

    public StandardAccount(AccountOwner accountOwner, string currencyCode, Func<string> uniqueNumberGenerator) : base(
        accountOwner, currencyCode, uniqueNumberGenerator)
    {
    }

    public StandardAccount(AccountOwner accountOwner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator,
        decimal initialBalance) : base(accountOwner, currencyCode, uniqueNumberGenerator, initialBalance)
    {
    }

    public StandardAccount(AccountOwner accountOwner, string currencyCode, Func<string> uniqueNumberGenerator,
        decimal initialBalance) : base(accountOwner, currencyCode, uniqueNumberGenerator, initialBalance)
    {
    }

    protected override decimal Overdraft => 0;

    protected override int CalculateDepositRewardPoints(decimal amount)
    {
        return (int)Math.Max(Math.Floor(this.Balance / StandardBalanceCostPerPoint), 0);
    }

    protected override int CalculateWithdrawRewardPoints(decimal amount)
    {
        return (int)Math.Max(Math.Floor(this.Balance / StandardBalanceCostPerPoint), 0);
    }
}
