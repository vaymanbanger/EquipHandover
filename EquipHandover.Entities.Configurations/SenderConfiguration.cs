using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquipHandover.Entities.Configurations;

/// <summary>
/// Описывает конфигурацию для <see cref="Sender"/>
/// </summary>
public class SenderConfiguration : IEntityTypeConfiguration<Sender>
{
    /// <summary>
    /// Конфигурация для <see cref="Sender"/>
    /// </summary>
    public void Configure(EntityTypeBuilder<Sender> builder)
    {
        builder.ToTable("Senders");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FullName)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Enterprise)
            .HasMaxLength(90)
            .IsRequired();
        
        builder.Property(x => x.TaxPayerId)
            .HasMaxLength(12)
            .IsRequired();
    }
}