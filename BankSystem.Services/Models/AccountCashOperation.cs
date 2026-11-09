using System.Globalization;

namespace BankSystem.Services.Models;

public class AccountCashOperation(decimal amount, DateTime date, string note)
{
    public decimal Amount { get; set; } = amount;
    public DateTime Date { get; set; } = date;
    public string Note { get; set; } = note;

    public override string ToString()
    {
        string action = this.Amount < 0 ? "Debited from" : "Credited to";
        return string.Create(
            CultureInfo.InvariantCulture,
            $"{this.Date:MM/dd/yyyy HH:mm:ss} {this.Note} : {action} account {this.Amount}.");
    }
}
