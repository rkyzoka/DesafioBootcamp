using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace DesafioBootcamp.Pages.Products
{
    public class DeleteModel : PageModel
    {
        public ProductInfo productInfo = new ProductInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            productInfo.id = Request.Form["id"];
            

            if (productInfo.id.Length == 0)
            {
                errorMessage = "ID required!";
                return;
            }

            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=products;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "DELETE FROM products WHERE id=@id;";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", productInfo.id);
                       
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception error)
            {
                errorMessage = error.Message;
                return;
            }

            Response.Redirect("/Products/Index");
        }
    }
}
