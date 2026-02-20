using Domain.Entities;
using Domain.Enums;

namespace Domain.UnitTests;

public class PaymentTests
{
    [Fact]
    public void CreateBRLCreditCardPayment_ShouldReturnPayment_WhenCardDetailsAreValid()
    {
        var result = Payment.CreateBRLCreditCardPayment(
            amount: 100m,
            description: "Order #123",
            cardNumber: "4111111111111111",
            cardHolderName: "John Doe",
            expirationDate: DateTime.UtcNow.AddMonths(6),
            cvv: "123");

        Assert.True(result.IsT0);
        Assert.Equal(PaymentType.CreditCard, result.AsT0.Type);
        Assert.Equal(100m, result.AsT0.Amount.Amount);
        Assert.Equal("Order #123", result.AsT0.Description);
    }

    [Fact]
    public void CreateBRLCreditCardPayment_ShouldReturnValidationErrors_WhenCardDetailsAreInvalid()
    {
        var result = Payment.CreateBRLCreditCardPayment(
            amount: 100m,
            description: "Order #123",
            cardNumber: "123",
            cardHolderName: " ",
            expirationDate: DateTime.UtcNow.AddMonths(-2),
            cvv: "1");

        Assert.True(result.IsT1);
        Assert.NotEmpty(result.AsT1.Errors);
    }
}
