using System;
using System.Collections.Generic;
using System.IO;
using MySql.Data.MySqlClient;

namespace ConsoleApp22
{
    class Program
    {
        static void Main(string[] args)
        {
            Connection conn = new Connection();
            
            List<product> products = conn.Select();
            foreach (product product in products)
            {
                Console.WriteLine(product);
            }

            Console.ReadLine();

            product productInsert = new product();
            productInsert.Name = "Product 9000";
            productInsert.Price = 1000;

            conn.Insert(productInsert);

            Console.WriteLine("Updated!");


            product productToUpdate = new product();
            productToUpdate.ProductID = 26;
            productToUpdate.Name = "Update 9111";
            productToUpdate.Price = 20000;

            conn.Update(productToUpdate);

            Console.WriteLine("UD");



            product prodDelete = new product();
            prodDelete.Name = "Product 1";
            prodDelete.ProductID = 26;

            conn.Delete(prodDelete.ProductID);

            Console.ReadLine();


            Console.ReadLine();
        }
    }
}
