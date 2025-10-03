namespace Basket;

public class AppSettings
{
    public BrokerOptions BrokerOptions { get; set; } = null!;
}

public sealed class BrokerOptions
{
    public const string SectionName = "BrokerOptions";

    public required string Host { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}