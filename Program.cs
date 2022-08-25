using azure_sql_app.services;

var builder = WebApplication.CreateBuilder(args);


// Requred to connect ot Azure App Configuration
var azAppConfigConnection = "Endpoint=https://webappconfigzg.azconfig.io;Id=JwA0-l9-s0:GRtuvlnq7+9fSJCnISYs;Secret=GWwu5mhT845NKra9p0uvQC+xJISWTzVesNl+TQzqfCo=";

builder.Host.ConfigureAppConfiguration(builder => {
    builder.AddAzureAppConfiguration(azAppConfigConnection);
});


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
