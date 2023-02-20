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
            //var list = customerRepository.GetAll().ToList();
            //foreach(var item in list)
            //{
            //    Console.WriteLine(item);
            //}

            //Console.WriteLine(customerRepository.GetById(6));
            //var list = customerRepository.GetCustomerByName("m");
            //foreach (var item in list)
            //{
            //    Console.WriteLine(item);
            //}

            //var list = customerRepository.GetCustomerPage(5 , 10).ToList();
            //foreach (var item in list)
            //{
            //    Console.WriteLine(item);
            //}

            //customerRepository.Update(new Customer(60,"James" , "Ketchup" , "England" , "320 59" , "333 444 22 11", "Jamesbond@pop.com"));

            //var list = customerRepository.GetAll().ToList();
            //foreach (var item in list)
            //{
            //    Console.WriteLine(item);
            //}

            var list = customerRepositoryv2.GetHighestSpenders();
            foreach (var item in list)
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