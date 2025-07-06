using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Linkload.API.Substructures
{
    [Route("API/[controller]/[action]")]
    [ApiController]
    public class BaseController:ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
