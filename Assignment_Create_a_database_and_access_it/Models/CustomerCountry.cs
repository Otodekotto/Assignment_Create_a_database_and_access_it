using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Create_a_database_and_access_it.Models
{
    public readonly record struct CustomerCountry(

        string Country,
        int Count
    );
}
