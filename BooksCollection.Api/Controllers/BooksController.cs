using BookCollectionApi.Models;
using BookCollectionApi.Repository.Concretes;
using BookCollectionApi.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookCollectionApi.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _booksRepo;
       
        public BooksController (IBooksRepository booksRepo) => _booksRepo = booksRepo;  

        [HttpGet("list")]
        public Task<BooksListResponse> GetBooksList()
        {
            var response = _booksRepo.GetBooksListResponse();

            return response;
        }

        [HttpPost("add")]
        public bool AddBookAsync()
        {
            // TODO: Placeholder to add a book
            var response = _booksRepo.GetBooksListResponse();

            return true;
        }


        [HttpPost("delete")]
        public bool DeleteBookAsync()
        {
            // TODO: Placeholder to delete a book
            var response = _booksRepo.GetBooksListResponse();

            return true;
        }
    }
}
