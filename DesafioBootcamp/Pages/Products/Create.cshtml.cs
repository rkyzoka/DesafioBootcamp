using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DesafioBootcamp.Pages.Products
{
    public class CreateModel : PageModel
    {
        public ProductInfo productInfo = new ProductInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            productInfo.name = Request.Form["name"];

            if(productInfo.name.Length == 0) {
                errorMessage = "All the fields are required!";
                return;
            }

            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=products;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO products (name) VALUES (@name);";

                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        command.Parameters.AddWithValue("@name", productInfo.name);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception error) 
            {
                errorMessage = error.Message;
                return;
            }



            productInfo.name = "";
            successMessage = "New Product created successfully!";

            Response.Redirect("/Products/Index");
        }
    }
}
