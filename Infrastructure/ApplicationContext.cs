using Application.Entities;
using Application.Interfaces;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationContext : DbContext, IApplicationContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(e =>
        {
            e.ToTable(nameof(Student));
            e.HasKey(d => d.Id);
        });
        modelBuilder.Entity<Subject>(e =>
        {
            e.ToTable(nameof(Subject));
            e.HasKey(d => d.Id);
        });
        modelBuilder.Entity<Role>(e =>
        {
            e.ToTable(nameof(Role));
            e.HasKey(d => d.Id);
            e.HasData(new Role() { Name = "monitor"}, new Role() { Name = "secretary"});
        });
        modelBuilder.ApplyConfiguration(new ClassEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ClassRoleConfiguration());
        base.OnModelCreating(modelBuilder);
    }

    public virtual DbSet<Student> Students => Set<Student>();
    public virtual DbSet<Class> Classes => Set<Class>();
    public virtual DbSet<Role> Roles => Set<Role>();
    public Task Save() => base.SaveChangesAsync();

    public virtual DbSet<Subject> Subjects => Set<Subject>();
}