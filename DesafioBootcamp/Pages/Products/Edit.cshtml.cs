using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DesafioBootcamp.Pages.Products
{
    public class EditModel : PageModel
    {
        public ProductInfo productInfo = new ProductInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            
        }

        public void OnPost() {
            productInfo.id = Request.Form["id"];
            productInfo.name = Request.Form["name"];

            if (productInfo.id.Length == 0 || productInfo.name.Length == 0)
            {
                errorMessage = "All the fields are required!";
                return;
            }

            try 
            {
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=products;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE products SET name=@name WHERE id=@id;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", productInfo.id);
                        command.Parameters.AddWithValue("@name", productInfo.name);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception error) 
            {
                errorMessage = error.Message;
                return;
            }

            Response.Redirect("/Products/Index");
        }
    }
}
