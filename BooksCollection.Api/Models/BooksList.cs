namespace BooksCollection.Api.Models
{
    public class BooksListRequest
    {
    }

    public class BooksListResponse : BaseApiResponse
    {
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
