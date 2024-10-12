﻿
namespace BulidingBlocks.Messaging.Events
{
    public record BasketCheckoutEvent :IntegrationEvent
    {
        public string UserName { get; set; } = default!;
        public Guid CustomerId { get; set; } = default!;
        public decimal TotalPrice { get; set; } = default!;
        //Shipping and Billing Address
        public string FirstName { get; set; } = default!;
        public string LastrName { get; set; } = default!;
        public string EmailAddress { get; set; } = default!;
        public string AddressLine { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string State { get; set; } = default!;
        public string ZiPCode { get; set; } = default!;

        //Payment
        public string CartName { get; set; } = default!;
        public string CartNumber { get; set; } = default!;
        public string Expiration { get; set; } = default!;
        public string CVV { get; set; } = default!;
        public int PaymentMethod { get; set; } = default!;
    }
}
