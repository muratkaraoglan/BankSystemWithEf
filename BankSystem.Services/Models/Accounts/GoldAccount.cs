using BankSystem.Services.Generators;

namespace BankSystem.Services.Models.Accounts;

public class GoldAccount : BankAccount
{
    private const decimal GoldDepositCostPerPoint = 10;
    private const decimal GoldWithdrawCostPerPoint = 5;
    private const decimal GoldBalanceCostPerPoint = 5;

    public GoldAccount(AccountOwner accountOwner,
        string currencyCode,
        IUniqueNumberGenerator uniqueNumberGenerator) : base(accountOwner, currencyCode, uniqueNumberGenerator)
    {
    }

    public GoldAccount(AccountOwner accountOwner, string currencyCode, Func<string> uniqueNumberGenerator) : base(
        accountOwner, currencyCode, uniqueNumberGenerator)
    {
    }

    public GoldAccount(AccountOwner accountOwner, string currencyCode, IUniqueNumberGenerator uniqueNumberGenerator,
        decimal initialBalance) : base(accountOwner, currencyCode, uniqueNumberGenerator, initialBalance)
    {
    }

    public GoldAccount(AccountOwner accountOwner, string currencyCode, Func<string> uniqueNumberGenerator,
        decimal initialBalance) : base(accountOwner, currencyCode, uniqueNumberGenerator, initialBalance)
    {
    }

    protected override decimal Overdraft => this.BonusPoints * 3;

    protected override int CalculateDepositRewardPoints(decimal amount)
    {
        var points = (this.Balance / GoldBalanceCostPerPoint) + (amount / GoldDepositCostPerPoint);

        return (int)Math.Max(Math.Ceiling(points), 0);
    }

    protected override int CalculateWithdrawRewardPoints(decimal amount)
    {
        var points = (this.Balance / GoldBalanceCostPerPoint) + (amount / GoldWithdrawCostPerPoint);

        return (int)Math.Max(Math.Ceiling(points), 0);
    }
}
