using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Extentions;
using MediatR;

namespace Application.Common.Behaviours
{
    public class UnhandledExceptions<TRequest, TResponse> : Result, IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                var result = await next();
                return result; 
            }
            catch (Exception ex)
            {
                return (TResponse)await UnHandledException($"Message: {ex.Message} || Inner Message: ${ex.InnerException?.Message}");
            }
        }
    }
}
