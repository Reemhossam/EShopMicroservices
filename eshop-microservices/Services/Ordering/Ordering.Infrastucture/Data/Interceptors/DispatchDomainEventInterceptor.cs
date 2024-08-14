﻿using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ordering.Infrastucture.Data.Interceptors
{
    public class DispatchDomainEventInterceptor(IMediator mediator) :SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            DispatchDomainEvent(eventData.Context).GetAwaiter().GetResult();
            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            await DispatchDomainEvent(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private async Task DispatchDomainEvent(DbContext? context)
        {
            if (context == null) return;
            var aggregates = context.ChangeTracker.Entries<IAggregate>()
                                    .Where(a => a.Entity.DomainEvents.Any())
                                    .Select(a => a.Entity);
            List<IDomainEvent> domainEvents = aggregates.SelectMany(a => a.DomainEvents).ToList();
            foreach( var entity in aggregates)
            {
                entity.ClearDomainEvents();
            }
            foreach( var domainEvent in  domainEvents) 
            {
                await mediator.Publish(domainEvent);
            }
            
        }
    }
}
