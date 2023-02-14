using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ticketanica.DataLayer;
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

builder.Services.AddDistributedMemoryCache();
builder.Services.AddHttpContextAccessor();

builder.Services.BindsServices();


builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".TicketanicaSession";
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSession();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();