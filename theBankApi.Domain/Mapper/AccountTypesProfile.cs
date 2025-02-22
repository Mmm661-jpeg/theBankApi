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
    public class AccountTypesProfile:Profile
    {
        public AccountTypesProfile()
        {
            CreateMap<AccountTypes, AccountTypesDTO>()
                .ForMember(dest => dest.AccountTypeId, opt => opt.MapFrom(src => src.AccountTypeId))
                .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.TypeName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}
