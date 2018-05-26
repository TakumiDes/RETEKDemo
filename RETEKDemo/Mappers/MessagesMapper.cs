using AutoMapper;
using RETEKDemo.Models;
using RETEKDemo.Models.Requesst;

namespace RETEKDemo.Mappers
{
    public class MessagesMapper: Profile
    {
        public MessagesMapper() {
            CreateMap<Messages, MessageRequestDto>()
                .ForMember(o => o.Message, opt => opt.MapFrom(src => (string)src.Msg))
                .ForMember(o => o.ParentId, opt => opt.MapFrom(src => src.Parent_Id.HasValue ? src.Parent_Id : null));
            CreateMap<MessageRequestDto, Messages>()
                .ForMember(o => o.Msg, opt => opt.MapFrom(src => (string)src.Message))
                .ForMember(o => o.Parent_Id, opt => opt.MapFrom(src => src.ParentId.HasValue ? src.ParentId : null));

            CreateMap<Messages, MessageResponseDto>()
                .ForMember(o => o.Id, opt => opt.MapFrom(src => (int)src.Id))
                .ForMember(o => o.Message, opt => opt.MapFrom(src => (string)src.Msg))
                .ForMember(o => o.ParentId, opt => opt.MapFrom(src => src.Parent_Id.HasValue ? src.Parent_Id : null));
            CreateMap<MessageResponseDto, Messages>()
                .ForMember(o => o.Id, opt => opt.MapFrom(src => (int)src.Id))
                .ForMember(o => o.Msg, opt => opt.MapFrom(src => (string)src.Message))
                .ForMember(o => o.Parent_Id, opt => opt.MapFrom(src => src.ParentId.HasValue ? src.ParentId : null));

        }
    }
}