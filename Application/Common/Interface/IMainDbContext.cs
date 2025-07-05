using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Link;
using Domain.Entity.Token;
using Domain.Entity.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interface
{
    public interface IMainDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Tokens> Tokens { get; set; }
        public DbSet<Links> Links { get; set; }
        public DbSet<Otp> Otps { get; set; }
        public DbSet<UserPicture> Pictures { get; set; }
        Task<int> SaveChangeAsync(CancellationToken cancellationToken);

    }
}
