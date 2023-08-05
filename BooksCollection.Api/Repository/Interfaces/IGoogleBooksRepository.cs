using BookCollectionApi.Models;

namespace BookCollectionApi.Repository.Interfaces
{
    public interface IGoogleBooksRepository
    {
        public Task<GoogleBooksSearchResponse> SearchGoogleBooksAsync(GoogleBooksSearchRequest request);
    }
}
