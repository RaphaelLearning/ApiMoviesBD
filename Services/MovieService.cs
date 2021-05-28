using APIMoviesBD.Domain.Models;
using APIMoviesBD.Domain.Repositories;
using APIMoviesBD.Domain.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIMoviesBD.Services
{
    public class MovieService : IMovieService
    {
        private readonly IAPIMovieBDClient _movieClient;
        public MovieService(IAPIMovieBDClient movieClient)
        {
            _movieClient = movieClient;
        }

        public async Task<IEnumerable<MovieModel>> ListAsync()
        {
            return await _movieClient.ListAsync();
        }
    }
}
