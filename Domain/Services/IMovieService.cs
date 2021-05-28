using APIMoviesBD.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIMoviesBD.Domain.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieModel>> ListAsync();
    }
}
