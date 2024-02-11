using Application.Data.Entities;
using Application.Model.Attachments;
using Application.Model.Board;
using Application.Model.Card;
using Application.Model.Comment;
using Application.Model.Element;
using Application.Model.List;
using Application.Model.Task;
using Application.Model.User;
using Application.Model.Workspace;
using AutoMapper;

namespace Application.Model
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Data.Entities.Workspace, WorkspaceDto>().ReverseMap();
            CreateMap<UpdateWorkspaceDto, Data.Entities.Workspace>().ReverseMap();
            CreateMap<CreateBoardDto, Data.Entities.Board>().ReverseMap();
            CreateMap<UserLoginDto, Data.Entities.User>().ReverseMap();

            CreateMap<UserDto, Data.Entities.User>()
                .ForMember(ud => ud.Login, f => f.MapFrom(u => u.Login))
                .ForMember(ud => ud.Email, f => f.MapFrom(u => u.Email))
                .ForMember(ud => ud.PasswordHash, f => f.MapFrom(u => u.PasswordHash))
                .ForMember(ud => ud.CreatedAt, f => f.MapFrom(u => u.CreatedAt))
                .ForMember(ud => ud.UpdatedAt, f => f.MapFrom(u => u.UpdatedAt))
                .ReverseMap();
            CreateMap<UpdateUserDto, Data.Entities.User>()
                .ForMember(u => u.PasswordHash, f => f.MapFrom(cud => cud.Password))
                .ReverseMap();
            CreateMap<CreateUserDto, Data.Entities.User>()
                .ForMember(u => u.PasswordHash, f => f.MapFrom(cud => cud.Password))
                .ReverseMap();
            CreateMap<ResponseUserDto, Data.Entities.User>().ReverseMap();
            CreateMap<ResponseBoardDto, Data.Entities.Board>().ReverseMap();
            CreateMap<ResponseBoardUser, Data.Entities.User>().ReverseMap();
            CreateMap<UpdateBoardDto, Data.Entities.Board>().ReverseMap();
            CreateMap<ListDto, Data.Entities.List>().ReverseMap();
            CreateMap<CardDto, Data.Entities.Card>().ReverseMap();
            CreateMap<CreateCardDto, Data.Entities.Card>().ReverseMap();
            CreateMap<TaskDto, Tasks>().ReverseMap();
            CreateMap<ResponseCommentDto, Data.Entities.Comment>().ReverseMap();
            CreateMap<CommentDto, Data.Entities.Comment>().ReverseMap();
            CreateMap<AttachmentResponseDto, Attachment>().ReverseMap();
            CreateMap<AttachmentDto, Attachment>().ReverseMap();
            CreateMap<ResponseElementDto, Data.Entities.Element>().ReverseMap();
            CreateMap<ElementDto, Data.Entities.Element>().ReverseMap();
            CreateMap<UpdateElementDto, Data.Entities.Element>().ReverseMap();
            CreateMap<ResponseTaskDto, Tasks>().ReverseMap();
            CreateMap<ElementCheckDto , Data.Entities.Element>().ReverseMap();
        }
    }
}
