using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace serverside.Models
{
    public class Movie
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }

        [JsonPropertyName("release_date")]
        public string ReleaseDate { get; set; }

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }

        [JsonPropertyName("popularity")]
        public double Popularity { get; set; }

        [JsonPropertyName("homepage")]
        public string Homepage { get; set; }

        [JsonPropertyName("runtime")]
        public int Runtime { get; set; }

        [JsonPropertyName("genres")]
        public List<Genre> Genres { get; set; } = new();
    }

    public class Genre
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class MovieResponse
    {
        [JsonPropertyName("results")]
        public List<Movie> Results { get; set; } = new();

        [JsonPropertyName("total_results")]
        public int TotalResults { get; set; }

        [JsonPropertyName("page")]
        public int Page { get; set; }
    }
}
