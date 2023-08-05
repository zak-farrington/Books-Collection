using BookCollection.Api.Configuration;
using BookCollection.Api.Data;
using BookCollectionApi.Repository.Concretes;
using BookCollectionApi.Repository.Interfaces;
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

//public static class BookEndpoints
//{
//	public static void MapBookEndpoints (this IEndpointRouteBuilder routes)
//    {
//        var group = routes.MapGroup("/api/Book").WithTags(nameof(Book));

//        group.MapGet("/", async (BookCollectionApiContext db) =>
//        {
//            return await db.Book.ToListAsync();
//        })
//        .WithName("GetAllBooks")
//        .WithOpenApi();

//        group.MapGet("/{id}", async Task<Results<Ok<Book>, NotFound>> (int id, BookCollectionApiContext db) =>
//        {
//            return await db.Book.AsNoTracking()
//                .FirstOrDefaultAsync(model => model.Id == id)
//                is Book model
//                    ? TypedResults.Ok(model)
//                    : TypedResults.NotFound();
//        })
//        .WithName("GetBookById")
//        .WithOpenApi();

//        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Book book, BookCollectionApiContext db) =>
//        {
//            var affected = await db.Book
//                .Where(model => model.Id == id)
//                .ExecuteUpdateAsync(setters => setters
//                  .SetProperty(m => m.Id, book.Id)
//                  .SetProperty(m => m.Uid, book.Uid)
//                  .SetProperty(m => m.Title, book.Title)
//                  .SetProperty(m => m.Description, book.Description)
//                  .SetProperty(m => m.AuthorName, book.AuthorName)
//                  .SetProperty(m => m.AuthorUid, book.AuthorUid)
//                  .SetProperty(m => m.PublishedDate, book.PublishedDate)
//                  .SetProperty(m => m.Msrp, book.Msrp)
//                  .SetProperty(m => m.Category, book.Category)
//                  .SetProperty(m => m.OtherCateogryName, book.OtherCateogryName)
//                );

//            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
//        })
//        .WithName("UpdateBook")
//        .WithOpenApi();

//        group.MapPost("/", async (Book book, BookCollectionApiContext db) =>
//        {
//            db.Book.Add(book);
//            await db.SaveChangesAsync();
//            return TypedResults.Created($"/api/Book/{book.Id}",book);
//        })
//        .WithName("CreateBook")
//        .WithOpenApi();

//        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, BookCollectionApiContext db) =>
//        {
//            var affected = await db.Book
//                .Where(model => model.Id == id)
//                .ExecuteDeleteAsync();

//            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
//        })
//        .WithName("DeleteBook")
//        .WithOpenApi();
//    }
////}