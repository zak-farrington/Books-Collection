using BooksCollection.Api.Decorators;
using BooksCollection.Api.Models;
using BooksCollection.Api.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BooksCollection.Api.Controllers
{
    [Route("api/google-books")]
    [ApiController]
    public class GoogleBooksController : ControllerBase
    {
        private readonly IGoogleBooksRepository _googleBooksRepo;

        public GoogleBooksController(IGoogleBooksRepository googleBooksRepo) => _googleBooksRepo = googleBooksRepo;

        [HttpGet("search")]
        [ValidateModelState]
        public async Task<GoogleBooksSearchResponse> SearchGoogleBooksAsync(string title)
        {
            var response = await _googleBooksRepo.SearchGoogleBooksAsync(title);

            return response;
        }
    }
}
