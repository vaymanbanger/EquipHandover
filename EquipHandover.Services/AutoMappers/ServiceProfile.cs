using AutoMapper;
using EquipHandover.Services.Contracts.Models.Document;
using EquipHandover.Services.Contracts.Models.Equipment;
using EquipHandover.Services.Contracts.Models.Receiver;
using EquipHandover.Services.Contracts.Models.Sender;

namespace EquipHandover.Services.AutoMappers;

/// <summary>
/// Маппер моделей сервиса
/// </summary>
public class ServiceProfile : Profile
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ServiceProfile"/>
    /// </summary>
    public ServiceProfile()
    {
        CreateMap<Entities.Document, DocumentModel>(MemberList.Destination);
        CreateMap<Entities.Equipment, EquipmentModel>(MemberList.Destination);
        CreateMap<Entities.Receiver, ReceiverModel>(MemberList.Destination);
        CreateMap<Entities.Sender, SenderModel>(MemberList.Destination);
    }
}