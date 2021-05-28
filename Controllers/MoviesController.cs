using Microsoft.AspNetCore.Mvc;
using APIMoviesBD.Domain.Models;
using APIMoviesBD.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIMoviesBD.Controllers
{
    [Route("/api/[controller]")]
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IEnumerable<MovieModel>> GetAllAsync()
        {
            var movies = await _movieService.ListAsync();
            return movies;
        }
    }
}
