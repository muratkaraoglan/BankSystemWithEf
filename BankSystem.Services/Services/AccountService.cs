using BankSystem.EF.Entities;
using BankSystem.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Services.Services;

public class AccountService : BaseService
{
    public AccountService(BankContext bankContext) : base(bankContext)
    {
    }

    public IReadOnlyList<BankAccountFullInfoModel> GetBankAccountsFullInfo()
    {
        return this.Context
            .BankAccounts
            .AsNoTracking()
            .OrderBy(a => a.AccountOwnerId)
            .Select(a => new BankAccountFullInfoModel()
            {
                BankAccountId = a.Id,
                FirstName = a.AccountOwner.FirstName,
                LastName = a.AccountOwner.LastName,
                AccountNumber = a.Number,
                Balance = a.Balance,
                BonusPoints = a.BonusPoints,
                CurrencyCode = a.CurrencyCode.CurrenciesCode
            }).ToList();
    }
}
