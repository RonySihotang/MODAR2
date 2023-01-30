using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;
using System.Security.Principal;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Contexts;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options) : base(options)
    {

    }

    // Introduces the Model to the Database that eventually becomes an Entity
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<EmployeeProjectList> EmployeeProjectLists { get; set; }
    public DbSet<ProjectList> ProjectLists { get; set; }
    public DbSet<Report> Reports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Unique Constraint
        // FAQ : Kenapa gk pake anotasi? gk bisa dan ribet :))
        modelBuilder.Entity<Employee>().HasAlternateKey(e => e.Phone);
        modelBuilder.Entity<Employee>().HasAlternateKey(e => e.Email);
        //modelBuilder.Entity<Employee>().HasAlternateKey(e => e.ManagerId);

        // Configure PK as FK 
        // FAQ : Kenapa gk pake anotasi? Karena Data Anotation gk support kalau ada PK sebagai FK juga

        // One Employee to One Account
        modelBuilder.Entity<Employee>()
            .HasOne(a => a.Account)
        .WithOne(e => e.Employee)
            .HasForeignKey<Account>(a => a.Id);

        //selfjoin
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Manager)
            .WithMany()
            .HasForeignKey(m => m.ManagerId)
            .IsRequired(false);

      


    }
}

