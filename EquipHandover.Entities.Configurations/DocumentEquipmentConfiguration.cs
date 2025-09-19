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
        
        builder.HasOne(x => x.Document)
            .WithMany(x => x.DocumentEquipments)
            .HasForeignKey(x => x.DocumentId);
        
        builder.HasOne(x => x.Equipment)
            .WithMany(x => x.DocumentEquipments)
            .HasForeignKey(x => x.EquipmentId);
    }
}