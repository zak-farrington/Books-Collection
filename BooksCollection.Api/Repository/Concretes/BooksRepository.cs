using BookCollectionApi.Models;
using BookCollectionApi.Repository.Interfaces;

namespace BookCollectionApi.Repository.Concretes
{
    public class BooksRepository : IBooksRepository
    {
        public async Task<BooksListResponse> GetBooksListResponse()
        {
            var response = new BooksListResponse();
            // Hardcode for now until I setup SQLight
            var hardCodeBookList = new List<Book>();

            var limitless = new Book
            {
                Uid = new Guid().ToString(),
                Title = "Limitless",
                Description = "Your brain is the most powerful technology in the world, but you never got the owner's manual.  Until now.",
                AuthorName = "Jim Kwik",
                Category = BookCategory.SelfHelp,
                PublishedDate = DateTime.Now, // TODO: Get publish date
                Msrp = (decimal)26.99,
            };

            var theOtherShore = new Book
            {
                Uid = new Guid().ToString(),
                Title = "The Ohter shore",
                Description = "Modern translation of the Heart Sutra about the insight that takes one to the Other Shore.",
                AuthorName = "Thich Nacht Hahn",
                Category = BookCategory.SelfHelp,
                PublishedDate = DateTime.Now, // TODO: Get publish date
                Msrp = (decimal)26.99,
            };


            hardCodeBookList.Add(limitless);
            hardCodeBookList.Add(theOtherShore);

            response.Books = hardCodeBookList;

            return response;
        }
    }
}
