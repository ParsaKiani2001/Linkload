using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Users;

namespace Application.Common.Interface
{
    public interface IToken
    {
        Task<Tuple<string, string, DateTime>> GenerateAccessToken(User user);
        Task<IResponceBase> ValidateToken (string token);
    }
}
