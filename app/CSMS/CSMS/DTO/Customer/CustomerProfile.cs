using AutoMapper;
using CSMS.Models;

namespace CSMS.DTO;

public class CustomerProfile : Profile
{
    public CustomerProfile() =>
        CreateMap<CustomerDto, CustomerModel>()
            .ForMember(
                dest => dest.CustomerId,
                opt =>
                {
                    opt.MapFrom(src => src);
                })
            .ForMember(
                dest => dest.Name,
                opt =>
                {
                    opt.MapFrom(src => src.Name);
                })
            .ForMember(
                dest => dest.Email,
                opt =>
                {
                    opt.MapFrom(src => src.Email);
                })
            .ForMember(
                dest => dest.Age,
                opt =>
                {
                    opt.MapFrom(src => src.Age);
                });
}