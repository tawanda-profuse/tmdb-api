using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using serverside.Models;
using serverside.Repositories;

namespace serverside.Controllers
{
    [ApiController]
    [Route("api")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ILogger<MoviesController> _logger;

        public MoviesController(IMovieRepository movieRepository, ILogger<MoviesController> logger)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets the top 20 most popular movies
        /// </summary>
        /// <returns>List of popular movies</returns>
        [HttpGet("popular")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Movie>>> GetPopularMovies()
        {
            try
            {
                var movies = await _movieRepository.GetPopularMoviesAsync();
                return Ok(movies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching popular movies");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error fetching popular movies" });
            }
        }

        /// <summary>
        /// Searches for movies by title
        /// </summary>
        /// <param name="query">The search query</param>
        /// <returns>List of movies matching the query</returns>
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Movie>>> SearchMovies([FromQuery] string query)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                {
                    return BadRequest(new { message = "Query parameter is required" });
                }

                var movies = await _movieRepository.SearchMoviesAsync(query);
                return Ok(movies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching movies");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error searching movies" });
            }
        }

        /// <summary>
        /// Gets a single movie by ID
        /// </summary>
        /// <param name="id">The movie ID</param>
        /// <returns>Movie details</returns>
        [HttpGet("movie/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Movie>> GetMovieById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { message = "Invalid movie ID" });
                }

                var movie = await _movieRepository.GetMovieByIdAsync(id);

                if (movie == null || movie.Id == 0)
                {
                    return NotFound(new { message = "Movie not found" });
                }

                return Ok(movie);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching movie by ID");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Error fetching movie" });
            }
        }
    }
}
