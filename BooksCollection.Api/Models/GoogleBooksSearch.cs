using System.ComponentModel.DataAnnotations;

namespace BooksCollection.Api.Models
{
    public class GoogleBooksSearchRequest
    {
        [Required(ErrorMessage = "Title is required to search books.")]
        public string Title { get; set; }
    }

    public class GoogleBooksSearchResponse : BaseApiResponse
    {
        public List<Book> Books { get; set; }
    }
}
