using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Context;

public class MyContext : DbContext
{
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
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Education>()
            .HasOne(p => p.Profiling)
            .WithOne(e => e.Education)
            .HasForeignKey<Profiling>(a => a.EducationId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Profiling>()
            .HasOne(e => e.Employee)
            .WithOne(p => p.Profiling)
            .HasForeignKey<Employee>(a => a.NIK)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Employee>()
            .HasOne(a => a.Account)
            .WithOne(e => e.Employee)
            .HasForeignKey<Account>(a => a.EmployeeNIK)
            .OnDelete(DeleteBehavior.Cascade);

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
    }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Profiling> Profilings { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<University> Universities { get; set; }
}
