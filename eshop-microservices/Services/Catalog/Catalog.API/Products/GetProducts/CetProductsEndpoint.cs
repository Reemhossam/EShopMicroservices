using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.GetProducts
{
    public record CetProductsResponse(IEnumerable<Product> products);
    public class CetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender) =>
            {
                var result = await sender.Send(new GetProductsQuery());
                var response = result.Adapt<CetProductsResponse>();
                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .Produces<CetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithDescription("Get Products");
        }
    }
}
