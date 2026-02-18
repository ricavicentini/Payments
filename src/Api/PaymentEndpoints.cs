public static class PaymentEndpoints
{
    public static void MapPaymentEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup("/api/paymets")
                    .WithTags("Payments");
    }
}