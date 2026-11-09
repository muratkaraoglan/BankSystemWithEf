using BankSystem.Services.Helpers;

namespace BankSystem.Services.Generators;
/// <summary>
/// Generates unique numbers based on a GUID, hashed with <see cref="CryptoHelper"/>.
/// </summary>
public class GuidNumberGenerator : IUniqueNumberGenerator
{
    public string Generate()
    {
        return CryptoHelper.GenerateHash(Guid.NewGuid().ToString());
    }
}
