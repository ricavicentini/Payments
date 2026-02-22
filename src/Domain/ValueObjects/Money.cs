using System;
using System.Globalization;
using System.Text;

using Domain.Enums;

using FluentResults;

namespace Domain.ValueObjects;

public record Money
{
    public decimal Amount { get; init; }
    public CurrencyType Currency { get; init; }

    protected Money(decimal amount, CurrencyType currencyType)
    {
        Amount = amount;
        Currency = currencyType;
    }

}

public record DefaultMoneyBehaviour : Money
{
    public string? MoneyTag { get; init; }
    public int Decimals { get; init; }

    public DefaultMoneyBehaviour(string moneyTag, int decimalpalces, decimal amount, CurrencyType currencyType) : base(amount, currencyType)
    {
        MoneyTag = moneyTag;
        Decimals = decimalpalces;
    }

    public override string ToString()
    {
        var rounded = Math.Round(Amount, Decimals);
        return $"{MoneyTag} {rounded.ToString($"F{Decimals}", CultureInfo.InvariantCulture)}";
    }
}

public sealed record BRL : DefaultMoneyBehaviour
{
    public BRL(decimal amount) : base("R$", 2, amount, CurrencyType.BRL)
    {

    }

    public override string ToString() => base.ToString();

    public static Result<BRL> Create(decimal amount) => amount <= 0
                                                         ? Result.Fail("Amount must be positive")
                                                         : Result.Ok(new BRL(amount));
}

public sealed record USD : DefaultMoneyBehaviour
{
    private const string moneyTag = "US$";
    public USD(decimal amount) : base(moneyTag, 2, amount, CurrencyType.USD)
    {

    }

    public override string ToString() => base.ToString();

    public static Result<USD> Create(decimal amount) => amount <= 0
                                                         ? Result.Fail("Amount must be positive")
                                                         : Result.Ok(new USD(amount));
}
