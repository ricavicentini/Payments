using System;
public record CreditCardDetails(
    string CardNumber,
    string CardHolderName,
    DateTime ExpirationDate,
    string CVV
);