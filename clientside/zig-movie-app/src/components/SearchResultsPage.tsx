import React, { useState, useEffect } from 'react';
import { useSearchParams } from 'react-router-dom';
import { Link } from 'react-router-dom';
import MovieService from '../services/MovieService';
import { Movie } from '../types/Movie';

const SearchResultsPage: React.FC = () => {
  const [searchParams] = useSearchParams();
  const [movies, setMovies] = useState<Movie[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const query = searchParams.get('query') || '';

  useEffect(() => {
    const performSearch = async () => {
      if (!query.trim()) {
        setError('No search query provided');
        setLoading(false);
        return;
      }

      try {
        setLoading(true);
        const data = await MovieService.searchMovies(query);
        setMovies(data);
      } catch (err) {
        setError('Failed to search movies');
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    performSearch();
  }, [query]);

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

  if (error) {
    return (
      <div className="container my-5">
        <div className="alert alert-danger" role="alert">
          {error}
        </div>
      </div>
    );
  }

  return (
    <div className="container my-5">
      <h1 className="mb-4">Search Results for "{query}"</h1>
      {movies.length === 0 ? (
        <div className="alert alert-info">No movies found</div>
      ) : (
        <div className="row">
          {movies.map((movie) => (
            <div key={movie.id} className="col-md-4 mb-4">
              <div className="card h-100">
                {movie.poster_path && (
                  <img
                    src={`https://image.tmdb.org/t/p/w500${movie.poster_path}`}
                    className="card-img-top"
                    alt={movie.title}
                  />
                )}
                <div className="card-body">
                  <h5 className="card-title">{movie.title}</h5>
                  <p className="card-text text-muted">{movie.release_date}</p>
                  <p className="card-text">
                    <small>⭐ {movie.vote_average}/10</small>
                  </p>
                  <Link
                    to={`/movie/${movie.id}`}
                    className="btn btn-primary btn-sm"
                  >
                    View Details
                  </Link>
                </div>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default SearchResultsPage;
