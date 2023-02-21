using Assignment_Create_a_database_and_access_it.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Create_a_database_and_access_it.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public string ConnectionString { get; set; } = string.Empty;

        /// <summary>
        /// Adds a customer to the database.
        /// </summary>
        /// <param name="entity">A customer entity</param>
        /// <returns>Boolean - True if successful</returns>
        public bool Add(Customer entity)
        {
            try
            {
                using var connection = new SqlConnection(ConnectionString);
                connection.Open();
                var sql = "INSERT INTO Customer (FirstName, LastName, Country, PostalCode, Phone, Email) VALUES (@FirstName, @LastName , @Country , @PostalCode, @Phone, @Email)";
                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@FirstName", entity.FirstName);
                command.Parameters.AddWithValue("@LastName", entity.LastName);
                command.Parameters.AddWithValue("@Country", entity.Country);
                command.Parameters.AddWithValue("@PostalCode", entity.PostalCode);
                command.Parameters.AddWithValue("@Phone", entity.PhoneNumber);
                command.Parameters.AddWithValue("@Email", entity.Email);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Updates a customer in the database.
        /// </summary>
        /// <param name="entity">A customer entity</param>
        /// <returns>Boolean - True if successful</returns>
        public bool Update(Customer entity)
        {
            try
            {
                using var connection = new SqlConnection(ConnectionString);
                connection.Open();
                var sql = "UPDATE Customer " +
                    "SET FirstName = @FirstName , " +
                    "LastName = @LastName , " +
                    "Country = @Country, " +
                    "PostalCode  = @PostalCode, " +
                    "Phone = @Phone, " +
                    "Email = @Email" +
                    " WHERE CustomerId = @CustomerId";

                using var command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@CustomerId", entity.Id);
                command.Parameters.AddWithValue("@FirstName", entity.FirstName);
                command.Parameters.AddWithValue("@LastName", entity.LastName);
                command.Parameters.AddWithValue("@Country", entity.Country);
                command.Parameters.AddWithValue("@PostalCode", entity.PostalCode);
                command.Parameters.AddWithValue("@Phone", entity.PhoneNumber);
                command.Parameters.AddWithValue("@Email", entity.Email);
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Gets all customers in the database.
        /// </summary>
        /// <returns>IEnumerable containing all the customers</returns>
        public IEnumerable<Customer> GetAll()
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            var sql = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer";
            using var command = new SqlCommand(sql, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                yield return new Customer(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                GetString(reader, 3),
                GetString(reader, 4),
                GetString(reader, 5),
                reader.GetString(6)
                );
            }
        }
        /// <summary>
        /// Gets a customer by given id.
        /// </summary>
        /// <param name="id">The given id.</param>
        /// <returns>A customer</returns>
        public Customer GetById(int id)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            var sql = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer WHERE CustomerId = @CustomerId";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@CustomerId", id);
            using var reader = command.ExecuteReader();

            var result = new Customer();

            while (reader.Read())
            {
                result = new Customer(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                GetString(reader, 3),
                GetString(reader, 4),
                GetString(reader, 5),
                reader.GetString(6)
                );
            }
            return result;
        }
        /// <summary>
        /// Gets customer or customers based on string matching first name.
        /// </summary>
        /// <param name="name">The string </param>
        /// <returns>IEnumerable containing the customers.</returns>
        public IEnumerable<Customer> GetCustomerByName(string name)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            var sql = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer WHERE FirstName LIKE @CustomerName";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@CustomerName", name+"%");
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                yield return new Customer(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                GetString(reader, 3),
                GetString(reader, 4),
                GetString(reader, 5),
                reader.GetString(6)
                );
            }
        }
        /// <summary>
        /// Gets a page of customers based on specific limit and offset.
        /// </summary>
        /// <param name="offset">The offset, how many rows skipped.</param>
        /// <param name="limit">The limit of how many records per page.</param>
        /// <returns></returns>
        public IEnumerable<Customer> GetCustomerPage(int offset , int limit)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            var sql = "SELECT CustomerId, FirstName, LastName, Country, PostalCode, Phone, Email FROM Customer ORDER BY CustomerId OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Offset", offset);
            command.Parameters.AddWithValue("@Limit", limit);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                yield return new Customer(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                GetString(reader, 3),
                GetString(reader, 4),
                GetString(reader, 5),
                reader.GetString(6)
                );
            }
        }
       
        private string? GetString(SqlDataReader reader, int i)
        {
            if (!reader.IsDBNull(i))
            {
                return reader.GetString(i);
            }
            else
                return null;
        }
        /// <summary>
        /// Gets the number of customers from each country in descending order.
        /// </summary>
        /// <returns>IEnumerable containing customerCountry objects.</returns>
        public IEnumerable<CustomerCountry> GetCustomerPerCountry()
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            var sql = "SELECT Country, Count(*) as CustomerCount FROM Customer GROUP BY Country ORDER BY CustomerCount DESC ";

            using var command = new SqlCommand(sql, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                yield return new CustomerCountry(
                    reader.GetString(0),
                    reader.GetInt32(1)
                    );
            }

        }
        /// <summary>
        /// Gets a specific customers favorite genre:
        /// </summary>
        /// <param name="id">Id of the customer.</param>
        /// <returns>IEnumerable containig genre or genres in case of a tie.</returns>
        public IEnumerable<CustomerGenre> GetFavoriteGenre(int id)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            var sql = "SELECT TOP 1 WITH TIES Genre.Name," +
                " Count(*) as CustomerCount FROM (Customer" +
                " INNER JOIN Invoice" +
                " ON Customer.CustomerId = Invoice.CustomerId" +
                " INNER JOIN InvoiceLine" +
                " ON Invoice.InvoiceId = InvoiceLine.InvoiceId" +
                " INNER JOIN Track" +
                " ON InvoiceLine.TrackId = track.TrackId" +
                " INNER JOIN Genre" +
                " ON Track.GenreId = Genre.GenreId)" +
                " WHERE Customer.CustomerId = @CustomerId " +
                " GROUP BY Genre.Name" +
                " ORDER BY CustomerCount DESC ";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@CustomerId", id );
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                yield return new CustomerGenre(
                    reader.GetString(0)
                );
            }
        }
    } 
}
