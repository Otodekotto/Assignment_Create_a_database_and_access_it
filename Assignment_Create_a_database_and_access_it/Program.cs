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
            var customerRepositoryv2 = new InvoiceRepository { ConnectionString = GetConnectionString() };
            var allCustomers = customerRepository.GetAll().ToList();
            foreach(var item in allCustomers)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(customerRepository.GetById(6));

            var CustomersByName = customerRepository.GetCustomerByName("m");
            foreach (var item in CustomersByName)
            {
                Console.WriteLine(item);
            }



            var customerPage = customerRepository.GetCustomerPage(5 , 10).ToList();
            foreach (var item in customerPage)
            {
                Console.WriteLine(item);
            }

            customerRepository.Add(new Customer(60, "James", "Bond", "England", "320 59", "333 444 22 11", "Jamesbond@pop.com"));
            allCustomers = customerRepository.GetAll().ToList();
            foreach (var item in allCustomers)
            {
                Console.WriteLine(item);
            }

            customerRepository.Update(new Customer(60,"James" , "Ketchup" , "England" , "320 59" , "333 444 22 11", "Jamesbond@pop.com"));
            allCustomers = customerRepository.GetAll().ToList();
            foreach (var item in allCustomers)
            {
                Console.WriteLine(item);
            }

            var customersPerCountry = customerRepository.GetCustomerPerCountry().ToList();
            foreach (var item in customersPerCountry)
            {
                Console.WriteLine(item);
            }

            var highestSpender = customerRepositoryv2.GetHighestSpenders().ToList();
            foreach (var item in highestSpender)
            {
                Console.WriteLine(item);
            }

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