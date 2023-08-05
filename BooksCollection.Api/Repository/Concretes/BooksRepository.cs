using BooksCollection.Api.Data;
using BooksCollection.Api.Models;
using BooksCollection.Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BooksCollection.Api.Repository.Concretes
{
    public class BooksRepository : IBooksRepository
    {
        private readonly BooksCollectionApiContext _context;

        public BooksRepository(BooksCollectionApiContext context)
        {
            _context = context;
        }

        public async Task<BooksListResponse> GetBooksListResponseAsync()
        {
            var books = await _context.Book.ToListAsync();
            var response = new BooksListResponse { Books = books };
            return response;
        }

        public async Task<AddBookResponse> AddBookAsync(AddBookRequest request)
        {
            var addBookResponse = new AddBookResponse();

            request.Book.Uid = Guid.NewGuid().ToString();
            request.Book.CreationDate = DateTime.Now;

            _context.Book.Add(request.Book);
            var rowsSaved = await _context.SaveChangesAsync();

            if (rowsSaved <= 0)
            {
                addBookResponse.ErrorMessage = "Failed to add book to collection.";
            }

            return addBookResponse;
        }

        public async Task<DeleteBookResponse> DeleteBookAsync(DeleteBookRequest request)
        {
            var deleteBookResponse = new DeleteBookResponse();

            var book = await _context.Book.FirstOrDefaultAsync(b => b.Uid == request.Uid);
            if (book != null)
            {
                _context.Book.Remove(book);
                var rowsDeleted = await _context.SaveChangesAsync();

                if (rowsDeleted <= 0)
                {
                    deleteBookResponse.ErrorMessage = "Failed to delete book from collection.";
                }
            }
            else
            {
                deleteBookResponse.ErrorMessage = "Book not found.";
            }

            return deleteBookResponse;
        }
    }

}
