using System.Globalization;
using BankSystem.Services.Helpers;

namespace BankSystem.Services.Generators;

/// <summary>
/// Singleton generator that produces sequential numbers starting from 1234567890,
/// hashed with <see cref="CryptoHelper"/>.
/// </summary>
public sealed class SimpleGenerator : IUniqueNumberGenerator
{
    private const long StartingNumber = 1234567890;

    private long counter = StartingNumber;

    static SimpleGenerator()
    {
        Instance = new SimpleGenerator();
    }

    private SimpleGenerator()
    {
    }

    public static SimpleGenerator Instance { get; }

    public string Generate()
    {
        long next = Interlocked.Increment(ref this.counter);

        return CryptoHelper.GenerateHash(next.ToString(CultureInfo.InvariantCulture));
    }
}
