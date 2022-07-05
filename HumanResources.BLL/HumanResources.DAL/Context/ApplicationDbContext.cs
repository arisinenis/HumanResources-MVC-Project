using HumanResources.Core.Entities;
using HumanResources.DAL.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.DAL.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<AdvancePayment> AdvancePayments { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        //public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration<Package>(new PackageConfiguration());
            modelBuilder.ApplyConfiguration<User>(new UserConfiguration());
            modelBuilder.ApplyConfiguration<Company>(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration<Employee>(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration<Permission>(new PermissionConfiguration());

        }
    }
}
