using Google.Apis.Books.v1.Data;
using System.ComponentModel.DataAnnotations;

namespace BookCollectionApi.Models
{
    public class GoogleBooksSearchRequest 
    {
        [Required]
        public string Title { get; set; }
    }

    public class GoogleBooksSearchResponse : BaseApiResponse
    {
        public List<Book> Books { get; set; }
    }
}
