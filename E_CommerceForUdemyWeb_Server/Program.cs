using E_CommerceForUdemy_Business.Repository;
using E_CommerceForUdemy_Business.Repository.IRepository;
using E_CommerceForUdemy_DataAccess.Data;
using E_CommerceForUdemyWeb_Server.Data;
using E_CommerceForUdemyWeb_Server.Service;
using E_CommerceForUdemyWeb_Server.Service.IService;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt/QHRqVVhkVVpFdEBBXHxAd1p/VWJYdVt5flBPcDwsT3RfQF5jTX5ad0BhWnxYeHZVTg==;Mgo+DSMBPh8sVXJ0S0J+XE9Af1RDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS31TckVqWHpcc3dTTmNYWQ==;ORg4AjUWIQA/Gnt2VVhkQlFaclZJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxQdkFjUH5bcndWQGhZUUw=;MTY2NDg3OUAzMjMwMmUzNDJlMzBvYitLNStNK014eGZNSGJhZGw0SlFtdlJua2NRVVF1cDBCNTQzb0FENWJBPQ==;MTY2NDg4MEAzMjMwMmUzNDJlMzBXMmM4QklVZHNyK3JFVFVlZnNqTmNkVEVBc1FNUWxrN1hlamliUG5SNEljPQ==;NRAiBiAaIQQuGjN/V0Z+WE9EaFtLVmJLYVB3WmpQdldgdVRMZVVbQX9PIiBoS35RdUVnWXZfdXZWRWZVUEx+;MTY2NDg4MkAzMjMwMmUzNDJlMzBYMnYzeER6dU9CUkN6V2d0emxjaEROaDVUZ1dFQTBRMDVBYVN4TWQ4TXEwPQ==;MTY2NDg4M0AzMjMwMmUzNDJlMzBQQ292STNVZ2NXa2krSGU2MVM3TDNKSVoyZ0p0cmF1VFpPYjlqUlhJT09nPQ==;Mgo+DSMBMAY9C3t2VVhkQlFaclZJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxQdkFjUH5bcndWQGlUV0w=;MTY2NDg4NUAzMjMwMmUzNDJlMzBKQnVwT2lmVTY1ZUl5TVEyQlU4ZzRwVllNM29XY0c4QVVXMUdaaEI4T2FzPQ==;MTY2NDg4NkAzMjMwMmUzNDJlMzBuZ0U4UjJ0LzNmNlRlcUdzVS9NNmRPRHRPVEZ3c2JFNFA2cCtVdUh1TUNrPQ==;MTY2NDg4N0AzMjMwMmUzNDJlMzBYMnYzeER6dU9CUkN6V2d0emxjaEROaDVUZ1dFQTBRMDVBYVN4TWQ4TXEwPQ==");
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IFileUpload, FileUpload>();
builder.Services.AddScoped<IProductPriceRepository, ProductPriceRepository>();
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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
