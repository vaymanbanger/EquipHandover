using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquipHandover.Entities.Configurations;

/// <summary>
/// Описывает конфигурацию для <see cref="Document"/>
/// </summary>
public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable("Documents");
        
        builder.HasKey(d => d.Id);
        builder.Property(x => x.RentalDate).IsRequired();
        builder.HasIndex(x => x.RentalDate);
        
        builder.Property(x => x.SignatureNumber)
            .IsRequired()
            .HasMaxLength(50);
        builder.HasIndex(x => x.SignatureNumber, $"IX_{nameof(Document)}_{nameof(Document.DeletedAt)}")
            .IsUnique()
            .HasFilter($"\"{nameof(Document.DeletedAt)}\" IS NULL");
        
        builder.Property(x => x.City)
            .IsRequired()
            .HasMaxLength(100);
    }
}