using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
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
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

app.UseSession();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();