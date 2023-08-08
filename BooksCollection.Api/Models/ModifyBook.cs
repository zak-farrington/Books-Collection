using System.ComponentModel.DataAnnotations;

namespace BooksCollection.Api.Models
{
    public class ModifyBookRequest
    {
        [Required(ErrorMessage = "Book details are required.")]
        public Book Book { get; set; }
    }

    public class ModifyBookResponse : BaseApiResponse
    {

    }
}
