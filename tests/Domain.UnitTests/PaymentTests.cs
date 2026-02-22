using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;

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

        Assert.IsType<Payment>(result.Value);
        Assert.Equal(PaymentType.CreditCard, result.Value.PaymentType);
        Assert.IsType<BRL>(result.Value.Money);
        Assert.Equal(100m, result.Value.Money.Amount);
        Assert.Equal("Order #123", result.Value.Description);
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

        Assert.True(result.IsFailed);
        Assert.NotEmpty(result.Errors);
        Assert.Contains(result.Errors, error => error.Message.Contains("holder name"));
    }
}
