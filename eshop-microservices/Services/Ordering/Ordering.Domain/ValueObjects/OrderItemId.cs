namespace Ordering.Domain.ValueObjects
{
    public record OrderItemId
    {
        public Guid Value { get; }
        public OrderItemId(Guid value)=> this.Value = value;
        
        public static OrderItemId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value ==Guid.Empty)
            {
                throw new DomainException("OrderItemId cannot be empty.");
            }
            return new OrderItemId(value);
        }
    }
}
