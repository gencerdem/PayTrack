using AutoMapper;
using PayTrack.Domain.Entities;
using PayTrack.Application.DTOs;


namespace PayTrack.Infrastructure.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserDTO, User>();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<CreateTransactionDTO, Transaction>();
            CreateMap<Transaction, TransactionDTO>().ReverseMap();
        }
    }
}
