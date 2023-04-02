using AutoMapper;
using Cabanoss.Core.Data.Entities;
using Cabanoss.Core.Model.Board;
using Cabanoss.Core.Model.User;
using Cabanoss.Core.Model.Workspace;

namespace Cabanoss.Core.Common
{
    public class CabanossMappingProfile : Profile
    {
        public CabanossMappingProfile()
        {
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
            CreateMap<Workspace, WorkspaceDto>().ReverseMap();
            CreateMap<UpdateWorkspaceDto, Workspace>().ReverseMap();
            CreateMap<CreateBoardDto,Board>().ReverseMap();
        }
    }
}
