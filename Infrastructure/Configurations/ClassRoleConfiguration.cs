using Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ClassRoleConfiguration : IEntityTypeConfiguration<ClassRole>
{
    public void Configure(EntityTypeBuilder<ClassRole> builder)
    {
        builder.ToTable(nameof(ClassRole));
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        builder.HasOne(e => e.Class)
            .WithMany(e => e.ClassRoles)
            .HasForeignKey(e => e.ClassId);

        builder.HasOne(e => e.Role)
            .WithMany(e => e.ClassRoles)
            .HasForeignKey(e => e.RoleId);

        builder.HasOne(e => e.Student)
            .WithMany(e => e.ClassRoles)
            .HasForeignKey(e => e.StudentId);
    }
}