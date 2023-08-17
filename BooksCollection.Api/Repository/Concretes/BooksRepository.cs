using BooksCollection.Api.Constants;
using BooksCollection.Api.Data;
using BooksCollection.Api.Hubs;
using BooksCollection.Api.Models;
using BooksCollection.Api.Repository.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace BooksCollection.Api.Repository.Concretes
{
    public class BooksRepository : IBooksRepository
    {
        private readonly BooksCollectionDbContext _dbContext;
        private readonly IHubContext<BooksCollectionHub> _hubContext;

        private string[] _unmodifiableProperties = { "Id", "CreationDate" }; // These properties cannot be updated by books/modify

        public BooksRepository(BooksCollectionDbContext context, IHubContext<BooksCollectionHub> hub)
        {
            _dbContext = context;
            _hubContext = hub;
        }

        public async Task<BooksListResponse> GetBooksListResponseAsync()
        {
            var books = await _dbContext.Book.ToListAsync();


            var response = new BooksListResponse { Books = books };
            return response;
        }

        public async Task<Book> GetBookByUid(string uid)
        {
            return await _dbContext.Book.FirstOrDefaultAsync(b => b.Uid == uid);
        }

        public async Task<AddBookResponse> AddBookAsync(AddBookRequest request)
        {
            var addBookResponse = new AddBookResponse();

            // Note: Using .ToLower because SQLite doesn't support EF.Functions.Like
            var existingBook = await _dbContext.Book.FirstOrDefaultAsync(b => (request.Book.Isbn != null && b.Isbn == request.Book.Isbn) || b.Title.ToLower() == request.Book.Title.ToLower());

            if (existingBook != null)
            {
                // Duplicate ISBN or Title found.
                addBookResponse.ErrorMessage = Messaging.ErrorMessages.DuplicateBookExists;
                return addBookResponse;
            }

            request.Book.Uid = Guid.NewGuid().ToString();
            request.Book.CreationDate = DateTime.Now;
            request.Book.LastUpdatedDate = DateTime.Now;

            _dbContext.Book.Add(request.Book);
            var rowsSaved = await _dbContext.SaveChangesAsync();

            if (rowsSaved <= 0)
            {
                addBookResponse.ErrorMessage = Messaging.ErrorMessages.AddBookFailed;
            }

            if (addBookResponse.IsSuccessful)
            {
                await _hubContext.Clients.All.SendAsync(SignalR.Messages.BookAddedOrModified, request.Book);
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

            var rowsSaved = await _dbContext.SaveChangesAsync();

            if (rowsSaved <= 0)
            {
                modifyBookResponse.ErrorMessage = Messaging.ErrorMessages.ModifyBookFailed;
            }

            if (modifyBookResponse.IsSuccessful)
            {
                await _hubContext.Clients.All.SendAsync(SignalR.Messages.BookAddedOrModified, existingBook);
            }

            return modifyBookResponse;
        }

        public async Task<DeleteBookResponse> DeleteBookAsync(string uid)
        {
            var deleteBookResponse = new DeleteBookResponse();

            if (!Guid.TryParse(uid, out _))
            {
                deleteBookResponse.ErrorMessage = Messaging.ErrorMessages.InvalidBookUid;
                return deleteBookResponse;
            }

            var book = await GetBookByUid(uid);
            if (book != null)
            {
                _dbContext.Book.Remove(book);
                var rowsDeleted = await _dbContext.SaveChangesAsync();

                if (rowsDeleted <= 0)
                {
                    deleteBookResponse.ErrorMessage = Messaging.ErrorMessages.DeleteBookFailed;
                }
            }
            else
            {
                deleteBookResponse.ErrorMessage = Messaging.ErrorMessages.BookNotFound;
            }

            if (deleteBookResponse.IsSuccessful)
            {
                await _hubContext.Clients.All.SendAsync(SignalR.Messages.BookRemovedFrom);
            }

            return deleteBookResponse;
        }
    }
}
