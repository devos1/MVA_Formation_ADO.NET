using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Northwind.Exemple1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Customer> customers;

            customers = CustomerServices.Current.GetCustomers();

            Customer cu = CustomerServices.Current.GetCustomerById("ALFKI");

            foreach (Customer c in customers)
                Console.WriteLine(c.ContactName);


            Console.WriteLine("Terminée");

            Console.ReadLine();
        }
    }
}
