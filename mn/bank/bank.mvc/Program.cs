using Bank.Data.Context;
using Bank.Mvc.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var bankIdentityConnectionString = builder.Configuration.GetConnectionString("BankIdentityDbConnection") ?? throw new InvalidOperationException("Connection string 'BankIdentityDbConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(bankIdentityConnectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var bankConnectionString = builder.Configuration.GetConnectionString("BankDbConnection") ?? throw new InvalidOperationException("Connection string 'BankDbConnection' not found.");
builder.Services.AddDbContext<BankDbContext>(options =>
{
    options.UseSqlServer(bankConnectionString);
});

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();

app.Run();
