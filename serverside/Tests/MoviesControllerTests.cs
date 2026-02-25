using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using serverside.Controllers;
using serverside.Models;
using serverside.Repositories;
using Microsoft.Extensions.Logging;

namespace serverside.Tests
{
    public class MoviesControllerTests
    {
        private readonly Mock<IMovieRepository> _mockRepository;
        private readonly Mock<ILogger<MoviesController>> _mockLogger;
        private readonly MoviesController _controller;

        public MoviesControllerTests()
        {
            _mockRepository = new Mock<IMovieRepository>();
            _mockLogger = new Mock<ILogger<MoviesController>>();
            _controller = new MoviesController(_mockRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetPopularMovies_ReturnsOkResult_WithMoviesList()
        {
            // Arrange
            var movies = new List<Movie>
            {
                new Movie { Id = 1, Title = "Movie 1", Overview = "Overview 1" },
                new Movie { Id = 2, Title = "Movie 2", Overview = "Overview 2" }
            };
            _mockRepository.Setup(r => r.GetPopularMoviesAsync())
                .ReturnsAsync(movies);

            // Act
            var actionResult = await _controller.GetPopularMovies();

            // Assert
            var okResult = actionResult.Result as Microsoft.AspNetCore.Mvc.OkObjectResult;
            Assert.NotNull(okResult);
            var returned = okResult.Value as List<Movie>;
            Assert.NotNull(returned);
            Assert.Equal(2, returned.Count);
            _mockRepository.Verify(r => r.GetPopularMoviesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetPopularMovies_ReturnServerError_WhenExceptionThrown()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetPopularMoviesAsync())
                .ThrowsAsync(new Exception("API Error"));

            // Act
            var result = await _controller.GetPopularMovies();

            // Assert
            Assert.NotNull(result);
            var objectResult = result.Result as ObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(500, objectResult.StatusCode);
        }

        [Fact]
        public async Task SearchMovies_ReturnsBadRequest_WhenQueryIsEmpty()
        {
            // Act
            var result = await _controller.SearchMovies("");

            // Assert
            var objectResult = result.Result as BadRequestObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public async Task SearchMovies_ReturnsOkResult_WithSearchResults()
        {
            // Arrange
            var query = "Avatar";
            var movies = new List<Movie>
            {
                new Movie { Id = 19995, Title = "Avatar", Overview = "Avatar Overview" }
            };
            _mockRepository.Setup(r => r.SearchMoviesAsync(query))
                .ReturnsAsync(movies);

            // Act
            var actionResult = await _controller.SearchMovies(query);

            // Assert
            var okResult = actionResult.Result as Microsoft.AspNetCore.Mvc.OkObjectResult;
            Assert.NotNull(okResult);
            var returned = okResult.Value as List<Movie>;
            Assert.NotNull(returned);
            Assert.Single(returned);
            _mockRepository.Verify(r => r.SearchMoviesAsync(query), Times.Once);
        }

        [Fact]
        public async Task GetMovieById_ReturnsBadRequest_WhenIdIsInvalid()
        {
            // Act
            var result = await _controller.GetMovieById(-1);

            // Assert
            var objectResult = result.Result as BadRequestObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(400, objectResult.StatusCode);
        }

        [Fact]
        public async Task GetMovieById_ReturnsOkResult_WithMovie()
        {
            // Arrange
            var movieId = 550;
            var movie = new Movie { Id = movieId, Title = "Fight Club", Overview = "Fight Club Overview" };
            _mockRepository.Setup(r => r.GetMovieByIdAsync(movieId))
                .ReturnsAsync(movie);

            // Act
            var actionResult = await _controller.GetMovieById(movieId);

            // Assert
            var okResult = actionResult.Result as Microsoft.AspNetCore.Mvc.OkObjectResult;
            Assert.NotNull(okResult);
            var returned = okResult.Value as Movie;
            Assert.NotNull(returned);
            Assert.Equal(movieId, returned.Id);
            _mockRepository.Verify(r => r.GetMovieByIdAsync(movieId), Times.Once);
        }

        [Fact]
        public async Task GetMovieById_ReturnsNotFound_WhenMovieDoesNotExist()
        {
            // Arrange
            var movieId = 999999;
            _mockRepository.Setup(r => r.GetMovieByIdAsync(movieId))
                .ReturnsAsync(new Movie { Id = 0 });

            // Act
            var result = await _controller.GetMovieById(movieId);

            // Assert
            var objectResult = result.Result as NotFoundObjectResult;
            Assert.NotNull(objectResult);
            Assert.Equal(404, objectResult.StatusCode);
        }
    }
}
