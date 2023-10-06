using AutoMapper;
using Todo.Repositories.Entities;

namespace SportHub.API
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<ToDo, ToDo>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}

