using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interface;
using Domain.Base;
using Domain.Entity.Link;
using Domain.Entity.Token;
using Domain.Entity.Users;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Infrastructure.Presistence
{
    public class MainDbContext : DbContext,IMainDbContext
    {
        private readonly ICurrentUser _currentUser;
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }
        protected MainDbContext(DbContextOptions options, ICurrentUser currentUser) : base(options)
        {
            _currentUser = currentUser;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Tokens> Tokens { get; set; }
        public DbSet<Links> Links { get; set; }
        public DbSet<Otp> Otps { get; set; }
        public DbSet<UserPicture> Pictures { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var date = DateTime.Now;
            Guid? currentUserId = _currentUser?.UserModel == null ? null : _currentUser.UserModel.Id;

            foreach (var entry in ChangeTracker.Entries<BaseEntity<Guid>>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreateBy = currentUserId;
                        entry.Entity.CreateDateTime = date;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdateDateTime = date;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
