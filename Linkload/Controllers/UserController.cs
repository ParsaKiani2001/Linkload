using Application.Common.Extentions;
using Application.Common.Interface;
using Application.CQRS.User.Command;
using Application.Dto;
using Application.Models;
using Linkload.API.Substructures;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Linkload.API.Controllers
{
    public class UserController:BaseController
    {
        [HttpPost]
        [SwaggerResponse(0, "", typeof(ResponceBaseModel))]
        public async Task<IResponceBase> SignupUser(SignupUser request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost]
        [SwaggerResponse(0, "", typeof(ResponceBaseModel))]
        public async Task<IResponceBase> LoginUser(LoginUser request)
        {
            return await Mediator.Send(request);
        }
        [HttpPost]
        [SwaggerResponse(0, "", typeof(ResponceBaseModel))]
        public async Task<IResponceBase> ValidUser(VerifyUser request)
        {
            return await Mediator.Send(request);
        }

        [HttpPost]
        [SwaggerResponse(0, "", typeof(ResponceBaseModel))]
        public async Task<IResponceBase> ReValidUser(ReValidate request)
        {
            return await Mediator.Send(request);
        }
    }
}
