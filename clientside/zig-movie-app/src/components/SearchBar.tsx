import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

interface SearchBarProps {
  onSearch: (query: string) => void;
}

const SearchBar: React.FC<SearchBarProps> = ({ onSearch }) => {
  const [query, setQuery] = useState('');
  const navigate = useNavigate();

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (query.trim()) {
      navigate(`/search?query=${encodeURIComponent(query)}`);
      onSearch(query);
    }
  };

  return (
    <form onSubmit={handleSubmit} className="d-flex" role="search">
      <input
        className="form-control me-2"
        type="search"
        placeholder="Search movies..."
        value={query}
        onChange={(e) => setQuery(e.target.value)}
      />
      <button className="btn btn-outline-primary" type="submit">
        Search
      </button>
    </form>
  );
};

export default SearchBar;
