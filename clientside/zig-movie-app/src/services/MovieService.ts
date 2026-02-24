import axios, { AxiosInstance } from 'axios';
import { Movie } from '../types/Movie';

class MovieService {
  private api: AxiosInstance;
  private baseURL: string;

  constructor() {
    this.baseURL = process.env.REACT_APP_API_URL || 'https://localhost:5001/api';
    this.api = axios.create({
      baseURL: this.baseURL,
      headers: {
        'Content-Type': 'application/json',
      },
    });
  }

  /**
   * Get popular movies
   */
  async getPopularMovies(): Promise<Movie[]> {
    try {
      const response = await this.api.get<Movie[]>('/popular');
      return response.data;
    } catch (error) {
      console.error('Error fetching popular movies:', error);
      throw error;
    }
  }

  /**
   * Search movies by query
   */
  async searchMovies(query: string): Promise<Movie[]> {
    try {
      const response = await this.api.get<Movie[]>('/search', {
        params: { query },
      });
      return response.data;
    } catch (error) {
      console.error('Error searching movies:', error);
      throw error;
    }
  }

  /**
   * Get movie by ID
   */
  async getMovieById(id: number): Promise<Movie> {
    try {
      const response = await this.api.get<Movie>(`/movie/${id}`);
      return response.data;
    } catch (error) {
      console.error('Error fetching movie:', error);
      throw error;
    }
  }
}

export default new MovieService();
