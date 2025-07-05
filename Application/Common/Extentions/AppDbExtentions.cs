using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Token;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Extentions
{
    public static class AppDbExtentions
    {
        public static bool HasUserTokenAsync(this DbSet<Tokens> model,Guid id)
        {
            return (id != null || id != Guid.Empty) && model.SingleOrDefault(x => x.CreateBy.Equals(id)) != null;
        }
        public static async Task<TModel> FindByUserNameAsync<TModel>(this DbSet<TModel> model, string username, CancellationToken cancellationToken = default)
        {
            return await model.SingleOrDefaultAsync(o=>o.UserName == username, cancellationToken);
        }
    }
}
