import React, { useState, useEffect } from 'react';
import { useParams, Link } from 'react-router-dom';
import MovieService from '../services/MovieService';
import { Movie } from '../types/Movie';

const MovieDetail: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const [movie, setMovie] = useState<Movie | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchMovieDetails = async () => {
      try {
        const movieId = parseInt(id || '0');
        const data = await MovieService.getMovieById(movieId);
        setMovie(data);
      } catch (err) {
        setError('Failed to load movie details');
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    fetchMovieDetails();
  }, [id]);

  if (loading) {
    return (
      <div className="container my-5">
        <div className="text-center">
          <div className="spinner-border" role="status">
            <span className="visually-hidden">Loading...</span>
          </div>
        </div>
      </div>
    );
  }

  if (error || !movie) {
    return (
      <div className="container my-5">
        <div className="alert alert-danger" role="alert">
          {error || 'Movie not found'}
        </div>
        <Link to="/" className="btn btn-secondary">
          Back to Home
        </Link>
      </div>
    );
  }

  return (
    <div className="container my-5">
      <Link to="/" className="btn btn-secondary mb-4">
        ← Back to Home
      </Link>

      <div className="row">
        <div className="col-md-4">
          {movie.poster_path && (
            <img
              src={`https://image.tmdb.org/t/p/w500${movie.poster_path}`}
              className="img-fluid rounded"
              alt={movie.title}
            />
          )}
        </div>
        <div className="col-md-8">
          <h1>
            {movie.homepage ? (
              <a href={movie.homepage} target="_blank" rel="noopener noreferrer">
                {movie.title}
              </a>
            ) : (
              movie.title
            )}
          </h1>

          <div className="my-3">
            <span className="badge bg-info">⭐ {movie.vote_average}/10</span>
            {movie.release_date && (
              <span className="badge bg-secondary ms-2">
                {new Date(movie.release_date).getFullYear()}
              </span>
            )}
          </div>

          {movie.runtime && (
            <p className="text-muted">
              <strong>Runtime:</strong> {movie.runtime} minutes
            </p>
          )}

          {movie.genres && movie.genres.length > 0 && (
            <p>
              <strong>Genres:</strong>{' '}
              {movie.genres.map((g) => g.name).join(', ')}
            </p>
          )}

          <h3 className="mt-4">Overview</h3>
          <p>{movie.overview || 'No description available'}</p>

          {movie.popularity && (
            <p className="text-muted">
              <strong>Popularity:</strong> {movie.popularity}
            </p>
          )}

          {movie.homepage && (
            <a
              href={movie.homepage}
              target="_blank"
              rel="noopener noreferrer"
              className="btn btn-primary"
            >
              Visit Official Site
            </a>
          )}
        </div>
      </div>
    </div>
  );
};

export default MovieDetail;
