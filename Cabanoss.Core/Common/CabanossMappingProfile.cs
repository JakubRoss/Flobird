using AutoMapper;
using Cabanoss.Core.Data.Entities;
using Cabanoss.Core.Model.Board;
using Cabanoss.Core.Model.Card;
using Cabanoss.Core.Model.List;
using Cabanoss.Core.Model.Task;
using Cabanoss.Core.Model.User;
using Cabanoss.Core.Model.Workspace;

namespace Cabanoss.Core.Common
{
    public class CabanossMappingProfile : Profile
    {
        public CabanossMappingProfile()
        {
            CreateMap<Workspace, WorkspaceDto>().ReverseMap();
            CreateMap<UpdateWorkspaceDto, Workspace>().ReverseMap();
            CreateMap<CreateBoardDto, Board>().ReverseMap();
            CreateMap<UserLoginDto, User>().ReverseMap();

            CreateMap<UserDto, User>()
                .ForMember(ud => ud.Login, f => f.MapFrom(u => u.Login))
                .ForMember(ud => ud.Email, f => f.MapFrom(u => u.Email))
                .ForMember(ud => ud.PasswordHash, f => f.MapFrom(u => u.PasswordHash))
                .ForMember(ud => ud.CreatedAt, f => f.MapFrom(u => u.CreatedAt))
                .ForMember(ud => ud.UpdatedAt, f => f.MapFrom(u => u.UpdatedAt))
                .ReverseMap();
            CreateMap<UpdateUserDto,User>()
                .ForMember(u => u.PasswordHash, f=>f.MapFrom(cud=>cud.Password))
                .ReverseMap();
            CreateMap<CreateUserDto,User>()
                .ForMember(u => u.PasswordHash, f => f.MapFrom(cud => cud.Password))
                .ReverseMap();
            CreateMap<ResponseUserDto,User>().ReverseMap();
            CreateMap<ResponseBoardDto, Board>().ReverseMap();
            CreateMap<ResponseBoardUser, User>().ReverseMap();
            CreateMap<UpdateBoardDto, Board>().ReverseMap();
            CreateMap<ListDto, List>().ReverseMap();
            CreateMap<CardDto, Card>().ReverseMap();
            CreateMap<CreateCardDto, Card>().ReverseMap();
            CreateMap<TaskDto, Tasks>().ReverseMap();
        }
    }
}
