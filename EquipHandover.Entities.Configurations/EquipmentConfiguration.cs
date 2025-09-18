using EquipHandover.Entities.Constants;
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
            .HasMaxLength(EntitiesConstants.MaxLength)
            .IsRequired();
        builder.HasIndex(x => x.Name, $"IX_{nameof(Equipment)}_{nameof(Equipment.Name)}");

        
        builder.Property(x => x.ManufacturedYear)
            .IsRequired();

        builder.Property(x => x.SerialNumber)
            .HasMaxLength(EntitiesConstants.MaxLength)
            .IsRequired();
    }
}