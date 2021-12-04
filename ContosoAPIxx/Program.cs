using ProductsAPI;
using Microsoft.EntityFrameworkCore;
using Contoso.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataDBContext>(option =>
{
    var connectionString = builder.Configuration.GetConnectionString("defaultConnection");

    option.UseSqlServer(connectionString);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseCors("Policy");

app.UseAuthorization();

app.MapControllers();

app.Run();


