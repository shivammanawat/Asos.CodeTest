using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using Asos.CodeTest.DataAccess;
using Asos.CodeTest.DataModel;
using Asos.CodeTest.Repository;

namespace Asos.CodeTest.Services
{
    public class FailOverEntriesService
    {
        public async Task<CustomerResponse> GetFailOver(int customerId)
        {
            var failoverEntries = GetFailOverEntries();
            var failedRequests = GetRequestCount(failoverEntries);
            CustomerResponse customerResponse = await GetCustomerFromFailOver(failedRequests, customerId);
            return customerResponse;
        }

        public List<FailoverEntry> GetFailOverEntries()
        {
            var failoverRespository = new FailoverRepository();
            var failoverEntries = failoverRespository.GetFailOverEntries();
            return failoverEntries;
        }

        public int GetRequestCount(List<FailoverEntry> failoverEntries)
        {
            int FailedRequestCount = 0;
            foreach (var failoverEntry in failoverEntries)
            {
                if (failoverEntry.DateTime > DateTime.Now.AddMinutes(-10))
                {
                    FailedRequestCount++;
                }
            }
            return FailedRequestCount;
        }


        public async Task<CustomerResponse> GetCustomerFromFailOver(int failedRequests, int customerId)
        {
            CustomerResponse customerResponse = null;
            bool IsFailover = GetStatus();
            if (failedRequests > 100 && IsFailover)
            {
                customerResponse = await FailoverCustomerDataAccess.GetCustomerById(customerId);
            }
            return customerResponse;
        }

        public bool GetStatus()
        {
            var IsFailoverModeEnabled = ConfigurationManager.AppSettings["IsFailoverModeEnabled"];

            bool status = false;
            if (IsFailoverModeEnabled == "true" || IsFailoverModeEnabled == "True")
            {
                status = true;
            }
            return status;
        }
    }
}
