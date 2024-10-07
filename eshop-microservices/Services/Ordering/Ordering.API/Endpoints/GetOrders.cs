using Azure;
using BulidingBlocks.CQRS;
using BulidingBlocks.Pagination;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.API.Endpoints
{
    //public record GetOrdersQuery(PaginationRequest Request);
    public record GetOrdersResult(PaginatedResult<OrderDto> Orders);
    public class GetOrders : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var query = new GetOrdersQuery(request);
                var result = await sender.Send(query);
                var response = result.Adapt<GetOrdersResult>();
                return Results.Ok(response);
            })
            .WithName("GetOrders")
            .Produces<GetOrdersResult>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Orders")
            .WithDescription("Get Orders");
        }
    }
}
