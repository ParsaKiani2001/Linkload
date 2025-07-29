using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Extentions;
using Application.Common.Interface;
using Application.Dto;
using Application.Models;
using Domain.Entity.Users;
using Domain.Enums;
using MediatR;

namespace Application.CQRS.Link.Query
{
    public class GetLinkQuery:IRequest<IResponceBase>
    {
        public PaginatedRequest PaginatedRequest { get; set; }
    }
    public class GetLinkHandler : Result, IRequestHandler<GetLinkQuery,IResponceBase>
    {
        public readonly IMainDbContext _dbContext;
        private readonly ICurrentUser _currentUser;
        public GetLinkHandler(IMainDbContext dbContext,ICurrentUser currentUser)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
        }

        public async Task<IResponceBase> Handle(GetLinkQuery request, CancellationToken cancellationToken)
        {
            if (_currentUser.UserModel == null)
                return await FalseOk(ResponceMessage.UserNotFound);
            var country = _dbContext.Links.Where(x => x.IsActive && !x.IsDeleted && x.CreateBy == _currentUser.UserModel.Id).Select(x =>
            new LinkDto() { 
                Id = x.Id,
                CreateTime = x.CreateDateTime,
                IsSecure = x.IsSecurted,
                Name = x.Name,
                AccessCode = x.AccessCode,
                Mode = x.Mode,
            });
            var pagination = await PaginatedList<LinkDto>.CreateAsync(country, request.PaginatedRequest);
            return await TrueOk(pagination);
        }
    }
}
