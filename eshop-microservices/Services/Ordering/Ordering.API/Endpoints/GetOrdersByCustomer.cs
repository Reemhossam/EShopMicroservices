﻿using BulidingBlocks.CQRS;
using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endpoints
{
    //public record GetOrdersByCustomerRequest(Guid CustomerId);
    public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);
    public class GetOrdersByCustomer : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/customer/{customerId}", async(Guid customerId, ISender sender) =>
            {
                var query = new GetOrdersByCustomerQuery(customerId);
                var result = await sender.Send(query);
                var response = result.Adapt<GetOrdersByCustomerResponse>();
                return Results.Ok(response);
            })
            .WithName("GetOrdersByCustomer")
            .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Orders By Customer")
            .WithDescription("Get Orders By Customer");
        }
    }
}
