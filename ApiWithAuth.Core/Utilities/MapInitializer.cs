using ApiWithAuth.Core.Domain;
using ApiWithAuth.Core.DTOs;
using AutoMapper;

namespace ApiWithAuth.Core.Utilities
{
    public class MapInitializer : Profile
    {
        public Mapper Mapper { get; set; }
        public MapInitializer()
        {
            CreateMap<AddEmployeeDto, Employee>().ReverseMap();
            CreateMap<UpdateEmployeeDto, Employee>().ReverseMap();
            CreateMap<GetEmployeeDto, Employee>().ReverseMap();
        }
    }
}
