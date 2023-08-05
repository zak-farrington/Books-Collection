using BooksCollection.Api.Decorators;
using BooksCollection.Api.Models;
using BooksCollection.Api.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BooksCollection.Api.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _booksRepo;

        public BooksController(IBooksRepository booksRepo) => _booksRepo = booksRepo;

        [HttpGet("list")]
        public async Task<BooksListResponse> GetBooksList()
        {
            var response = await _booksRepo.GetBooksListResponseAsync();

            return response;
        }

        [HttpPost("add")]
        [ValidateModelState]
        public async Task<AddBookResponse> AddBookAsync(AddBookRequest request)
        {
            var response = await _booksRepo.AddBookAsync(request);

            return response;
        }


        [HttpPost("delete")]
        [ValidateModelState]
        public async Task<DeleteBookResponse> DeleteBookAsync(DeleteBookRequest request)
        {
            var response = await _booksRepo.DeleteBookAsync(request);

            return response;
        }
    }
}
