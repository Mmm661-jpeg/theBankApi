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
    public class LoansProfile:Profile
    {
        public LoansProfile()
        {
            CreateMap<Loans, LoansDTO>()
                .ForMember(dest => dest.LoanId, opt => opt.MapFrom(src => src.LoanId))
                .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.AccountId))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Duration))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
        }
    }
}
