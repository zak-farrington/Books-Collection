using BookCollection.Api.Configuration;
using BooksCollection.Api.Constants;
using Microsoft.Extensions.Configuration;

namespace BooksCollection.Tests
{
    public class GoogleBooksRepositoryIntegrationTests
    {
        private IConfiguration _configuration;

        public GoogleBooksRepositoryIntegrationTests()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = configurationBuilder.Build();

            // Initialize a Global Config - example of Ambient Context dependency injection
            GlobalConfig.Initialize(_configuration);
        }

        [Fact]
        public async Task SearchGoogleBooksAsync_ReturnsBooks_Success()
        {
            var repository = new GoogleBooksRepository();
            var request = new GoogleBooksSearchRequest { Title = "Limitless" };

            var response = await repository.SearchGoogleBooksAsync(request);

            Assert.Null(response.ErrorMessage);
            Assert.True(response.Books.Count > 0);
        }

        [Fact]
        public async Task SearchGoogleBooksAsync_NoBooksFound_Fails()
        {
            var repository = new GoogleBooksRepository();
            var request = new GoogleBooksSearchRequest { Title = Guid.NewGuid().ToString() }; // Hopefully there's no books found that match a random Guid.

            var response = await repository.SearchGoogleBooksAsync(request);

            Assert.Equal(Messaging.ErrorMessages.CouldNotFindTitles, response.ErrorMessage);
        }
    }
}
