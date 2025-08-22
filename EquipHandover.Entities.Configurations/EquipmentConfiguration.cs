using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquipHandover.Entities.Configurations;

/// <summary>
/// Описывает конфигурацию для <see cref="Equipment"/>
/// </summary>
public class EquipmentConfiguration : IEntityTypeConfiguration<Equipment>
{
    /// <summary>
    /// Конфигурация для <see cref="Equipment"/>
    /// </summary>
    public void Configure(EntityTypeBuilder<Equipment> builder)
    {
        builder.ToTable("Equipment");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();
        builder.HasIndex(x => x.Name, $"IX_{nameof(Equipment)}_{nameof(Equipment.Name)}")
            .IsUnique()
            .HasFilter($"\"{nameof(Equipment.DeletedAt)}\" IS NULL");

        
        builder.Property(x => x.ManufactureDate)
            .HasMaxLength(4)
            .IsRequired();

        builder.Property(x => x.SerialNumber)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(x => x.EquipmentNumber)
            .HasMaxLength(200)
            .IsRequired();
    }
}