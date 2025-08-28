using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquipHandover.Entities.Configurations;

/// <summary>
/// Описывает конфигурацию для <see cref="Receiver"/>
/// </summary>
public class ReceiverConfiguration : IEntityTypeConfiguration<Receiver>
{
    /// <summary>
    /// Конфигурация для <see cref="Receiver"/>
    /// </summary>
    public void Configure(EntityTypeBuilder<Receiver> builder)
    {
        builder.ToTable("Receivers");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FullName)
            .HasMaxLength(50);

        builder.Property(x => x.Enterprise)
            .HasMaxLength(90);

        builder.Property(x => x.RegistrationNumber)
            .HasMaxLength(13)
            .IsRequired();
        builder.HasIndex(x => x.RegistrationNumber, $"IX_{nameof(Receiver)}_{nameof(Receiver.RegistrationNumber)}")
            .IsUnique()
            .HasFilter("[RegistrationNumber] IS NOT NULL");
    }
}