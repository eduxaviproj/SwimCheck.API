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
                .ForMember(d => d.Enrolls, o => o.Ignore()); // navigation properties ignored
            // UPDATE
            CreateMap<AthleteUpdateDTO, Athlete>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Enrolls, o => o.Ignore());
            // READ 
            CreateMap<Athlete, AthleteViewDTO>();


            // --------- ENROLL ----------
            //CreateMap<Enroll, EnrollUpdateDTO>().ReverseMap(); // Not needed, if so, delete Enroll and create a new one
            CreateMap<EnrollCreateDTO, Enroll>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Athlete, o => o.Ignore()) // navigation properties ignored
                .ForMember(d => d.Race, o => o.Ignore()); // navigation properties ignored

            CreateMap<Enroll, EnrollViewDTO>()
                .ForMember(d => d.AthleteName, o => o.MapFrom(s => s.Athlete.Name))
                .ForMember(d => d.RaceDisplayName, o => o.MapFrom(s => $"{s.Race.DistanceMeters}m {s.Race.Stroke}"));


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
