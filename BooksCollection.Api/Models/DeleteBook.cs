using System.ComponentModel.DataAnnotations;

namespace BooksCollection.Api.Models
{
    public class DeleteBookRequest
    {
        [Required(ErrorMessage = "Uid is required to delete book from collection.")]
        public string Uid { get; set; }
    }

    public class DeleteBookResponse : BaseApiResponse
    {
    }
}
