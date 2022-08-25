using azure_sql_app.models;
using azure_sql_app.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace sqlapp.Pages;

public class IndexModel : PageModel
{
    public List<Product> Products { get; set; } = new();


    public void OnGet()
    {
        ProductService service = new ProductService();
        Products = service.GetProducts();
    }
}
