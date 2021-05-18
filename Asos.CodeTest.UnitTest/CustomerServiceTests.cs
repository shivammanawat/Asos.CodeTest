using Asos.CodeTest.Services;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Asos.CodeTest.UnitTest
{

   [TestFixture]
   public class CustomerServiceTests
    {
        private CustomerService _customerService;
        
        [SetUp]
        public void Setup()
        {
            _customerService = new CustomerService();
        }

        [TearDown]
        public void TearDown()
        {
            _customerService = null;
        }

        public async Task GetCustomer()
        {
            var result = await _customerService.GetCustomer(45, true);

            Assert.That(result != null);
        }
    }
}
