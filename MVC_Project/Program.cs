using Microsoft.EntityFrameworkCore;

using MVC_Project.Data;
using MVC_Project.Data.Implementation;
using MVC_Project.Data.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<ICompaniesRepository,CompaniesRepository>();
builder.Services.AddTransient<IDepartmentsRepository, DepartmentsRepository>();
builder.Services.AddTransient<IDesignationsRepository, DesignationsRepository>();
builder.Services.AddTransient<IShiftsRepository, ShiftsRepository>();
builder.Services.AddTransient<IEmployeesRepository, EmployeesRepository>();

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
