using BooksCollection.Api.Models;

namespace BooksCollection.Api.Repository.Interfaces
{
    public interface IGoogleBooksRepository
    {
        public Task<GoogleBooksSearchResponse> SearchGoogleBooksAsync(GoogleBooksSearchRequest request);
    }
}
