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

            // Try configuration key first (appsettings.json), then common environment variable names.
            var cfgKey = configuration["TmdbApiKey"];
            var envKey1 = Environment.GetEnvironmentVariable("TMDB_API_KEY");
            var envKey2 = Environment.GetEnvironmentVariable("TmdbApiKey");
            var envKey3 = configuration["TMDB_API_KEY"]; // in case env vars were loaded into IConfiguration under this name

            _apiKey = cfgKey ?? envKey1 ?? envKey2 ?? envKey3;

            if (!string.IsNullOrEmpty(cfgKey))
            {
                _logger?.LogInformation("TMDB API key found in configuration (TmdbApiKey).");
            }
            else if (!string.IsNullOrEmpty(envKey1))
            {
                _logger?.LogInformation("TMDB API key found in environment variable TMDB_API_KEY.");
            }
            else if (!string.IsNullOrEmpty(envKey2))
            {
                _logger?.LogInformation("TMDB API key found in environment variable TmdbApiKey.");
            }
            else if (!string.IsNullOrEmpty(envKey3))
            {
                _logger?.LogInformation("TMDB API key found in configuration under TMDB_API_KEY.");
            }

            if (string.IsNullOrEmpty(_apiKey))
            {
                _logger?.LogError("TMDB API key not found in configuration or environment.");
                throw new InvalidOperationException("TMDB API key is not configured. Set it in appsettings.json, user secrets, or TMDB_API_KEY environment variable.");
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
