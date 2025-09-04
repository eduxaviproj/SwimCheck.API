using AutoMapper;
using SwimCheck.API.Models.Domain;
using SwimCheck.API.Models.DTOs;

namespace SwimCheck.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Athlete
            CreateMap<Athlete, AthleteCreateDTO>().ReverseMap();
            CreateMap<Athlete, AthleteReadDTO>();

            //Enroll
            CreateMap<Enroll, EnrollReadDTO>().ReverseMap();
            CreateMap<Enroll, EnrollCreateDTO>().ReverseMap();

            //Race
            CreateMap<Race, RaceCreateDTO>()
                // entity -> dto (enum -> string)
                .ForMember(d => d.Stroke, o => o.MapFrom(s => s.Stroke.ToString())).ReverseMap()
                // dto -> entity (string -> enum)
                .ForMember(d => d.Stroke, o => o.MapFrom(s => Enum.Parse<Stroke>(s.Stroke, true)));

            CreateMap<Race, RaceReadDTO>()
                .ForMember(dest => dest.Stroke, opt => opt.MapFrom(src => src.Stroke.ToString()))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => $"{src.DistanceMeters}m {src.Stroke}"));
        }
    }
}
