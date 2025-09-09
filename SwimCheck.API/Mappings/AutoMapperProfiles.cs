using AutoMapper;
using SwimCheck.API.Models.Domain;
using SwimCheck.API.Models.Domain.Enum;
using SwimCheck.API.Models.DTOs.AthleteDTOs;
using SwimCheck.API.Models.DTOs.EnrollDTOs;
using SwimCheck.API.Models.DTOs.RaceDTOs;

namespace SwimCheck.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // --------- ATHLETE ----------
            // CREATE
            CreateMap<AthleteCreateDTO, Athlete>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Enrolls, o => o.Ignore());
            // UPDATE
            CreateMap<AthleteUpdateDTO, Athlete>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Enrolls, o => o.Ignore());
            // READ 
            CreateMap<Athlete, AthleteViewDTO>();


            // --------- ENROLL ----------
            CreateMap<Enroll, EnrollUpdateDTO>().ReverseMap();
            CreateMap<Enroll, EnrollCreateDTO>().ReverseMap();
            CreateMap<Enroll, EnrollViewDTO>().ReverseMap();


            // --------- RACE ----------
            // CREATE
            CreateMap<RaceCreateDTO, Race>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Enrolls, o => o.Ignore())
                .ForMember(d => d.Stroke, o => o.MapFrom(s => Enum.Parse<Stroke>(s.Stroke, true))); // string to enum
            // UPDATE
            CreateMap<RaceUpdateDTO, Race>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Enrolls, o => o.Ignore())
                .ForMember(d => d.Stroke, o => o.MapFrom(s => Enum.Parse<Stroke>(s.Stroke, true)));
            // READ
            CreateMap<Race, RaceViewDTO>();// sem ForMember; DisplayName é calculado no DTO)

        }
    }
}
