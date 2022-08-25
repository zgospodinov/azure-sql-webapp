using System.Data.SqlClient;
using azure_sql_app.models;

namespace azure_sql_app.services
{
    public class ProductService
    {
        private static string connString = "appserverdbzg.database.windows.net";
        private static string user = "demouser";
        private static string pss = "Pa$$w0rd";
        private static string database = "appdb";

        private SqlConnection GetConnection()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = connString;
            builder.UserID = user;
            builder.Password = pss;
            builder.InitialCatalog = database;

            return new SqlConnection(builder.ConnectionString);
        }

        public List<Product> GetProducts()
        {
            // var conn = GetConnection();


            using SqlConnection conn = GetConnection();

            List<Product> products = new List<Product>();
            string query = "SELECT ProductID, ProductName, Quantity FROM Products";

            conn.Open();

            SqlCommand cmd = new SqlCommand(query, conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product
                    {
                        ProductID = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        Quantity = reader.GetInt32(2)
                    };

                    products.Add(product);
                }
            }


            return products;
        }

    }
}