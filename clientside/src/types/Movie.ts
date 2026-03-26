export interface Genre {
  id: number;
  name: string;
}

export interface Movie {
  id: number;
  title: string;
  overview: string;
  poster_path: string;
  release_date: string;
  vote_average: number;
  popularity: number;
  homepage?: string;
  runtime?: number;
  genres?: Genre[];
}

export interface SearchResult {
  results: Movie[];
  total_results: number;
  page: number;
}
