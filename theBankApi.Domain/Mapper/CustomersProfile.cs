using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theBankApi.Domain.DTOs;
using theBankApi.Domain.Models;

namespace theBankApi.Domain.Mapper
{
    public class CustomersProfile:Profile
    {
        public CustomersProfile()
        {
            CreateMap<Customers,CustomersDTO>();
        }
    }
}
