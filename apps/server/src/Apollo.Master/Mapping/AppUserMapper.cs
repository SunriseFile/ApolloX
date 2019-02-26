using Apollo.Core.Models;
using Apollo.Master.DTO;

using AutoMapper;

namespace Apollo.Master.Mapping
{
    public class AppUserMapper : Profile
    {
        public AppUserMapper()
        {
            CreateMap<AppUser, AppUserDTO>()
                .ForMember(d => d.UserName,
                           r => r.MapFrom(m => m.DisplayUserName));
        }
    }
}
