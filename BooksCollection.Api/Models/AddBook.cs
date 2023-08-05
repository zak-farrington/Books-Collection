using System.ComponentModel.DataAnnotations;

namespace BooksCollection.Api.Models
{
    public class AddBookRequest
    {
        [Required(ErrorMessage = "Book details are required.")]
        public Book Book { get; set; }
    }

    public class AddBookResponse : BaseApiResponse
    {

    }
}
