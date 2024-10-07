using BulidingBlocks.CQRS;
using Ordering.Application.Orders.Queries.GetOrderByName;

namespace Ordering.API.Endpoints
{
    //public record GetOrdersByNameRequest(string name);

    public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);
    public class GetOrdersByName : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{orderName}", async (string orderName, ISender sender) =>
            {
                var query = new GetOrdersByNameQuery(orderName);
                var result = await sender.Send(query);
                var response = result.Adapt<GetOrdersByNameResponse>();
                return Results.Ok(response);
            })
            .WithName("GetOrdersByName")
            .Produces<GetOrdersByNameResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Orders By Name")
            .WithDescription("Get Orders By Name");
        }
    }
}
