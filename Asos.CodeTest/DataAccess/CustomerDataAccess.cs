using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Asos.CodeTest.DataModel;


namespace Asos.CodeTest.DataAccess
{

    using Asos.CodeTest.DataDeserializer;
    public class CustomerDataAccess
    {
        private readonly HttpClient client;

        public CustomerDataAccess()
        {
            var baseUrl = ConfigurationManager.AppSettings["CustomerService.ThirdParty.Http.Connection"];

            client = new HttpClient { BaseAddress = new Uri(baseUrl) };
        }

        public async Task<CustomerResponse> LoadCustomerAsync(int customerId)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, string.Format("/api/customers/{0}", customerId));

            var response = await client.SendAsync(httpRequest);

            var responseContent = await response.Content.ReadAsStringAsync();

            return DataDeserializer.Deserialize<CustomerResponse>(responseContent);
        }
    }
}