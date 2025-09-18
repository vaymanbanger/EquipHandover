using AutoMapper;
using EquipHandover.Entities;
using EquipHandover.Repositories.Contracts.Models;
using EquipHandover.Services.Contracts.Models.Document;
using EquipHandover.Services.Contracts.Models.Equipment;
using EquipHandover.Services.Contracts.Models.Receiver;
using EquipHandover.Services.Contracts.Models.Sender;
using EquipHandover.Web.Models.Document;
using EquipHandover.Web.Models.Equipment;
using EquipHandover.Web.Models.Receiver;
using EquipHandover.Web.Models.Sender;

namespace EquipHandover.Web.AutoMappers;

/// <summary>
/// Маппер моделей сервиса
/// </summary>
public class ApiMapper : Profile
{
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ApiMapper"/>
    /// </summary>
    public ApiMapper()
    {
        CreateMap<DocumentCreateApiModel, DocumentCreateModel>(MemberList.Destination);
        CreateMap<DocumentCreateApiModel, DocumentModel>(MemberList.Destination)
            .ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<DocumentModel, DocumentResponseApiModel>(MemberList.Destination).ReverseMap();
        CreateMap<DocumentDbModel, Document>(MemberList.Destination).ReverseMap();

        CreateMap<EquipmentCreateApiModel, EquipmentCreateModel>(MemberList.Destination);
        CreateMap<EquipmentCreateApiModel, EquipmentModel>(MemberList.Destination)
            .ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<EquipmentModel, EquipmentResponseApiModel>(MemberList.Destination).ReverseMap();

        CreateMap<ReceiverCreateApiModel, ReceiverCreateModel>(MemberList.Destination);
        CreateMap<ReceiverCreateApiModel, ReceiverModel>(MemberList.Destination)
            .ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<ReceiverModel, ReceiverResponseApiModel>(MemberList.Destination).ReverseMap();
        CreateMap<ReceiverResponseApiModel, ReceiverCreateModel>(MemberList.Destination).ReverseMap();
        
        CreateMap<SenderCreateApiModel, SenderCreateModel>(MemberList.Destination);
        CreateMap<SenderCreateApiModel, SenderModel>(MemberList.Destination)
            .ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<SenderModel, SenderResponseApiModel>(MemberList.Destination).ReverseMap();
        CreateMap<SenderResponseApiModel, SenderCreateModel>(MemberList.Destination).ReverseMap();
    }
}