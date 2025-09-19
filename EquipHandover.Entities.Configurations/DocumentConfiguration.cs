using EquipHandover.Entities.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquipHandover.Entities.Configurations;

/// <summary>
/// Описывает конфигурацию для <see cref="Document"/>
/// </summary>
public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    /// <summary>
    /// Конфигурация для <see cref="Document"/>
    /// </summary>
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable("Documents");
        
        builder.HasKey(d => d.Id);
        builder.Property(x => x.RentalDate).IsRequired();
        
        builder.Property(x => x.SignatureNumber)
            .IsRequired()
            .HasMaxLength(EntitiesConstants.MaxLengthSignatureNumber);
        
        builder.Property(x => x.City)
            .IsRequired()
            .HasMaxLength(EntitiesConstants.MaxLengthCity);
    }
}