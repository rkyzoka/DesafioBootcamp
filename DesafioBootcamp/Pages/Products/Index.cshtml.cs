using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DesafioBootcamp.Pages.Products
{
    public class IndexModel : PageModel
    {
        public List<ProductInfo> listProducts = new List<ProductInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=products;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM products";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductInfo productInfo = new ProductInfo();
                                productInfo.id = "" + reader.GetInt32(0);
                                productInfo.name = reader.GetString(1);
                                listProducts.Add(productInfo);

                            }
                        }
                    }
                }

            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }
    }

    public class ProductInfo
    {
        public string id;
        public string name;
    }
}
