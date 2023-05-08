using MyShop.ApplicationCore.Entities;
using MyShop1.Configuration;
using MyShop1.Interfaces;
using MyShop1.Models;
using MyShop1.Services;
using Microsoft.EntityFrameworkCore;
using MyShop.Infrastructure.Data;
using MyShop.ApplicationCore.Interfaces;
using MyShop.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using MyShop.ApplicationCore.Services;
using MyShop.ApplicationCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AppIdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AppIdentityDbContextConnection' not found.");

builder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(connectionString));

MyShop.Infrastructure.Dependencies.ConfigureServices(builder.Configuration, builder.Services);

//Ñonfigure Identity
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>()
    .AddDefaultUI()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

//IoC
builder.Services.AddCoreServices();
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<ICatalogItemViewModelService, CatalogItemViewModelService>();
builder.Services.AddSingleton<IUriComposer>(new UriComposer(builder.Configuration.Get<CatalogSettings>()));

var app = builder.Build();
app.Logger.LogInformation("App created...");

app.Logger.LogInformation("Database migraion running...");
using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var catalogContext = scopedProvider.GetRequiredService<CatalogContext>();
        if (catalogContext.Database.IsSqlServer())
        {
            catalogContext.Database.Migrate();
        }
        await CatalogContextSeed.SeedAsync(catalogContext, app.Logger);

        var identityContext = scopedProvider.GetRequiredService<AppIdentityDbContext>();
        if (identityContext.Database.IsSqlServer()) 
        {
            identityContext.Database.Migrate();
        }
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred adding migrations to Databse.");
    }

}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Catalog}/{action=Index}/{id?}");


app.MapRazorPages();
app.Logger.LogDebug("Starting the app...");
app.Run();
