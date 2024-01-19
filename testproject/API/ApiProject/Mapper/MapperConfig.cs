using Api.ViewModels;
using AutoMapper;
using DAL.enities;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Api.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Person, PersonViewModel>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => new DateOnly(src.DateOfBirth.Year, src.DateOfBirth.Month, src.DateOfBirth.Day)));
        }
    }
}
