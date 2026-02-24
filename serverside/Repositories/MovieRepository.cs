using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using serverside.Models;
using serverside.Services;

namespace serverside.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly ITmdbService _tmdbService;

        public MovieRepository(ITmdbService tmdbService)
        {
            _tmdbService = tmdbService ?? throw new ArgumentNullException(nameof(tmdbService));
        }

        public async Task<List<Movie>> GetPopularMoviesAsync()
        {
            return await _tmdbService.GetPopularMoviesAsync();
        }

        public async Task<List<Movie>> SearchMoviesAsync(string query)
        {
            return await _tmdbService.SearchMoviesAsync(query);
        }

        public async Task<Movie> GetMovieByIdAsync(int movieId)
        {
            return await _tmdbService.GetMovieByIdAsync(movieId);
        }
    }
}
