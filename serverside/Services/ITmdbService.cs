using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using serverside.Models;

namespace serverside.Services
{
    public interface ITmdbService
    {
        Task<List<Movie>> GetPopularMoviesAsync();
        Task<List<Movie>> SearchMoviesAsync(string query);
        Task<Movie> GetMovieByIdAsync(int movieId);
    }
}
