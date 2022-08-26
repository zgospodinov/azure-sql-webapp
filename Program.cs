using azure_sql_app.services;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);


// Requred to connect ot Azure App Configuration
var azAppConfigConnection = "Endpoint=https://webappconfigzg.azconfig.io;Id=JwA0-l9-s0:GRtuvlnq7+9fSJCnISYs;Secret=GWwu5mhT845NKra9p0uvQC+xJISWTzVesNl+TQzqfCo=";

builder.Host.ConfigureAppConfiguration(builder =>
{
    builder.AddAzureAppConfiguration(options =>
    {
        options.Connect(azAppConfigConnection)
            .UseFeatureFlags();
    });

});


// Add services to the container.
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddRazorPages();
builder.Services.AddFeatureManagement();

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
