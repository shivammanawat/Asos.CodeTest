using Asos.CodeTest.DataModel;
using Asos.CodeTest.DataAccess;
using System.Threading.Tasks;

namespace Asos.CodeTest.Services
{

    public class CustomerService
    {
        public async Task<Customer> GetCustomer(int customerId, bool isCustomerArchived)
        {
            ArchivedCustomerService archivedCustomer = new ArchivedCustomerService();
            Customer customer = null;
            CustomerResponse customerResponse = null;

            if (isCustomerArchived)
            {
                return archivedCustomer.GetArchivedCustomer(customerId);
            }
            else
            {

                FailOverEntriesService failOverEntries = new FailOverEntriesService();

                customerResponse = await failOverEntries.GetFailOver(customerId);

                if (customerResponse == null)
                {
                    var dataAccess = new CustomerDataAccess();
                    customerResponse = await dataAccess.LoadCustomerAsync(customerId);
                }

                if (customerResponse.IsArchived)
                {
                    customer = archivedCustomer.GetArchivedCustomer(customerId);
                }
                else
                {
                    customer = customerResponse.Customer;
                }

                return customer;
            }
        }
    }

}
