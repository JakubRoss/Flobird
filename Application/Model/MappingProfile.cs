using Application.Model.Attachments;
using Application.Model.Board;
using Application.Model.Card;
using Application.Model.Comment;
using Application.Model.Element;
using Application.Model.List;
using Application.Model.Task;
using Application.Model.User;
using AutoMapper;
using Domain.Data.Entities;

namespace Application.Model
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBoardDto, Domain.Data.Entities.Board>().ReverseMap();
            CreateMap<UserLoginDto, Domain.Data.Entities.User>().ReverseMap();

            CreateMap<UserDto, Domain.Data.Entities.User>().ReverseMap();
            CreateMap<UpdateUserDto, Domain.Data.Entities.User>()
                //.ForMember(u => u.PasswordHash, f => f.MapFrom(cud => cud.Password))
                .ReverseMap();
            CreateMap<CreateUserDto, Domain.Data.Entities.User>()
                //.ForMember(u => u.PasswordHash, f => f.MapFrom(cud => cud.Password))
                .ReverseMap();
            CreateMap<ResponseUserDto, Domain.Data.Entities.User>().ReverseMap();
            CreateMap<ResponseBoardDto, Domain.Data.Entities.Board>().ReverseMap();
            CreateMap<ResponseBoardUser, Domain.Data.Entities.User>().ReverseMap();
            CreateMap<UpdateBoardDto, Domain.Data.Entities.Board>().ReverseMap();
            CreateMap<ListDto, Domain.Data.Entities.List>().ReverseMap();
            CreateMap<CardDto, Domain.Data.Entities.Card>().ReverseMap();
            CreateMap<CreateCardDto, Domain.Data.Entities.Card>().ReverseMap();
            CreateMap<TaskDto, Tasks>().ReverseMap();
            CreateMap<ResponseCommentDto, Domain.Data.Entities.Comment>().ReverseMap();
            CreateMap<CommentDto, Domain.Data.Entities.Comment>().ReverseMap();
            CreateMap<AttachmentResponseDto, Attachment>().ReverseMap();
            CreateMap<AttachmentDto, Attachment>().ReverseMap();
            CreateMap<ResponseElementDto, Domain.Data.Entities.Element>().ReverseMap();
            CreateMap<ElementDto, Domain.Data.Entities.Element>().ReverseMap();
            CreateMap<UpdateElementDto, Domain.Data.Entities.Element>().ReverseMap();
            CreateMap<ResponseTaskDto, Tasks>().ReverseMap();
            CreateMap<ElementCheckDto, Domain.Data.Entities.Element>().ReverseMap();
        }
    }
}
