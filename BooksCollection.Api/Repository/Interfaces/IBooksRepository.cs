using BooksCollection.Api.Models;

namespace BooksCollection.Api.Repository.Interfaces
{
    public interface IBooksRepository
    {
        Task<BooksListResponse> GetBooksListResponseAsync();
        Task<AddBookResponse> AddBookAsync(AddBookRequest request);
        Task<ModifyBookResponse> ModifyBookAsync(ModifyBookRequest request);
        Task<DeleteBookResponse> DeleteBookAsync(DeleteBookRequest request);
    }

}
