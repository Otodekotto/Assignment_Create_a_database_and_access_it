using Assignment_Create_a_database_and_access_it.Models;
using Assignment_Create_a_database_and_access_it.Repository;
using Microsoft.Data.SqlClient;

namespace Assignment_Create_a_database_and_access_it
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var customerRepository = new CustomerRepository { ConnectionString = GetConnectionString() };
            var invoiceRepository = new InvoiceRepository { ConnectionString = GetConnectionString() };
            // Part 1
            var allCustomers = customerRepository.GetAll().ToList();
            foreach(var item in allCustomers)
            {
                Console.WriteLine(item);
            }
            // Part 2
            Console.WriteLine(customerRepository.GetById(6));
            // Part 3
            var CustomersByName = customerRepository.GetCustomerByName("m");
            foreach (var item in CustomersByName)
            {
                Console.WriteLine(item);
            }


            // Part 4
            var customerPage = customerRepository.GetCustomerPage(5 , 10).ToList();
            foreach (var item in customerPage)
            {
                Console.WriteLine(item);
            }
            // Part 5
            customerRepository.Add(new Customer(60, "James", "Bond", "England", "320 59", "333 444 22 11", "Jamesbond@pop.com"));
            allCustomers = customerRepository.GetAll().ToList();
            foreach (var item in allCustomers)
            {
                Console.WriteLine(item);
            }
            // Part 6
            customerRepository.Update(new Customer(60,"James" , "Ketchup" , "England" , "320 59" , "333 444 22 11", "Jamesbond@pop.com"));
            allCustomers = customerRepository.GetAll().ToList();
            foreach (var item in allCustomers)
            {
                Console.WriteLine(item);
            }
            // Part 7
            var customersPerCountry = customerRepository.GetCustomerPerCountry().ToList();
            foreach (var item in customersPerCountry)
            {
                Console.WriteLine(item);
            }
            // Part 8
            var highestSpender = invoiceRepository.GetHighestSpenders().ToList();
            foreach (var item in highestSpender)
            {
                Console.WriteLine(item);
            }
            // Part 9
            var customerFavoriteGenre = customerRepository.GetFavoriteGenre(12);
            foreach (var item in customerFavoriteGenre)
            {
                Console.WriteLine(item);
            }
        }

        static string GetConnectionString()
        {
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = "(LocalDb)\\MSSQLLocalDB",
                InitialCatalog = "Chinook",
                IntegratedSecurity = true,
                TrustServerCertificate = true
            };

            return builder.ConnectionString;
        }
    }
}