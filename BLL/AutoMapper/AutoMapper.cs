using AutoMapper;
using BLL.Models;
using DAL.Entities;

namespace BLL.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Customer, CreateCustomerDto>().ReverseMap();
            CreateMap<Customer, UpdateCustomerDto>().ReverseMap();

            CreateMap<Photo, PhotoDto>().ReverseMap();
            CreateMap<Photo, CreatePhotoDto>().ReverseMap();
            CreateMap<Photo, UpdatePhotoDto>().ReverseMap();

            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<Role, CreateRoleDto>().ReverseMap();
            CreateMap<Role, UpdateRoleDto>().ReverseMap();

            CreateMap<TradingPoint, TradingPointDto>().ReverseMap();
            CreateMap<TradingPoint, CreateTradingPointDto>().ReverseMap();
            CreateMap<TradingPoint, UpdateTradingPointDto>().ReverseMap();

            CreateMap<Trip, TripDto>().ReverseMap();
            CreateMap<Trip, CreateTripDto>().ReverseMap();
            CreateMap<Trip, UpdateTripDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();

            CreateMap<Visit, VisitDto>().ReverseMap();
            CreateMap<Visit, CreateVisitDto>().ReverseMap();
            CreateMap<Visit, UpdateVisitDto>().ReverseMap();
            CreateMap<Visit, UpdateVisitWithPhotosDto>().ReverseMap();

            CreateMap<WorkRegion, WorkRegionDto>().ReverseMap();
            CreateMap<WorkRegion, CreateWorkRegionDto>().ReverseMap();
            CreateMap<WorkRegion, UpdateWorkRegionDto>().ReverseMap();
        }
    }
}
