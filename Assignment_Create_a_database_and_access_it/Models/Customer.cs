using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Create_a_database_and_access_it.Models
{
    public readonly record struct Customer (
        int Id, 
        string FirstName,
        string LastName,
        string? Country,
        string? PostalCode,
        string? PhoneNumber,
        string? Email
        );
}
