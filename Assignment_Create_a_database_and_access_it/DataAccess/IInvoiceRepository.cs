using Assignment_Create_a_database_and_access_it.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Create_a_database_and_access_it.Repository
{
    public interface IInvoiceRepository
    {
        public IEnumerable<CustomerSpender> GetHighestSpenders();
    }
}
