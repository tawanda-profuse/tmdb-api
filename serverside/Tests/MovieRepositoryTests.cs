using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using serverside.Repositories;
using serverside.Services;
using serverside.Models;

namespace serverside.Tests
{
    public class MovieRepositoryTests
    {
        [Fact]
        public void Constructor_ThrowsArgumentNullException_WhenTmdbServiceIsNull()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() => new MovieRepository(null));
        }

        [Fact]
        public async Task GetPopularMoviesAsync_DelegatesToTmdbService_AndReturnsList()
        {
            // Arrange
            var mockService = new Mock<ITmdbService>();
            var sample = new List<Movie> { new Movie { Id = 1, Title = "A" } };
            mockService.Setup(s => s.GetPopularMoviesAsync()).ReturnsAsync(sample);

            var repo = new MovieRepository(mockService.Object);

            // Act
            var result = await repo.GetPopularMoviesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            mockService.Verify(s => s.GetPopularMoviesAsync(), Times.Once);
        }

        [Fact]
        public async Task SearchMoviesAsync_DelegatesQueryToTmdbService_AndReturnsResults()
        {
            // Arrange
            var mockService = new Mock<ITmdbService>();
            var query = "Matrix";
            var sample = new List<Movie> { new Movie { Id = 2, Title = "The Matrix" } };
            mockService.Setup(s => s.SearchMoviesAsync(query)).ReturnsAsync(sample);

            var repo = new MovieRepository(mockService.Object);

            // Act
            var result = await repo.SearchMoviesAsync(query);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("The Matrix", result[0].Title);
            mockService.Verify(s => s.SearchMoviesAsync(query), Times.Once);
        }

        [Fact]
        public async Task GetMovieByIdAsync_DelegatesIdToTmdbService_AndReturnsMovie()
        {
            // Arrange
            var mockService = new Mock<ITmdbService>();
            var id = 42;
            var movie = new Movie { Id = id, Title = "Answer" };
            mockService.Setup(s => s.GetMovieByIdAsync(id)).ReturnsAsync(movie);

            var repo = new MovieRepository(mockService.Object);

            // Act
            var result = await repo.GetMovieByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            mockService.Verify(s => s.GetMovieByIdAsync(id), Times.Once);
        }
    }
}
