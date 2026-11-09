using System.Globalization;
using BankSystem.Services.Helpers;

namespace BankSystem.Services.Generators;

/// <summary>
/// Generates unique numbers based on the ticks elapsed since a given starting point.
/// </summary>
public class BasedOnTickUniqueNumberGenerator(DateTime startingPoint) : IUniqueNumberGenerator
{
    public string Generate()
    {
        long elapsedTicks = DateTime.UtcNow.Ticks - startingPoint.Ticks;

        return CryptoHelper.GenerateHash(elapsedTicks.ToString(CultureInfo.InvariantCulture));
    }
}
