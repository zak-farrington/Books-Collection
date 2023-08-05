using BookCollectionApi.Models;

namespace BookCollectionApi.Repository.Interfaces
{
    public interface IBooksRepository
    {
        public Task<BooksListResponse> GetBooksListResponse();
        //public Book GetById(int uid);
        //public bool AddBook(Book book);
        //public bool UpdateBook(Book book);
        //public bool DeleteBook(int uid);
    }
}
