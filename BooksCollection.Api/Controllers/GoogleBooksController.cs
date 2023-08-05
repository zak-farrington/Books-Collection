﻿using BookCollectionApi.Models;
using BookCollectionApi.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookCollectionApi.Controllers
{
    [Route("api/google-books")]
    [ApiController]
    public class GoogleBooksController : ControllerBase
    {
        private readonly IGoogleBooksRepository _googleBooksRepo;
       
        public GoogleBooksController(IGoogleBooksRepository googleBooksRepo) => _googleBooksRepo = googleBooksRepo;

        [HttpPost("search")]
        public async Task<GoogleBooksSearchResponse> SearchGoogleBooksAsync(GoogleBooksSearchRequest request)
        {
            var response = await _googleBooksRepo.SearchGoogleBooksAsync(request).ConfigureAwait(false);

            return response;
        }
    }
}