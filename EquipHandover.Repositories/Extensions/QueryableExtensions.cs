using EquipHandover.Entities;
using EquipHandover.Repositories.Contracts.Models;

namespace EquipHandover.Repositories.Extensions;

/// <summary>
/// Расширения для IQueryable
/// </summary>
public static class QueryableExtensions
{
    /// <summary>
    /// Подгружает полностью модель
    /// </summary>
    public static IQueryable<DocumentDbModel> SelectFullModel(this IQueryable<Document> query)
        => query.Select(x => new DocumentDbModel
        {
            SignatureNumber = x.SignatureNumber,
            RentalDate = x.RentalDate,
            Receiver = x.Receiver!,
            Sender = x.Sender!,
            Equipment = x.DocumentEquipments
                .Where(y => y.DeletedAt == null)
                .Select(y => y.Equipment!),
            City = x.City,
            Id = x.Id
        });
}