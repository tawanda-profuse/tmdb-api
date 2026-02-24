using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using serverside.Models;

namespace serverside.Repositories
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetPopularMoviesAsync();
        Task<List<Movie>> SearchMoviesAsync(string query);
        Task<Movie> GetMovieByIdAsync(int movieId);
    }
}
