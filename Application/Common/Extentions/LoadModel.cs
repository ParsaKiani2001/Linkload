using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Extentions
{
    public class LoadModel
    {
        public static async Task<List<TModel>> LoadingModel <TModel,Tkey>(DbSet<TModel> model,CancellationToken cancellationToken = default) where TModel:BaseEntity<Tkey>
        {  
            return await model.Where(x => x.IsActive && !x.IsDeleted).ToListAsync(cancellationToken);
        }
    }
}
