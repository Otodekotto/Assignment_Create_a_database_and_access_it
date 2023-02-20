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
    } 
}
