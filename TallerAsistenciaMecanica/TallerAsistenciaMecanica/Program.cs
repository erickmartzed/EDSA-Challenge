using MASBusiness.DTO;
using MASDataAccess;
using MASDataAccess.Models;
using MASBusiness.Interfaces;
using MASBusiness.Services;
using Microsoft.EntityFrameworkCore;
using MASDataAccess.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MyDatabase");

builder.Services.AddDbContext<DbMechanicalAssistanceShopContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddScoped<IUtilsService, UtilsService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<IModelService, ModelService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IRenderedServiceService, RenderedServiceService>();
builder.Services.AddScoped<IServiceByRendServService, ServiceByRendServService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
