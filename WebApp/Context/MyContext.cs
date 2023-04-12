using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Context;

public class MyContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Profiling> Profilings { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<University> Universities { get; set; }

    public MyContext(DbContextOptions<MyContext> option) : base(option)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<University>()
            .HasMany(u => u.Educations)
            .WithOne(e => e.University)
            .HasForeignKey(a => a.UniversityId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Education>()
            .HasOne<University>(e => e.University)
            .WithMany(u => u.Educations)
            .HasForeignKey(e => e.UniversityId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Profiling>()
            .HasKey(p => p.EducationId);

        modelBuilder.Entity<Education>()
            .HasOne<Profiling>(e => e.Profiling)
            .WithOne(p => p.Education);

        modelBuilder.Entity<Profiling>()
            .HasKey(p => p.EmployeeNIK);

        modelBuilder.Entity<Employee>()
            .HasOne<Profiling>(e => e.Profiling)
            .WithOne(p => p.Employee);

        modelBuilder.Entity<Profiling>()
            .HasKey(p => p.EmployeeNIK);

        modelBuilder.Entity<Employee>()
            .HasOne<Account>(e => e.Account)
            .WithOne(a => a.Employee);

        modelBuilder.Entity<AccountRole>()
                .HasKey(ar => ar.Id);

        modelBuilder.Entity<AccountRole>()
            .HasOne(ar => ar.Account)
            .WithMany(a => a.AccountRoles)
            .HasForeignKey(ar => ar.AccountNIK)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AccountRole>()
            .HasOne(ar => ar.Role)
            .WithMany(r => r.AccountRoles)
            .HasForeignKey(ar => ar.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<LoginVM>()
            .HasNoKey();
    }

    public DbSet<WebApp.ViewModels.LoginVM>? LoginVM { get; set; }
}
