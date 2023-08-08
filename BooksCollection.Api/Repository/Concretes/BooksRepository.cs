using BooksCollection.Api.Constants;
using BooksCollection.Api.Data;
using BooksCollection.Api.Models;
using BooksCollection.Api.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BooksCollection.Api.Repository.Concretes
{
    public class BooksRepository : IBooksRepository
    {
        private readonly BooksCollectionApiContext _context;
        private string[] _unmodifiableProperties = { "Id", "CreationDate" }; // These properties cannot be updated by books/modify


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
       
        public async Task<Book> GetBookByUid(string uid)
        {
            return await _context.Book.FirstOrDefaultAsync(b => b.Uid == uid);
        }

        public async Task<AddBookResponse> AddBookAsync(AddBookRequest request)
        {
            var addBookResponse = new AddBookResponse();

            // Note: Using .ToLower because SQLite doesn't support EF.Functions.Like
            var existingBook = await _context.Book.FirstOrDefaultAsync(b => (request.Book.Isbn != null && b.Isbn == request.Book.Isbn) || b.Title.ToLower() == request.Book.Title.ToLower());
            
            if (existingBook != null)
            {
                // Duplicate ISBN or Title found.
                addBookResponse.ErrorMessage = Messaging.ErrorMessages.DuplicateBookExists;
                return addBookResponse;
            }

            request.Book.Uid = Guid.NewGuid().ToString();
            request.Book.CreationDate = DateTime.Now;
            request.Book.LastUpdatedDate = DateTime.Now;

            _context.Book.Add(request.Book);
            var rowsSaved = await _context.SaveChangesAsync();

            if (rowsSaved <= 0)
            {
                addBookResponse.ErrorMessage = Messaging.ErrorMessages.AddBookFailed;
            }

            return addBookResponse;
        }

        public async Task<ModifyBookResponse> ModifyBookAsync(ModifyBookRequest request)
        {
            var modifyBookResponse = new ModifyBookResponse();

            var existingBook = await GetBookByUid(request.Book.Uid);
            if (existingBook == null)
            {
                modifyBookResponse.ErrorMessage = Messaging.ErrorMessages.BookNotFound;
                return modifyBookResponse;
            }

            var properties = typeof(Book).GetProperties();
            foreach (var property in properties)
            {
                if (_unmodifiableProperties.Any(s => string.Equals(property.Name, s)))
                {
                    // Skip properties that can't be modified.
                    continue;
                }

                // Check if property should be modified.
                var updatedValue = property.GetValue(request.Book);
                if (updatedValue != null)
                {
                    property.SetValue(existingBook, updatedValue);
                }
            }

            existingBook.LastUpdatedDate = DateTime.Now;

            var rowsSaved = await _context.SaveChangesAsync();

            if (rowsSaved <= 0)
            {
                modifyBookResponse.ErrorMessage = Messaging.ErrorMessages.ModifyBookFailed;
            }

            return modifyBookResponse;
        }


        public async Task<DeleteBookResponse> DeleteBookAsync(DeleteBookRequest request)
        {
            var deleteBookResponse = new DeleteBookResponse();

            var book = await GetBookByUid(request.Uid);
            if (book != null)
            {
                _context.Book.Remove(book);
                var rowsDeleted = await _context.SaveChangesAsync();

                if (rowsDeleted <= 0)
                {
                    deleteBookResponse.ErrorMessage = Messaging.ErrorMessages.DeleteBookFailed;
                }
            }
            else
            {
                deleteBookResponse.ErrorMessage = Messaging.ErrorMessages.BookNotFound;
            }

            return deleteBookResponse;
        }
    }
}
