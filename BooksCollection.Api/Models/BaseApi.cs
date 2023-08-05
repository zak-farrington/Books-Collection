namespace BooksCollection.Api.Models
{
    public class BaseApiResponse
    {
        public string ErrorMessage { get; set; }
        public bool IsSuccessful => string.IsNullOrEmpty(ErrorMessage);

    }

    public class BaseApiRequest
    {

    }
}
