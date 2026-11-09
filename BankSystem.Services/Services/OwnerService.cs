using BankSystem.EF.Entities;
using BankSystem.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Services.Services;

public class OwnerService : BaseService
{
    public OwnerService(BankContext bankContext) : base(bankContext)
    {
    }

    public IReadOnlyList<AccountOwnerTotalBalanceModel> GetAccountOwnersTotalBalance()
    {
        return this.Context.BankAccounts
            .AsNoTracking()
            .GroupBy(a => new
            {
                a.AccountOwnerId,
                a.AccountOwner.FirstName,
                a.AccountOwner.LastName,
                CurrencyCode = a.CurrencyCode.CurrenciesCode
            })
            .Select(g => new AccountOwnerTotalBalanceModel
            {
                AccountOwnerId = g.Key.AccountOwnerId,
                FirstName = g.Key.FirstName,
                LastName = g.Key.LastName,
                CurrencyCode = g.Key.CurrencyCode,
                Total = g.Sum(a => a.Balance)
            })
            .OrderByDescending(m => m.Total)
            .ToList();
    }
}
