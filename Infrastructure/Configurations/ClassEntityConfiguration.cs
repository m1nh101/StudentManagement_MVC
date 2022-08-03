using Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ClassEntityConfiguration : IEntityTypeConfiguration<Class>
{
  public void Configure(EntityTypeBuilder<Class> builder)
  {
    builder.ToTable(nameof(Class));
    builder.HasKey(e => e.Id);

    builder.HasMany(e => e.Students)
      .WithMany(e => e.Classes)
      .UsingEntity<ClassStudent>(j =>
        j.HasOne(e => e.Student)
          .WithMany(e => e.ClassStudents)
          .HasForeignKey(e => e.StudentId),
          j => j
            .HasOne(e => e.Class)
            .WithMany(e => e.ClassStudents)
            .HasForeignKey(e => e.ClassId),
            j =>
            {
              j.ToTable(nameof(ClassStudent));
              j.HasKey(e => new { e.ClassId, e.StudentId });
            });

    builder.HasMany(e => e.Subjects)
      .WithMany(e => e.Classes)
      .UsingEntity<ClassSubject>(j =>
        j.HasOne(e => e.Subject)
        .WithMany(e => e.ClassSubjects)
        .HasForeignKey(e => e.SubjectId),
        j => j
          .HasOne(e => e.Class)
          .WithMany(e => e.ClassSubjects)
          .HasForeignKey(e => e.ClassId),
          j =>
          {
            j.ToTable(nameof(ClassSubject));
            j.HasKey(e => new { e.ClassId, e.SubjectId });
          });
  }
}