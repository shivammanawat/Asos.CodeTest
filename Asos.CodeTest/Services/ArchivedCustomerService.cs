using Asos.CodeTest.DataModel;

namespace Asos.CodeTest.Services
{
    public class ArchivedCustomerService
    {
        public Customer GetArchivedCustomer(int customerId)
        {

            Customer archivedCustomer = null;

            var archivedDataService = new ArchivedDataService();
            archivedCustomer = archivedDataService.GetArchivedCustomer(customerId);

            return archivedCustomer;
        }
    }
}
