using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Exemple1
{
   public class CustomerServices
    {

       private static CustomerServices current;

       public static CustomerServices Current
       {
           get
           {
               return current ?? (current = new CustomerServices());
           }
       }


       private String connectionString;
       public CustomerServices()
       {
           connectionString = ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString;
       }


       public List<Customer> GetCustomers()
       {
           List<Customer> customers = new List<Customer>();

           using (SqlConnection myConnection = new SqlConnection(connectionString))
           {
               try
               {

                   String myQuery = "Select * from Customers";

                   using (SqlCommand myCommand = new SqlCommand(myQuery, myConnection))
                   {
                       myConnection.Open();
                       SqlDataReader myReader = myCommand.ExecuteReader();

                       while (myReader.Read())
                       {
                           Customer customer = FillCustomerFromReader(myReader);

                           customers.Add(customer);
                       }
                       myConnection.Close();
                   }

               }
               catch (Exception exception)
               {
                   Console.WriteLine(exception.Message);
               }

           }

           return customers;
       }

       public Customer GetCustomerById(string id)
       {
           Customer customer = null;

           using (SqlConnection myConnection = new SqlConnection(connectionString))
           {
               try
               {

                   String myQuery = "Select * from Customers where CustomerId = @customerID";

                   
                   using (SqlCommand myCommand = new SqlCommand(myQuery, myConnection))
                   {
                       SqlParameter p = new SqlParameter("@customerID", System.Data.SqlDbType.NChar, 5);
                       p.Value = id;

                       myCommand.Parameters.Add(p);

                       myConnection.Open();
                       SqlDataReader myReader = myCommand.ExecuteReader();

                       if (myReader.Read())
                           customer = FillCustomerFromReader(myReader);

                       myConnection.Close();
                   }

               }
               catch (Exception exception)
               {
                   Console.WriteLine(exception.Message);
               }

           }
           return customer;

       }

       public static Customer FillCustomerFromReader(SqlDataReader reader)
       {
           Customer customer = new Customer();

           customer.CustomerID = (String)reader["CustomerID"];
           customer.CompanyName = (String)reader["CompanyName"];
           customer.ContactName = reader["ContactName"] == DBNull.Value ? null : (String)reader["ContactName"];
           customer.ContactTitle = reader["ContactTitle"] as String;
           customer.Address = reader["Address"] as String;
           customer.City = reader["City"] as String;
           customer.Region = reader["Region"] as String;
           customer.PostalCode = reader["PostalCode"] as String;
           customer.Country = reader["Country"] as String;
           customer.Phone = reader["Phone"] as String;
           customer.Fax = reader["Fax"] as String;

           return customer;
       }

    }
}
