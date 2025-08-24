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
        CreateMap<DocumentModel, DocumentCreateModel>(MemberList.Destination)
            .ForMember(dest => dest.EquipmentIds,
                opt => opt.MapFrom(src => src.Equipment
                    .Select(x => x.Id)));
        CreateMap<Entities.Document, DocumentModel>(MemberList.Destination)
            .ForMember(dest => dest.Equipment,
                opt => opt.MapFrom(src => src.DocumentEquipments
                    .Select(x => x.Equipment)));

        CreateMap<EquipmentModel, EquipmentCreateModel>(MemberList.Destination);
        CreateMap<Entities.Equipment, EquipmentModel>(MemberList.Destination);

        CreateMap<ReceiverModel, ReceiverCreateModel>(MemberList.Destination);
        CreateMap<Entities.Receiver, ReceiverModel>(MemberList.Destination);

        CreateMap<SenderModel, SenderCreateModel>(MemberList.Destination);
        CreateMap<Entities.Sender, SenderModel>(MemberList.Destination);
    }
}