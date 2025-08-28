using AutoMapper;
using EquipHandover.Entities;
using EquipHandover.Repositories.Contracts.Models;
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
                    .Select(x => x.Id))).ReverseMap();
        CreateMap<Document, DocumentModel>(MemberList.Destination)
            .ForMember(dest => dest.Equipment,
                opt => opt.MapFrom(src => src.DocumentEquipments
                    .Select(x => x.Equipment))).ReverseMap();
        CreateMap<DocumentDbModel, DocumentModel>(MemberList.Destination).ReverseMap();

        CreateMap<EquipmentModel, EquipmentCreateModel>(MemberList.Destination).ReverseMap();
        CreateMap<Equipment, EquipmentModel>(MemberList.Destination).ReverseMap();

        CreateMap<ReceiverModel, ReceiverCreateModel>(MemberList.Destination).ReverseMap();
        CreateMap<Receiver, ReceiverModel>(MemberList.Destination).ReverseMap();

        CreateMap<SenderModel, SenderCreateModel>(MemberList.Destination).ReverseMap();
        CreateMap<Sender, SenderModel>(MemberList.Destination).ReverseMap();
    }
}