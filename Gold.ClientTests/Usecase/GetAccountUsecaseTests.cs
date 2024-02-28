using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gold.Usecase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Gold.Usecase.GetAccountUsecase;
using Gold.Service;

namespace Gold.Usecase.Tests
{
    [TestClass()]
    public class GetAccountUsecaseTests
    {
        public class MockGraphQlService : GraphQlService
        {
            public override async Task<string> Execute(string qry)
            {
                return "{\"data\":{\"account\":{\"id\":42}}}";
            }
        }


        [TestMethod()]
        public async Task ExecuteTestAsync()
        {
            // Arrange
            GraphQlService mockGraphQlService;
            MockGraphQlService mock = new MockGraphQlService();
            mockGraphQlService = mock as GraphQlService;
            GetAccountUsecase usecase = new GetAccountUsecase(mockGraphQlService);

            var expected = new Account();
            expected.id = 42;

            // Act
            var resultat = await usecase.Execute(1);

            // Assert
            Assert.AreEqual(expected.id, resultat.id, "Résultat incorrect");
        }
    }
}