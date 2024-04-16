using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductById
{
    //public record CetProductByIdRequest(Guid Id);
    public record CetProductByIdResponse(Product Product);
    public class CetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdQuery(id));
                var response = result.Adapt<CetProductByIdResponse>();
                return Results.Ok(response);
            })
            .WithName("GetProductsById")
            .Produces<CetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products By Id")
            .WithDescription("Get Products By Id");
        }
    }
}
