using System;
using System.Collections.Generic;
using System.Linq;
using OneOf;

namespace Domain.ValueObjects;

public sealed record CreditCardDetails
{
    public string CardNumber { get; }
    public string CardHolderName { get; }
    public DateTime ExpirationDate { get; }
    public string CVV { get; }

    private CreditCardDetails(string cardNumber, string cardHolderName, DateTime expirationDate, string cvv)
    {
        CardNumber = cardNumber;
        CardHolderName = cardHolderName;
        ExpirationDate = expirationDate;
        CVV = cvv;
    }

    public static OneOf<CreditCardDetails, CreditCardValidationErrors> Create(
        string cardNumber,
        string cardHolderName,
        DateTime expirationDate,
        string cvv)
    {
        var errors = new List<string>();
        var normalizedCardNumber = NormalizeCardNumber(cardNumber);
        var normalizedHolderName = cardHolderName?.Trim();
        var normalizedCvv = cvv?.Trim();

        if (string.IsNullOrWhiteSpace(normalizedCardNumber))
        {
            errors.Add("Card number is required.");
        }
        else if (!normalizedCardNumber.All(char.IsDigit) || normalizedCardNumber.Length < 13 || normalizedCardNumber.Length > 19)
        {
            errors.Add("Card number must contain only digits and have between 13 and 19 characters.");
        }

        if (string.IsNullOrWhiteSpace(normalizedHolderName))
        {
            errors.Add("Card holder name is required.");
        }
        else if (normalizedHolderName.Length < 2 || normalizedHolderName.Length > 100)
        {
            errors.Add("Card holder name must have between 2 and 100 characters.");
        }

        if (expirationDate == default)
        {
            errors.Add("Expiration date is required.");
        }
        else
        {
            var expirationLimit = new DateTime(expirationDate.Year, expirationDate.Month, DateTime.DaysInMonth(expirationDate.Year, expirationDate.Month), 23, 59, 59, DateTimeKind.Utc);
            if (expirationLimit < DateTime.UtcNow)
            {
                errors.Add("Expiration date cannot be in the past.");
            }
        }

        if (string.IsNullOrWhiteSpace(normalizedCvv))
        {
            errors.Add("CVV is required.");
        }
        else if (!normalizedCvv.All(char.IsDigit) || (normalizedCvv.Length != 3 && normalizedCvv.Length != 4))
        {
            errors.Add("CVV must contain only digits and have 3 or 4 characters.");
        }

        if (errors.Count > 0)
        {
            return new CreditCardValidationErrors(errors);
        }

        return new CreditCardDetails(
            cardNumber: normalizedCardNumber,
            cardHolderName: normalizedHolderName!,
            expirationDate: expirationDate,
            cvv: normalizedCvv!);
    }

    private static string NormalizeCardNumber(string value) =>
        string.IsNullOrWhiteSpace(value)
            ? string.Empty
            : value.Replace(" ", string.Empty).Replace("-", string.Empty);
}

public sealed record CreditCardValidationErrors
{
    public CreditCardValidationErrors(IReadOnlyCollection<string> errors)
    {
        Errors = errors;
    }

    public IReadOnlyCollection<string> Errors { get; }
}
