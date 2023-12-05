using AutoMapper;
using CSMS.Models;

namespace CSMS.DTO.Contract;

public class ContractProfile : Profile
{
    public ContractProfile() =>
        CreateMap<ContractDto, ContractModel>()
            .ForMember(
                dest => dest.ContractId,
                opt =>
                {
                    opt.MapFrom(src => src.ContractId);
                }
            )
            .ForMember(
                dest => dest.ContractCode,
                opt =>
                {
                    opt.MapFrom(src => src.ContractCode);
                }
            )
            .ForMember(
                dest => dest.ContractName,
                opt =>
                {
                    opt.MapFrom(src => src.ContractName);
                }
            ).ForMember(
                dest => dest.CustomerId,
                opt =>
                {
                    opt.MapFrom(src => src.CustomerId);
                }
            )
            // .ForMember(
            //     dest => dest._Money,
            //     opt =>
            //     {
            //         opt.MapFrom(src => src.AmountExcludingTax);
            //     }
            // )
            .ForMember(
                dest => dest._TaxRate,
                opt =>
                {
                    opt.MapFrom(src => src.TaxRate);
                }
            );
}