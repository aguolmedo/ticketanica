using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ticketanicav2.DataLayer;
using ticketanicav2.Dependencies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TicketanicaDbContext>(_ =>
{
    var connectionString = builder.Configuration.GetConnectionString("MariaDbConnectionString");
    _.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});



builder.Services.AddServiceDependency();
builder.Services.BindsServices();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();