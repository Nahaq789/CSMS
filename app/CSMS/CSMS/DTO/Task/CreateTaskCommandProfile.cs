using AutoMapper;
using CSMS.Domain.Models;
using CSMS.UseCase.Commands.TaskCommand;

namespace CSMS.DTO.Task;

public class CreateTaskCommandProfile : Profile
{
    public CreateTaskCommandProfile() =>
        CreateMap<TaskDto, CreateTaskCommand>()
            .ForMember(
                dest => dest.TaskId,
                opt =>
                {
                    opt.MapFrom(src => src.TaskId);
                })
            .ForMember(
                dest => dest.TaskName,
                opt =>
                {
                    opt.MapFrom(src => src.TaskName);
                })
            .ForMember(
                dest => dest.Contents,
                opt =>
                {
                    opt.MapFrom(src => src.Contents);
                })
            .ForMember(
                dest => dest.Deadline,
                opt =>
                {
                    opt.MapFrom(src => src.Deadline);
                })
            .ForMember(
                dest => dest.CustomerId,
                opt =>
                {
                    opt.MapFrom(src => src.CustomerId);
                })
            .ForMember(
                dest => dest.ContractId,
                opt =>
                {
                    opt.MapFrom(src => src.ContractId);
                });
}