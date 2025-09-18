using EquipHandover.Entities.Constants;
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
            .HasMaxLength(EntitiesConstants.MaxLengthFullName)
            .IsRequired();

        builder.Property(x => x.Enterprise)
            .HasMaxLength(EntitiesConstants.MaxLengthEnterprise)
            .IsRequired();
        
        builder.Property(x => x.TaxPayerNum)
            .HasMaxLength(EntitiesConstants.TaxPayerNumLength)
            .IsRequired();
        builder.HasIndex(x => x.TaxPayerNum, $"IX_{nameof(Sender)}_{nameof(Sender.TaxPayerNum)}")
            .IsUnique()
            .HasFilter("[TaxPayerId] IS NOT NULL");
    }
}