using System.Data.SqlClient;
using azure_sql_app.models;

namespace azure_sql_app.services
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }

    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;

        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection GetConnection()
        {
            // var builder = new SqlConnectionStringBuilder();
            // builder.DataSource = connString;
            // builder.UserID = user;
            // builder.Password = pss;
            // builder.InitialCatalog = database;

            // return new SqlConnection(builder.ConnectionString);
            var connString = _configuration["SQLConnection"];

            return new SqlConnection(connString);
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