using APIMoviesBD.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIMoviesBD.Domain.Repositories
{
    public interface IAPIMovieBDClient
    {
        Task<IEnumerable<MovieModel>> ListAsync();
    }
}
