using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Extentions;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Application.Common.Behaviours
{
    public class AuthorizationBhaviour<TRequest, TResponse> : Result, IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var authorizeAttr = request.GetType().GetCustomAttributes<AuthorizeAttribute>();
            if (authorizeAttr.Any())
            {
                return (TResponse)await FalseException(ResponceMessage.AccessDenied);
            }
            return await next();
        }
    }
}
