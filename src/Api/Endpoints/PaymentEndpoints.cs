namespace Api.Endpoints;
public static class PaymentEndpoints
{
    public static void MapPaymentEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGroup("/api/paymets")
                    .WithTags("Payments");

        endpoints.MapGet("/", async () => TypedResults.NoContent());
    }
}