using System.Text.Json.Serialization;
using HotelListing.API.Contracts;
using HotelListing.API.Data;
using HotelListing.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<HotelListingDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("HotelListingConnectionString")));  //DB IoC

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options=>
    options.AddDocumentTransformer((document, context, CancellationToken) =>
    {
        document.Servers = new List<OpenApiServer>
        {
            new OpenApiServer{Url = builder.Configuration["serverUrl"], Description = "Production Server"}
        };
        return Task.CompletedTask;
    })
);

builder.Services.AddScoped<ICountriesService, CountriesService>();
builder.Services.AddScoped<IHotelsService, HotelsService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
