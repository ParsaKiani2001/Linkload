using Application.Common.Extentions;
using Application.Common.Interface;
using Application.CQRS.Link.Query;
using Application.Dto;
using Application.Models;
using Linkload.API.Attributes;
using Linkload.API.Substructures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;


namespace Linkload.API.Controllers
{
    public class LinkController:BaseController
    {
        [HttpPost]
        [SwaggerResponse(0,"",typeof(ResponceBaseModel<PaginatedList<LinkPublicDto>>))]
        public async Task<IResponceBase> GetPublicLinks(GetLinkPublicQuery request)
        {
            return await Mediator.Send(request);
        }

        [Athentication]
        [HttpPost]
        [SwaggerResponse(0, "", typeof(ResponceBaseModel<PaginatedList<LinkDto>>))]
        public async Task<IResponceBase> GetLinks(GetLinkQuery request)
        {
            return await Mediator.Send(request);
        }
    }
}
