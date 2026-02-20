using Domain.ValueObjects;

namespace Domain.UnitTests;

public class CreditCardDetailsTests
{
    [Fact]
    public void Create_ShouldReturnCreditCardDetails_WhenInputIsValid()
    {
        var expirationDate = DateTime.UtcNow.AddMonths(6);

        var result = CreditCardDetails.Create(
            cardNumber: "4111 1111-1111 1111",
            cardHolderName: "John Doe",
            expirationDate: expirationDate,
            cvv: "123");

        Assert.True(result.IsT0);
        Assert.Equal("4111111111111111", result.AsT0.CardNumber);
        Assert.Equal("John Doe", result.AsT0.CardHolderName);
        Assert.Equal("123", result.AsT0.CVV);
    }

    [Fact]
    public void Create_ShouldReturnErrors_WhenCardNumberIsInvalid()
    {
        var result = CreditCardDetails.Create(
            cardNumber: "ABC",
            cardHolderName: "John Doe",
            expirationDate: DateTime.UtcNow.AddMonths(6),
            cvv: "123");

        Assert.True(result.IsT1);
        Assert.Contains(result.AsT1.Errors, error => error.Contains("Card number"));
    }

    [Fact]
    public void Create_ShouldReturnErrors_WhenCardHolderNameIsInvalid()
    {
        var result = CreditCardDetails.Create(
            cardNumber: "4111111111111111",
            cardHolderName: " ",
            expirationDate: DateTime.UtcNow.AddMonths(6),
            cvv: "123");

        Assert.True(result.IsT1);
        Assert.Contains(result.AsT1.Errors, error => error.Contains("Card holder name"));
    }

    [Fact]
    public void Create_ShouldReturnErrors_WhenExpirationDateIsPast()
    {
        var result = CreditCardDetails.Create(
            cardNumber: "4111111111111111",
            cardHolderName: "John Doe",
            expirationDate: DateTime.UtcNow.AddMonths(-2),
            cvv: "123");

        Assert.True(result.IsT1);
        Assert.Contains(result.AsT1.Errors, error => error.Contains("Expiration date"));
    }

    [Fact]
    public void Create_ShouldReturnErrors_WhenCvvIsInvalid()
    {
        var result = CreditCardDetails.Create(
            cardNumber: "4111111111111111",
            cardHolderName: "John Doe",
            expirationDate: DateTime.UtcNow.AddMonths(6),
            cvv: "12A");

        Assert.True(result.IsT1);
        Assert.Contains(result.AsT1.Errors, error => error.Contains("CVV"));
    }
}
