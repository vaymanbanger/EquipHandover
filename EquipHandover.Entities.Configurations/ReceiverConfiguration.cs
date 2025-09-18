using EquipHandover.Entities.Constants;
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
            .HasMaxLength(EntitiesConstants.MaxLengthFullName);

        builder.Property(x => x.Enterprise)
            .HasMaxLength(EntitiesConstants.MaxLengthEnterprise);

        builder.Property(x => x.RegistrationNumber)
            .HasMaxLength(EntitiesConstants.RegistrationNumberLength)
            .IsRequired();
        builder.HasIndex(x => x.RegistrationNumber, $"IX_{nameof(Receiver)}_{nameof(Receiver.RegistrationNumber)}")
            .IsUnique()
            .HasFilter("[RegistrationNumber] IS NOT NULL");
    }
}