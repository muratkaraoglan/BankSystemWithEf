using System.Globalization;
using System.Net.Mail;

namespace BankSystem.Services.Helpers;

public static class ValidationService
{
    private static readonly Lazy<HashSet<string>> KnownCurrencyCodes = new(LoadCurrencyCodes);

    /// <summary>
    /// Checks whether the specified value is an existing ISO currency code (e.g. "USD").
    /// </summary>
    public static bool IsCurrencyValid(this string currencyCode)
    {
        if (string.IsNullOrWhiteSpace(currencyCode))
        {
            return false;
        }

        return KnownCurrencyCodes.Value.Contains(currencyCode);
    }

    /// <summary>
    /// Checks whether the specified value is a correctly formatted email address.
    /// </summary>
    public static bool IsEmailValid(this string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        try
        {
            var address = new MailAddress(email);
            return address.Address == email;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    private static HashSet<string> LoadCurrencyCodes()
    {
        var codes = new HashSet<string>(StringComparer.Ordinal);

        foreach (CultureInfo culture in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
        {
            var region = new RegionInfo(culture.Name);
            codes.Add(region.ISOCurrencySymbol);
        }

        return codes;
    }
}
