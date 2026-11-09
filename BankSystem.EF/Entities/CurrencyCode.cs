using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankSystem.EF.Entities;

[Table("currency_code")]
public class CurrencyCode
{
    [Key] [Column("currency_code_id")] public int Id { get; set; }

    [Column("currency_code")]
    [StringLength(5)]
    public string CurrenciesCode { get; set; } = string.Empty;

    public IList<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();
}
