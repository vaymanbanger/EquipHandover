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
        CreateMap<DocumentModel, DocumentResponseApiModel>(MemberList.Destination);

        CreateMap<EquipmentRequestApiModel, EquipmentCreateModel>(MemberList.Destination);
        CreateMap<EquipmentModel, EquipmentResponseApiModel>(MemberList.Destination);

        CreateMap<ReceiverRequestApiModel, ReceiverCreateModel>(MemberList.Destination);
        CreateMap<ReceiverModel, ReceiverResponseApiModel>(MemberList.Destination);
        
        CreateMap<SenderRequestApiModel, SenderCreateModel>(MemberList.Destination);
        CreateMap<SenderModel, SenderResponseApiModel>(MemberList.Destination);
    }
}