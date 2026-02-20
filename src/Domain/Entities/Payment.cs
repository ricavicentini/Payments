using System;
using System.Reflection.Metadata.Ecma335;
using Domain.Enums;
using Domain.ValueObjects;
using OneOf;


namespace Domain.Entities;

public class Payment
{
    public Guid Id { get; init; }
    public Money Amount { get; private set; }
    public string? Description { get; private set; }
    public PaymentType Type { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public OneOf<PixDetails, CreditCardDetails> PaymentDetails {get; private set;}

    private Payment(Money amount, string? description, PaymentType paymentType, DateTime createdAt, OneOf<PixDetails, CreditCardDetails> paymentDetails)
    {
        Id = Guid.NewGuid();
        Amount = amount;
        Description = description;
        Type = paymentType;
        CreatedAt = createdAt;
        PaymentDetails = paymentDetails;
    }

    private static OneOf<CreditCardDetails, CreditCardValidationErrors> AddCreditCardDetails(string cardNumber, string cardHolderName, DateTime expirationDate, string cvv) =>
        CreditCardDetails.Create(cardNumber, cardHolderName, expirationDate, cvv);

    public static OneOf<Payment, CreditCardValidationErrors> CreateBRLCreditCardPayment(decimal amount, string? description, string cardNumber, string cardHolderName, DateTime expirationDate, string cvv)
    {
        var cardDetailsResult = AddCreditCardDetails(cardNumber, cardHolderName, expirationDate, cvv);

        return cardDetailsResult.Match<OneOf<Payment, CreditCardValidationErrors>>(
            cardDetails => new Payment(amount: new BRL(amount),
                               description: description,
                               paymentType: PaymentType.CreditCard,
                               createdAt: DateTime.UtcNow,
                               paymentDetails: cardDetails),
            validationErrors => validationErrors
        );
    }



}
