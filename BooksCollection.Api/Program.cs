using BookCollection.Api.Configuration;
using BooksCollection.Api.Data;
using BooksCollection.Api.Repository.Concretes;
using BooksCollection.Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Initialize Global Config once and for all - example of Ambient Context dependency injection
GlobalConfig.Initialize(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBooksRepository, BooksRepository>();
builder.Services.AddScoped<IGoogleBooksRepository, GoogleBooksRepository>();

builder.Services.AddDbContext<BooksCollectionApiContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BookCollectionApiContext")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();


app.Run();