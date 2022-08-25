using azure_sql_app.models;
using azure_sql_app.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace sqlapp.Pages;

public class IndexModel : PageModel
{
    public List<Product> Products { get; set; } = new();

    private readonly IProductService _service;

    public IndexModel(IProductService service)
    {
        _service = service;
    }

    public void OnGet()
    {
        Products = _service.GetProducts();
    }
}
