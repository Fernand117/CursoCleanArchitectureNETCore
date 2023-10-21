using Domain.Employes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class EmployeConfiguration : IEntityTypeConfiguration<Employe>
{
    public void Configure(EntityTypeBuilder<Employe> builder)
    {
        builder.ToTable("Employes");

        builder.Property(e => e.Id).HasConversion(
            employerId => employerId.Id,
            value => new EmployeId(value));

        builder.Property(e => e.Nombre).HasMaxLength(50);
        builder.Property(e => e.Paterno).HasMaxLength(50);
        builder.Property(e => e.Materno).HasMaxLength(50);
        builder.Property(e => e.FechaNacimiento);

        builder.Property(e => e.Active).IsRequired(true);
    }
}