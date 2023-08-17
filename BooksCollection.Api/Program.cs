using BookCollection.Api.Configuration;
using BooksCollection.Api.Data;
using BooksCollection.Api.Hubs;
using BooksCollection.Api.Repository.Concretes;
using BooksCollection.Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Initialize Global Config once and for all - example of ambient context.
GlobalConfig.Initialize(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalHostCors",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000") // Your client's origin here
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSignalR();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBooksRepository, BooksRepository>();
builder.Services.AddScoped<IGoogleBooksRepository, GoogleBooksRepository>();

builder.Services.AddDbContext<BooksCollectionDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BookCollectionApiContext")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowLocalHostCors");
}

app.UseAuthorization();

app.MapControllers();

app.MapHub<BooksCollectionHub>("/booksCollectionHub");


app.Run();