using BooksCollection.Api.Constants;
using BooksCollection.Api.Data;
using Moq;
using Moq.EntityFrameworkCore;

namespace BooksCollection.Tests
{
    /// <summary>
    /// Unit tests that demonstrate mock context.
    /// </summary>
    public class BooksRepositoryUnitTests
    {
        private readonly Book _mockBook = new Book
        {
            Id = 1,
            Uid = Guid.Empty.ToString(),
            CreationDate = DateTime.Now,
            Title = "Limitless",
            AuthorName = "Jim Kwik",
            Description = "Upgrade your brain, learn anything faster and unlock your exceptional life.",
            PublishedDate = DateTime.Today,
            Category = BookCategory.SelfHelp,
            Msrp = (decimal)26.99,
            MsrpUnit = MsrpUnit.Usd,
            ImageUrl = "http://books.google.com/books/content?id=WibGDwAAQBAJ&printsec=frontcover&img=1&zoom=1&edge=curl&source=gbs_api",
        };

        /// <summary>
        /// Helper to create mock context. 
        /// </summary>
        /// <param name="_mockBook"></param>
        /// <returns></returns>
        private Mock<BooksCollectionApiContext> CreateMockContext(Book? book)
        {
            var books = new List<Book> { };
            if (book != null)
            {
                books.Add(book);
            }

            var mockContext = new Mock<BooksCollectionApiContext>();
            mockContext.Setup(x => x.Book).ReturnsDbSet(books);
            return mockContext;
        }

        [Fact]
        public async Task GetBooksListResponseAsync_ReturnsBooks_Success()
        {
            var mockContext = CreateMockContext(_mockBook);
            var repository = new BooksRepository(mockContext.Object);

            var result = await repository.GetBooksListResponseAsync();

            Assert.True(result.Books.Count > 0);
        }

        [Fact]
        public async Task AddBookAsync_AddBook_Success()
        {
            var request = new AddBookRequest
            {
                Book = _mockBook,
            };

            var mockContext = CreateMockContext(null); // Create mock context without book
            mockContext.Setup(ctx => ctx.SaveChangesAsync(default)).ReturnsAsync(1);
            var repository = new BooksRepository(mockContext.Object);

            var result = await repository.AddBookAsync(request);

            Assert.Null(result.ErrorMessage);
        }

        [Fact]
        public async Task AddBookAsync_AddDuplicate_Fails()
        {
            var request = new AddBookRequest
            {
                Book = _mockBook,
            };

            var mockContext = CreateMockContext(_mockBook);
            var repository = new BooksRepository(mockContext.Object);

            var result = await repository.AddBookAsync(request);

            Assert.Equal(Messaging.ErrorMessages.DuplicateBookExists, result.ErrorMessage);
        }

        [Fact]
        public async Task DeleteBookAsync_DeletesBook_Success()
        {
            var request = new DeleteBookRequest { Uid = Guid.Empty.ToString() }; // Empty guid always matches our _mockBook
            var mockContext = CreateMockContext(_mockBook);
            mockContext.Setup(ctx => ctx.SaveChangesAsync(default)).ReturnsAsync(1);
            var repository = new BooksRepository(mockContext.Object);

            var result = await repository.DeleteBookAsync(request);

            Assert.Null(result.ErrorMessage);
        }

        [Fact]
        public async Task DeleteBookAsync_NonExistingBook_Fails()
        {
            var request = new DeleteBookRequest { Uid = Guid.NewGuid().ToString() }; // Guid won't exist in our mocked list.
            var mockContext = CreateMockContext(_mockBook);
            mockContext.Setup(ctx => ctx.SaveChangesAsync(default)).ReturnsAsync(1);
            var repository = new BooksRepository(mockContext.Object);

            var result = await repository.DeleteBookAsync(request);

            Assert.Equal(Messaging.ErrorMessages.BookNotFound, result.ErrorMessage);
        }
    }
}
