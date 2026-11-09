using BankSystem.Services.Generators;

namespace BankSystem.Services.Models.Accounts;

public class SilverAccount : BankAccount
{
    private const decimal SilverDepositCostPerPoint = 5;
    private const decimal SilverWithdrawCostPerPoint = 2;
    private const decimal SilverBalanceCostPerPoint = 100;

    public SilverAccount(AccountOwner accountOwner,
        string currencyCode,
        IUniqueNumberGenerator uniqueNumberGenerator) : base(accountOwner, currencyCode, uniqueNumberGenerator)
    {
    }

    public SilverAccount(AccountOwner accountOwner, string currencyCode, Func<string> uniqueNumberGenerator) : base(
        accountOwner, currencyCode, uniqueNumberGenerator)
    {
    }

    public SilverAccount(AccountOwner accountOwner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator,
        decimal initialBalance) : base(accountOwner, currencyCode, uniqueNumberGenerator, initialBalance)
    {
    }

    public SilverAccount(AccountOwner accountOwner, string currencyCode, Func<string> uniqueNumberGenerator,
        decimal initialBalance) : base(accountOwner, currencyCode, uniqueNumberGenerator, initialBalance)
    {
    }

    protected override decimal Overdraft => this.BonusPoints * 2;

    protected override int CalculateDepositRewardPoints(decimal amount)
    {
        var points = (this.Balance / SilverBalanceCostPerPoint) + (amount / SilverDepositCostPerPoint);

        return (int)Math.Max(Math.Floor(points), 0);
    }

    protected override int CalculateWithdrawRewardPoints(decimal amount)
    {
        var points = (this.Balance / SilverBalanceCostPerPoint) + (amount / SilverWithdrawCostPerPoint);

        return (int)Math.Max(Math.Floor(points), 0);
    }
}
