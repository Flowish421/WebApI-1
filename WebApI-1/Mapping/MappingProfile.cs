using AutoMapper;
using WebApI_1.Models;
using WebApI_1.DTOs;

namespace WebApI_1.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateWorkplaceDto, Workplace>();
            CreateMap<Workplace, GetWorkplaceDto>();
        }
    }
}

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateWorkplaceDto, Workplace>();
        CreateMap<Workplace, GetWorkplaceDto>();
        CreateMap<Workplace, WorkplaceDto>();
    }
}
