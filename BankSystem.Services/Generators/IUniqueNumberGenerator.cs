namespace BankSystem.Services.Generators;

/// <summary>
/// Defines a contract for generating unique account numbers.
/// </summary>
public interface IUniqueNumberGenerator
{
    string Generate();
}
