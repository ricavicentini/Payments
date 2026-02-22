namespace Domain.ValueObjects;

public record PixDetails : PaymentMethod
{
    public string? PixKey { get; init; }
    public string? Description { get; init; }
}
