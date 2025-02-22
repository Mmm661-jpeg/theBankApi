using Inlämningsuppgift3.Core.Services;
using Inlämningsuppgift3.Data.Interfaces;
using theBankApi.Domain.Models;
using theBankApi.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlamningsuppgift3.Test.UsersTesting
{
    public class RegisterTest
    {
        private readonly Mock<IUsersRepo> mockUsersRepo;

        private readonly UsersService usersService;
        public RegisterTest()
        {
            mockUsersRepo = new Mock<IUsersRepo>();


            usersService = new UsersService(mockUsersRepo.Object);
        }

        [Fact]

        public void RegisterSucces()
        {
            var username = "username";
            var password = "password";
            var gender = "male";
            var givename = "User1";
            var surname = "surname";
            var streetaddress = "myadress";
            var city = "Tokyo";
            var zipcode = "123";
            var country = "Japan";
            var countrycode = "1234";
            var birthday = DateOnly.FromDateTime(DateTime.Now);
            var telephonecountrycode = "423";
            var telephonenumber = "123-123-23-23";
            var email = "Random@Email.com";

            var registerRequest = new RegisterRequest
            {
                Username = username,
                Password = password,
                Gender = gender,
                Givenname = givename,
                Surname = surname,
                Streetaddress = streetaddress,
                City = city,
                Zipcode = zipcode,
                Country = country,
                CountryCode = countrycode,
                Birthday = birthday,
                Telephonecountrycode = telephonecountrycode,
                Telephonenumber = telephonenumber,
                Emailaddress = email,
            };



            mockUsersRepo.Setup(repo => repo.Register(It.IsAny<Users>(), It.IsAny<Customers>())).Returns(true);

            var result = usersService.Register(registerRequest);

            Assert.True(result);

            mockUsersRepo.Verify(repo => repo.Register(It.IsAny<Users>(), It.IsAny<Customers>()), Times.Once);





        }

        [Fact]
        public void RegisterFails()
        {
            // Arrange
            var registerRequest = new RegisterRequest
            {
                Username = "existinguser",
                Password = "password123",
                Gender = "Male",
                Givenname = "Jane",
                Surname = "Doe",
                Streetaddress = "456 Elm St",
                City = "Los Angeles",
                Zipcode = "90001",
                Country = "USA",
                CountryCode = "1",
                Birthday = DateOnly.FromDateTime(DateTime.Now),
                Telephonecountrycode = "1",
                Telephonenumber = "987-654-3210",
                Emailaddress = "jane.doe@example.com"
            };

            // Mock
            mockUsersRepo.Setup(repo => repo.Register(It.IsAny<Users>(), It.IsAny<Customers>())).Returns(false);

            // Act
            var result = usersService.Register(registerRequest);

            // Assert
            Assert.False(result);

            // Verify
            mockUsersRepo.Verify(repo => repo.Register(It.IsAny<Users>(), It.IsAny<Customers>()), Times.Once);
        }
    
    }
}
