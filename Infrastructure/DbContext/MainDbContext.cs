using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interface;
using Domain.Entity.Link;
using Domain.Entity.Token;
using Domain.Entity.Users;
using Microsoft.EntityFrameworkCore;

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

        public  Task<int> SaveChangeAsync(CancellationToken cancellationToken)
        {
           /* var date = DateTime.Now;
            Guid? currentUserId = _currentUser?.UserModel == null ? null : _currentUser.UserModel.UserID;

            foreach (var entry in ChangeTracker.Entries<IBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreateBy = currentUserId;
                        entry.Entity.CreateOn = date;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = currentUserId;
                        entry.Entity.LastModifiedOn = date;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);*/
           return Task.FromResult(0);

        }
    }
}
