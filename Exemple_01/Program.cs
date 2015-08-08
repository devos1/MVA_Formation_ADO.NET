using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Northwind.Exemple1
{
    /// <summary>
    /// Date : 08.08.2015
    /// Formation MVA sur ADO.NET : Module 1
    /// Description : Simple lecture de la BDD et affichage ligne par ligne du contenu de la table Product
    ///             : Ce n'est PAS bonne méthode car c'est en mode connecté et la connexion reste ouverte!
    /// 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            using (SqlConnection myConnection = new SqlConnection(@"Data Source=localhost;Initial Catalog=NorthwindNET;Integrated Security=true;"))
            {
                try
                {
                    myConnection.Open();
                        
                    String myQuery = "Select * from Product";

                    using (SqlCommand myCommand = new SqlCommand(myQuery, myConnection))
                    {
                        SqlDataReader myReader = myCommand.ExecuteReader();

                        // Lecture de chaque ligne et affichage direct à la console
                        while (myReader.Read())
                        {
                            int productID = (int)myReader["ID"];
                            string productName = (String)myReader["Name"];
                            Decimal unitPrice = myReader["UnitPrice"] == DBNull.Value ? (Decimal)0 : (Decimal)myReader["UnitPrice"];

                            Console.WriteLine("Id : " + productID + " Name : " + productName + " Price : " + unitPrice);
                        }

                    }

                    myConnection.Close();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }

            }
            Console.WriteLine("Terminée");

            Console.ReadLine();
        }
    }
}
