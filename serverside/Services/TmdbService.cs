using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using serverside.Models;

namespace serverside.Services
{
    public class TmdbService : ITmdbService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://api.themoviedb.org/3";
        private readonly string _apiKey;
        private readonly ILogger<TmdbService> _logger;

        public TmdbService(HttpClient httpClient, IConfiguration configuration, ILogger<TmdbService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;

            _apiKey = configuration["TmdbApiKey"];

            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new InvalidOperationException(
                    "TMDB API key is not configured. Set it via User Secrets or environment variables."
                );
            }
        }

        public async Task<List<Movie>> GetPopularMoviesAsync()
        {
            var url = $"{BaseUrl}/movie/popular?api_key={_apiKey}&page=1";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var movieResponse = JsonSerializer.Deserialize<MovieResponse>(json, options);

            return movieResponse?.Results.Take(20).ToList() ?? new List<Movie>();
        }

        public async Task<List<Movie>> SearchMoviesAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return new List<Movie>();
            }

            var url = $"{BaseUrl}/search/movie?api_key={_apiKey}&query={Uri.EscapeDataString(query)}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var movieResponse = JsonSerializer.Deserialize<MovieResponse>(json, options);

            return movieResponse?.Results ?? new List<Movie>();
        }

        public async Task<Movie> GetMovieByIdAsync(int movieId)
        {
            var url = $"{BaseUrl}/movie/{movieId}?api_key={_apiKey}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var movie = JsonSerializer.Deserialize<Movie>(json, options);

            return movie ?? new Movie();
        }
    }
}
