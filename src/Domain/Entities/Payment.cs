using System;
using System.Reflection.Metadata.Ecma335;

using Domain.Enums;
using Domain.ValueObjects;

using FluentResults;

using OneOf;


namespace Domain.Entities;

public class Payment
{
    public Guid Id { get; init; }
    public Money Money { get; private set; }
    public string? Description { get; private set; }
    public PaymentType PaymentType { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public PaymentMethod PaymentDetails { get; init; }


    private Payment(Money amount, string? description, PaymentType paymentType, DateTime createdAt, PaymentMethod paymentDetails)
    {
        Id = Guid.NewGuid();
        Money = amount;
        Description = description;
        PaymentType = paymentType;
        CreatedAt = createdAt;
        PaymentDetails = paymentDetails;
    }


    public static Result<Payment> CreateBRLCreditCardPayment(decimal amount, string? description, string cardNumber, string cardHolderName, DateTime expirationDate, string cvv)
    {
        var brlMoneyResult = BRL.Create(amount);
        if (brlMoneyResult.IsFailed)
            return Result.Fail(brlMoneyResult.Errors);

        var cardDetailsResult = CreditCardDetails.Create(cardNumber, cardHolderName, expirationDate, cvv);
        if (cardDetailsResult.IsFailed)
            return Result.Fail(cardDetailsResult.Errors);

        if (description == string.Empty || description?.Length < 5 || description?.Length > 50)
            return Result.Fail("Payment description is invalid");

        return new Payment(brlMoneyResult.Value,
                            description: description,
                            paymentType: PaymentType.CreditCard,
                            createdAt: DateTime.UtcNow,
                            paymentDetails: cardDetailsResult.Value);
    }



}
