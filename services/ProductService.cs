using System.Data.SqlClient;
using azure_sql_app.models;
using Microsoft.FeatureManagement;
using Newtonsoft.Json;

namespace azure_sql_app.services
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Task<List<Product>> GetProductsAzFunc();
        Task<bool> IsBeta();
    }

    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;
        private readonly IFeatureManager _featureManager;

        public ProductService(IConfiguration configuration, IFeatureManager featureManager)
        {
            _featureManager = featureManager;
            _configuration = configuration;
        }

        private SqlConnection GetConnection()
        {
            var connString = _configuration["SQLConnection"];

            return new SqlConnection(connString);
        }

        public async Task<List<Product>> GetProductsAzFunc(){
            var funcUrl = "https://appfzg.azurewebsites.net/api/GetProducts?code=FgU-6A6atRGhmgr-qG9KTs9J8QlYA1K9y5DGaKRsLFh2AzFu_qZ-0g==";

            using (HttpClient client = new HttpClient()){
                var response = await client.GetAsync(funcUrl);

                var content =await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var emptyResult = Enumerable.Empty<Product>().ToList();

                    return await Task.FromResult<List<Product>>(emptyResult);
                }
                return JsonConvert.DeserializeObject<List<Product>>(content);
            }
        }
        
        
        public List<Product> GetProducts()
        {

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

        public async Task<bool> IsBeta()
        {
            if (await _featureManager.IsEnabledAsync("beta"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}