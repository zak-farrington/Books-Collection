﻿using BookCollection.Api.Configuration;
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
        public async Task<GoogleBooksSearchResponse> SearchGoogleBooksAsync(GoogleBooksSearchRequest request)
        {
            var response = new GoogleBooksSearchResponse();

            var booksRequest = _booksService.Volumes.List($"intitle:{request.Title}");
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
                AuthorName = volume.VolumeInfo?.Authors?.FirstOrDefault(), // Assuming the first author if there are multiple
                PublishedDate = DateTime.TryParse(volume.VolumeInfo?.PublishedDate, out var publishedDate) ? publishedDate : default,
                // Assuming that the MSRP is found in the 'ListPrice' property of the 'SaleInfo' object
                Msrp = (decimal)(volume.SaleInfo?.ListPrice?.Amount ?? 0),
                ImageUrl = volume.VolumeInfo?.ImageLinks?.Thumbnail,
            };

            return book;
        }
    }
}
