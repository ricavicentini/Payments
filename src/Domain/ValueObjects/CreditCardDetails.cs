using System;
using System.Collections.Generic;
using System.Linq;
using OneOf;

namespace Domain.ValueObjects;

public sealed record CreditCardDetails
{
    private const int MinCardNumberLength = 13;
    private const int MaxCardNumberLength = 19;
    private const int MinHolderNameLength = 2;
    private const int MaxHolderNameLength = 100;
    private static readonly int[] AllowedCvvLengths = [3, 4];

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
        var normalizedCardNumber = NormalizeCardNumber(cardNumber);
        var normalizedHolderName = cardHolderName?.Trim();
        var normalizedCvv = cvv?.Trim();
        var errors = Validate(normalizedCardNumber, normalizedHolderName, expirationDate, normalizedCvv);

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

    private static List<string> Validate(
        string normalizedCardNumber,
        string? normalizedHolderName,
        DateTime expirationDate,
        string? normalizedCvv)
    {
        var errors = new List<string>();
        ValidateCardNumber(normalizedCardNumber, errors);
        ValidateCardHolderName(normalizedHolderName, errors);
        ValidateExpirationDate(expirationDate, errors);
        ValidateCvv(normalizedCvv, errors);
        return errors;
    }

    private static void ValidateCardNumber(string cardNumber, ICollection<string> errors)
    {
        if (string.IsNullOrWhiteSpace(cardNumber))
        {
            errors.Add("Card number is required.");
            return;
        }

        var hasValidLength = cardNumber.Length is >= MinCardNumberLength and <= MaxCardNumberLength;
        if (!cardNumber.All(char.IsDigit) || !hasValidLength)
        {
            errors.Add("Card number must contain only digits and have between 13 and 19 characters.");
        }
    }

    private static void ValidateCardHolderName(string? cardHolderName, ICollection<string> errors)
    {
        if (string.IsNullOrWhiteSpace(cardHolderName))
        {
            errors.Add("Card holder name is required.");
            return;
        }

        var hasValidLength = cardHolderName.Length is >= MinHolderNameLength and <= MaxHolderNameLength;
        if (!hasValidLength)
        {
            errors.Add("Card holder name must have between 2 and 100 characters.");
        }
    }

    private static void ValidateExpirationDate(DateTime expirationDate, ICollection<string> errors)
    {
        if (expirationDate == default)
        {
            errors.Add("Expiration date is required.");
            return;
        }

        var expirationLimit = new DateTime(
            expirationDate.Year,
            expirationDate.Month,
            DateTime.DaysInMonth(expirationDate.Year, expirationDate.Month),
            23,
            59,
            59,
            DateTimeKind.Utc);

        if (expirationLimit < DateTime.UtcNow)
        {
            errors.Add("Expiration date cannot be in the past.");
        }
    }

    private static void ValidateCvv(string? cvv, ICollection<string> errors)
    {
        if (string.IsNullOrWhiteSpace(cvv))
        {
            errors.Add("CVV is required.");
            return;
        }

        if (!cvv.All(char.IsDigit) || !AllowedCvvLengths.Contains(cvv.Length))
        {
            errors.Add("CVV must contain only digits and have 3 or 4 characters.");
        }
    }
}

public sealed record CreditCardValidationErrors
{
    public CreditCardValidationErrors(IReadOnlyCollection<string> errors)
    {
        Errors = errors;
    }

    public IReadOnlyCollection<string> Errors { get; }
}
