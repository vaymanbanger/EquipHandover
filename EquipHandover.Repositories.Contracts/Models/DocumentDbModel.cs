using EquipHandover.Entities;

namespace EquipHandover.Repositories.Contracts.Models;

/// <summary>
/// Модель документа с заполненными связанными сущностями
/// </summary>
public class DocumentDbModel
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Дата аренды оборудования
    /// </summary>
    public DateOnly RentalDate { get; set; }
    
    /// <summary>
    /// Номер подписания договора
    /// </summary>
    public DateOnly SignatureNumber { get; set; }
    
    /// <summary>
    /// Город
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// <see cref="Sender"/>
    /// </summary>
    public required Sender Sender { get; set; }
    
    /// <summary>
    /// <see cref="Receiver"/>
    /// </summary>
    public required Receiver Receiver { get; set; }
    
    /// <summary>
    /// <see cref="DocumentEquipment"/>
    /// </summary>
    public required IEnumerable<Equipment> Equipment { get; set; }
}