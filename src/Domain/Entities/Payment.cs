using System;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Payment
{
    public Guid Id { get; init; }
    public Money Amount { get; private set; }
    public string? Description { get; private set; }
    public PaymentType Type { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public PixDetails? PixDetails { get; private set; }
    public CreditCardDetails? CreditCardDetails { get; private set; }

    public Payment(Money amount,  string? description, PaymentType paymentType, DateTime createdAt)
    {
        Id = Guid.NewGuid();
        Amount = amount;
        Description = description;
        Type = paymentType;
        CreatedAt = createdAt;
    }



}