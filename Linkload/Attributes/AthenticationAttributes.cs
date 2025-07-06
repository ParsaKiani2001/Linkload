using Application.Common.Interface;
using Application.Models;
using Domain.Enums;
using Domain.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Linkload.API.Attributes
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
    public class AthenticationAttribute:ActionFilterAttribute
    {
        private readonly bool _type;
        public AthenticationAttribute(bool type = true)
        {
            _type = type;
        }
        public override async void OnActionExecuting(ActionExecutingContext context)
        {

            if (_type)
            {
                var jwt = context.HttpContext.Request.Headers.SingleOrDefault(x => x.Key == "Authorization").Value.FirstOrDefault();
                var token = context.HttpContext.RequestServices.GetService(typeof(IToken)) as IToken;
                var valid = await token.ValidateToken(jwt);
                if (valid.Status != Domain.Enums.ResponceMessage.Ok)
                {
                    context.Result = new JsonResult(new ResponceBaseModel()
                    {
                        Message = valid.Message,
                        Status = valid.Status
                    }) { StatusCode = StatusCodes.Status200OK};
                    return;
                }
            }
        }
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : ActionFilterAttribute
    {
        private readonly bool _authorized;
        public AuthorizeAttribute(bool authorized = true)
        {
            _authorized = authorized;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var db = context.HttpContext.RequestServices.GetService<IMainDbContext>();
            var user = context.HttpContext.RequestServices.GetService<ICurrentUser>();
            try
            {
                var id = user.UserModel.Id;
                string? actionName = context.HttpContext.Request.RouteValues["action"]?.ToString();
                string? controllerName = context.HttpContext.Request.RouteValues["controller"]?.ToString();
                if (string.IsNullOrEmpty(actionName) || string.IsNullOrEmpty(controllerName))
                    return;
            }
            catch (Exception ex) {
                context.Result = new JsonResult(new ResponceBaseModel() { 
                    Status= Domain.Enums.ResponceMessage.AccessDenied,
                    Message=ResponceMessage.AccessDenied.GetDescription()
                }) { StatusCode = StatusCodes.Status200OK };
                return;
            }

        }
    }
}
