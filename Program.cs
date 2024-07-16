using Home_Loan_App.Data;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//This is done after connecting MsServer and creating a table on  (appsettings.json and ApplictationsDbContext.cs
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
            builder.Configuration.GetConnectionString("Somee")
            ));/* after writing this code , install Microsoft.EntityFrameworkCore.SqlServer
                * Then Add a migration (first install Microsoft.EntityFrameworkCore.Tools)
                * to add migration go to toolS>Nuget Package manager>package manager console
                * The after PM> write add-migration AddCustomerToDatabase(migration name) 
                * then write and run update-database (to push migrations to database.
                * After all these steps , the project will be connected to MSSQL
                */
//builder.Services.AddRazorPages();//this was added after installing 'Microsoft.AspNetCore.Mvc.Razor.Runtimecompilation'
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();//Add this code after adding nav from bootstrap5
// Set EPPlus license context
ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Or LicenseContext.Commercial if you have a commercial license
var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
