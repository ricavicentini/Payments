using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.UnitTests;

public class MoneyTests
{
    [Fact]
    public void BRL_Should_Set_Currency_Tag_And_Decimals_Correctly()
    {
        var money = new BRL(10.239m);

        Assert.Equal(CurrencyType.BRL, money.Currency);
        Assert.Equal("R$", money.MoneyTag);
        Assert.Equal(2, money.Decimals);
        Assert.Equal("R$ 10.24", money.ToString());
    }

    [Fact]
    public void USD_Should_Set_Currency_Tag_And_Decimals_Correctly()
    {
        var money = new USD(20.125m);

        Assert.Equal(CurrencyType.USD, money.Currency);
        Assert.Equal("US$", money.MoneyTag);
        Assert.Equal(2, money.Decimals);
        Assert.Equal("US$ 20.12", money.ToString());
    }

    [Fact]
    public void Money_Base_Record_Should_Keep_Amount_And_Currency()
    {
        var money = new Money(99.99m, CurrencyType.USD);

        Assert.Equal(99.99m, money.Amount);
        Assert.Equal(CurrencyType.USD, money.Currency);
    }
}
