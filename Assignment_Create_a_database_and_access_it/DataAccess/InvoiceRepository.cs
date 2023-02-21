using Assignment_Create_a_database_and_access_it.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_Create_a_database_and_access_it.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        public string ConnectionString { get; set; } = string.Empty;

        /// <summary>
        /// Gets the highest spenders based on invoice records.
        /// </summary>
        /// <returns>IEnumerable containing customerSpender object.</returns>
        public IEnumerable<CustomerSpender> GetHighestSpenders()
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            var sql = "SELECT CustomerId, SUM(Total) as TotalSpent FROM Invoice GROUP BY CustomerId ORDER BY TotalSpent DESC ";

            using var command = new SqlCommand(sql, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                yield return new CustomerSpender(
                    reader.GetInt32(0),
                    reader.GetDecimal(1)
                    );
            }
        }
    }
}
