using Moq;
using MortgageCalculator.Api.Controllers;
using MortgageCalculator.Api.Helpers;
using MortgageCalculator.Api.Services;
using MortgageCalculator.Dto;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Web.Http.Filters;

namespace MortgageCalculator.UnitTests.API
{
    class MortgageCalculatorApiTest
    {

        [TestFixture]
        public class MortgageAPITest
        {
            private List<Dto.Mortgage> Setup()
            {
                var Mortgages = new List<Dto.Mortgage>
    {
        new Dto.Mortgage
        {
            MortgageId = 1,
            Name = "Fixed Home Loan (Interest Only)",
            MortgageType = MortgageType.Fixed,
            CancellationFee = Convert.ToDecimal(259.99),
            EstablishmentFee = Convert.ToDecimal(199.99),
            InterestRate = Convert.ToDecimal(4.99)
        },
        new Dto.Mortgage
        {
            MortgageId = 2,
            Name = "Fixed Home Loan (Principal and Interest)",
            MortgageType = MortgageType.Fixed,
            CancellationFee = Convert.ToDecimal(259.99),
            EstablishmentFee = Convert.ToDecimal(199.99),
            InterestRate = Convert.ToDecimal(4.81)
        },
        new Dto.Mortgage
        {
            MortgageId = 3,
            Name = "Fixed Investment Loan (Interest Only)",
            MortgageType = MortgageType.Fixed,
            CancellationFee = Convert.ToDecimal(259.99),
            EstablishmentFee = Convert.ToDecimal(199.99),
            InterestRate = Convert.ToDecimal(5.19)
        }
    };
                return Mortgages;
            }

            [Test]
            public void GetMortgagesTest()
            {
                var lstmortgage = Setup();
                var mortgageService = new Mock<IMortgageService>();
                mortgageService.Setup(service => service.GetAllMortgages())
                .Returns(lstmortgage);
                MortgageController controller = new MortgageController(mortgageService.Object);
                var result = controller.Get() as List<Dto.Mortgage>;
                Assert.IsNotNull(result);
                Assert.IsTrue(lstmortgage.Count == 3);


              
            }
        }

    }
}
