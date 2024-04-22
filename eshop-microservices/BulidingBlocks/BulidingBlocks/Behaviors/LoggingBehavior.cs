using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BulidingBlocks.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>
        (ILogger<LoggingBehavior<TRequest, TResponse>> logger) : 
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull,IRequest<TResponse>
        where   TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handle request={Request}- Response{Response} - RequestData={RequestData}",
                typeof(TRequest).Name, typeof(TResponse).Name, request);

            var timer = new Stopwatch();
            timer.Start();
            var response= await next();
            timer.Stop();
            var timeToken = timer.Elapsed;
            if (timeToken.Seconds > 3)
                logger.LogWarning("[PERFORMANCE] the request{Request} took {timeToken} seconds", typeof(TRequest).Name, timeToken.Seconds);
            logger.LogInformation("[END] Handled {Request} and {Response}", typeof(TRequest).Name, typeof(TResponse).Name);
            return response;
        }
    }
}
