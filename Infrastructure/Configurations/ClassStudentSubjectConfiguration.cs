using Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ClassStudentSubjectConfiguration : IEntityTypeConfiguration<ClassStudentSubject>
{
    public void Configure(EntityTypeBuilder<ClassStudentSubject> builder)
    {
        builder.ToTable(nameof(ClassStudentSubject));
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        builder.HasOne(e => e.Class)
            .WithMany(e => e.ClassStudentSubjects)
            .HasForeignKey(e => e.ClassId);

        builder.HasOne(e => e.Student)
            .WithMany(e => e.ClassStudentSubjects)
            .HasForeignKey(e => e.StudentId);

        builder.HasOne(e => e.Subject)
            .WithMany(e => e.ClassStudentSubjects)
            .HasForeignKey(e => e.SubjectId);
    }
}