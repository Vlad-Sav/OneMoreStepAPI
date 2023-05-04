using AutoMapper;
using OneMoreStepAPI.Models;
using OneMoreStepAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace OneMoreStepAPI.Utils
{
    public class OMSAutoMapper: Profile
    {
        public OMSAutoMapper()
        {
            CreateMap<RouteSaveRequest, Route>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.RouteTitle))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.RouteDescription))
                .ForMember(dest => dest.CoordinatesJSON, opt => opt.MapFrom(src => JsonSerializer.Serialize<List<LatLng>>(src.Coordinates, null)));

            CreateMap<User, UserProfileResponse>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username));

            CreateMap<Route, RouteResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}
