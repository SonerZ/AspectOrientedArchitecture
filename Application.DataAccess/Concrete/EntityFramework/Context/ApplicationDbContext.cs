using Application.Core.Entities.Concrete;
using Application.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.DataAccess.Concrete.EntityFramework.Context
{
    /// <summary>
    /// DbContext contains database entities.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        private readonly string _connectionString;

        public ApplicationDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) { }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        
        public DbSet<Function> Functions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<RoleClaim> RoleClaims { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        
        

        /// <summary>
        /// provides normally Npgsql.
        /// </summary>
        /// <param name="optionsBuilder">DbContextOptionsBuilder instance.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
            }
        }
    }
}
