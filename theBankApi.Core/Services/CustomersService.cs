using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theBankApi.Core.Interfaces;
using theBankApi.Data.Interfaces;
using theBankApi.Domain.DTOs;
using theBankApi.Domain.Models;

namespace theBankApi.Core.Services
{
    public class CustomersService:ICustomersService
    {
        private readonly ICustomersRepo customersRepo;
        private readonly ILogger<CustomersService> logger;
        private readonly IMapper mapper;

        public CustomersService(ICustomersRepo customersRepo, ILogger<CustomersService> logger,IMapper mapper)
        {
            this.customersRepo = customersRepo;
            this.logger = logger;
            this.mapper = mapper;
        }

        public CustomersDTO GetCustomerById(int customerid)
        {
            try
            {
                var dbCustomer = customersRepo.GetCustomerById(customerid);
                var result = mapper.Map<CustomersDTO>(dbCustomer);
                return result;
            }
            catch(Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public HashSet<CustomersDTO> GetCustomers(int pageNumber)
        {
            var dbCustomers = customersRepo.GetCustomers(pageNumber);
            var result = mapper.Map<HashSet<CustomersDTO>>(dbCustomers);
            return result;
        }

        public HashSet<CustomersDTO> SearchCustomers(string keyword, int pageNumber)
        {
            var dbSearchresult = customersRepo.SearchCustomers(keyword, pageNumber);

            var result = mapper.Map<HashSet<CustomersDTO>>(dbSearchresult);

            return result;

            
        }
    }
}
