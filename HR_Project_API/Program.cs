using HR_Project_API.Data.Implementation;
using HR_Project_API.Data.Interface;
using HR_Project_API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddTransient<ICompaniesRepository, CompaniesRepository>();
builder.Services.AddTransient<IDepartmentsRepository, DepartmentsRepository>();
builder.Services.AddTransient<IDesignationsRepository, DesignationsRepository>();
builder.Services.AddTransient<IShiftsRepository, ShiftsRepository>();
builder.Services.AddTransient<IEmployeesRepository, EmployeesRepository>();
builder.Services.AddTransient<IAttendancesRepository, AttendancesRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
