using AutoMapper;
using theBankApi.Domain.DTOs;
using theBankApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theBankApi.Domain.Mapper
{
    public class AccountsProfile:Profile
    {
        public AccountsProfile()
        {
            CreateMap<Accounts, AccountsDTO>()
                .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.AccountId))
                .ForMember(dest => dest.Frequency, opt => opt.MapFrom(src => src.Frequency))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))
                 .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Balance))
                  .ForMember(dest => dest.AccountTypesId, opt => opt.MapFrom(src => src.AccountTypes));
        }
    }
}
