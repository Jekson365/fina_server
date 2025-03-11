using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Interfaces;
using server.repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddNewtonsoftJson(Options =>
{
    Options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddCors(options =>
   {
       options.AddPolicy("AllowAll", builder =>
           builder.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader());
   });

builder.Services.AddScoped<IGroupRepository, GroupRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connection = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connection);

});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseStaticFiles();
app.UseHttpsRedirection();
app.MapControllers();



app.Run();