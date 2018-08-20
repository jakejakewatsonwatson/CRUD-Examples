using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp22
{
    public class Connection
    {
        public string ConnectionString { get; set; }
        
        public void ProductRepo(string connectionString)
        {
            var ConfigurationBuilder = new ConfigurationBuilder()
#if DEBUG
            .AddJsonFile("jsconfig1.json")
#else
                .AddJsonFile("json_release.json")
#endif
                .Build();

            ConnectionString = "DefaultConnection";
        }

        public void Insert(product product)
        {
            MySqlConnection conn = new MySqlConnection(ConnectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO products (Name, Price) VALUES (@Name, @Price);";
                cmd.Parameters.AddWithValue("name", product.Name);
                cmd.Parameters.AddWithValue("price", product.Price);

                cmd.ExecuteNonQuery();

            }
        }

        public List<product> Select()
        {


            MySqlConnection conn = new MySqlConnection(ConnectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();

                cmd.CommandText = "Select ProductID, Name, Price" +
                                  " From Products;";

                MySqlDataReader dr = cmd.ExecuteReader();

                List<product> products = new List<product>();

                while (dr.Read())
                {
                    product product = new product();
                    product.ProductID = Convert.ToInt32(dr["ProductID"]);
                    product.Name = dr["Name"].ToString();
                    product.Price = Convert.ToDecimal(dr["Price"]);

                    products.Add(product);
                }
                return products;
            }


        }

        public void Update(product product)
        {

            MySqlConnection conn = new MySqlConnection(ConnectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE PRODUCTS " +
                    "SET Name = @name, Price = @price " +
                    " WHERE ProductID = @id; ";
                cmd.Parameters.AddWithValue("name", product.Name);
                cmd.Parameters.AddWithValue("price", product.Price);
                cmd.Parameters.AddWithValue("id", product.ProductID);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int productID)
        {


            MySqlConnection conn = new MySqlConnection(ConnectionString);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = $"DELETE Name FROM Categories WHERE categoryID = {productID};";

                cmd.ExecuteNonQuery();
            }
        }


    }
}
