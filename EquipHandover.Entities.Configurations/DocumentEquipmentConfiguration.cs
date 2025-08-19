using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquipHandover.Entities.Configurations;

/// <summary>
/// Описывает конфигурацию для <see cref="DocumentEquipment"/>
/// </summary>
public class DocumentEquipmentConfiguration  : IEntityTypeConfiguration<DocumentEquipment>
{
    /// <summary>
    /// Конфигурация для <see cref="DocumentEquipment"/>
    /// </summary>
    public void Configure(EntityTypeBuilder<DocumentEquipment> builder)
    {
        builder.ToTable("DocumentEquipment");
        builder.HasKey(x => new { x.DocumentId, x.EquipmentId });
        
        builder.HasOne(x => x.Document)
            .WithMany()
            .HasForeignKey(x => x.DocumentId);
        
        builder.HasOne(x => x.Equipment)
            .WithMany()
            .HasForeignKey(x => x.EquipmentId);
    }
}