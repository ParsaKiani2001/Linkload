using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Extentions;
using Application.Common.Interface;
using Application.Dto;
using Application.Models;
using Domain.Enums;
using MediatR;

namespace Application.CQRS.Link.Query
{
    public class GetLinkPublicQuery : IRequest<IResponceBase>
    {
        public PaginatedRequest PaginatedRequest { get; set; }
    }
    public class GetLinkPublicHandler : Result, IRequestHandler<GetLinkPublicQuery, IResponceBase>
    {
        public readonly IMainDbContext _dbContext;
        public GetLinkPublicHandler(IMainDbContext dbContext, ICurrentUser currentUser)
        {
            _dbContext = dbContext;
        }

        public async Task<IResponceBase> Handle(GetLinkPublicQuery request, CancellationToken cancellationToken)
        {
           

            var country = _dbContext.Links.Where(x => x.IsActive && !x.IsDeleted && x.Mode == LinkMode.publiced).Select(x =>
            new LinkPublicDto()
            {
                Id = x.Id,
                CreateTime = x.CreateDateTime,
                Name = x.Name,
                AccessCode = x.AccessCode,
            });

            var pagination = await PaginatedList<LinkPublicDto>.CreateAsync(country, request.PaginatedRequest);
            return await TrueOk(pagination);
        }

    }
}
