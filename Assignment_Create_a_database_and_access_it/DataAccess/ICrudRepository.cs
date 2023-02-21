using Assignment_Create_a_database_and_access_it.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Create_a_database_and_access_it.Repository
{
    public interface ICrudRepository<T, Id>
    {
        public IEnumerable<Customer> GetAll();

        T GetById(Id id);

        bool Add(T entity);

        bool Update(T entity);

    }
}
