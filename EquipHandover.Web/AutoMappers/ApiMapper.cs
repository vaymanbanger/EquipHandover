using AutoMapper;
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
        CreateMap<DocumentRequestApiModel, DocumentCreateModel>(MemberList.Destination);
        CreateMap<DocumentRequestApiModel, DocumentModel>(MemberList.Destination)
            .ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<DocumentModel, DocumentResponseApiModel>(MemberList.Destination).ReverseMap();

        CreateMap<EquipmentRequestApiModel, EquipmentCreateModel>(MemberList.Destination);
        CreateMap<EquipmentRequestApiModel, EquipmentModel>(MemberList.Destination)
            .ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<EquipmentModel, EquipmentResponseApiModel>(MemberList.Destination).ReverseMap();

        CreateMap<ReceiverRequestApiModel, ReceiverCreateModel>(MemberList.Destination);
        CreateMap<ReceiverRequestApiModel, ReceiverModel>(MemberList.Destination)
            .ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<ReceiverModel, ReceiverResponseApiModel>(MemberList.Destination).ReverseMap();
        CreateMap<ReceiverResponseApiModel, ReceiverCreateModel>(MemberList.Destination).ReverseMap();
        
        CreateMap<SenderRequestApiModel, SenderCreateModel>(MemberList.Destination);
        CreateMap<SenderRequestApiModel, SenderModel>(MemberList.Destination)
            .ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<SenderModel, SenderResponseApiModel>(MemberList.Destination).ReverseMap();
        CreateMap<SenderResponseApiModel, SenderCreateModel>(MemberList.Destination).ReverseMap();
    }
}