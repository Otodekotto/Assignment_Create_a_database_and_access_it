using Assignment_Create_a_database_and_access_it.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Create_a_database_and_access_it.Repository
{
    internal interface ICustomerRepository : ICrudRepository<Customer ,int>
    {
        public IEnumerable<Customer> GetCustomerByName(string name);
        public IEnumerable<Customer> GetCustomerPage(int offset , int limit);

        public IEnumerable<CustomerCountry> GetCustomerPerCountry();
    }
}
