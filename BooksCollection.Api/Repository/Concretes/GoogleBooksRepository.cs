using BookCollection.Api.Configuration;
using BooksCollection.Api.Constants;
using BooksCollection.Api.Models;
using BooksCollection.Api.Repository.Interfaces;
using Google.Apis.Books.v1;
using Google.Apis.Services;

namespace BooksCollection.Api.Repository.Concretes
{
    public class GoogleBooksRepository : IGoogleBooksRepository
    {
        private readonly BooksService _booksService;
        public GoogleBooksRepository()
        {

            var googleBooksApiKey = GlobalConfig.GetSetting("GoogleBooksApiKey");

            _booksService = new BooksService(new BaseClientService.Initializer
            {
                ApiKey = googleBooksApiKey,
                ApplicationName = "MyApp 1",
            });
        }

        /// <summary>
        /// Search Google Books for a list of matching titles.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>GoogleBooksSearchResponse</returns>
        public async Task<GoogleBooksSearchResponse> SearchGoogleBooksAsync(string title)
        {
            var response = new GoogleBooksSearchResponse();

            if(string.IsNullOrEmpty(title))
            {
                response.ErrorMessage = Messaging.ErrorMessages.InvalidTitleSupplied;
            }

            var booksRequest = _booksService.Volumes.List($"intitle:{title}");
            var booksResponse = await booksRequest.ExecuteAsync();

            if (booksResponse?.Items?.Count > 0)
            {
                // Convert from Google Volume to Book
                response.Books = booksResponse.Items.Select(i => ConvertVolumeToBook(i)).ToList();
            }
            else
            {
                response.ErrorMessage = Messaging.ErrorMessages.CouldNotFindTitles;
            }

            return response;
        }

        /// <summary>
        /// Convert Google Book Volume to Book object.
        /// </summary>
        /// <param name="volume"></param>
        /// <returns></returns>
        public Book ConvertVolumeToBook(Google.Apis.Books.v1.Data.Volume volume)
        {
            var book = new Book
            {
                Title = volume.VolumeInfo?.Title,
                Description = volume.VolumeInfo?.Description,
                AuthorName = volume.VolumeInfo?.Authors?.FirstOrDefault(),
                PublishedDate = DateTime.TryParse(volume.VolumeInfo?.PublishedDate, out var publishedDate) ? publishedDate : default,
                Msrp = (decimal)(volume.SaleInfo?.ListPrice?.Amount ?? 0),
                MsrpUnit = MsrpUnit.Usd,
                ImageUrl = volume.VolumeInfo?.ImageLinks?.Thumbnail,
                //Rating = volume.VolumeInfo.AverageRating,
            };

            return book;
        }
    }
}
