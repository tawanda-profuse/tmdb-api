import React, { useState } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';
import HomePage from './components/HomePage';
import SearchResultsPage from './components/SearchResultsPage';
import MovieDetail from './components/MovieDetail';
import SearchBar from './components/SearchBar';

const App: React.FC = () => {
  const [searchPerformed, setSearchPerformed] = useState(false);

  const handleSearch = (query: string) => {
    setSearchPerformed(true);
  };

  return (
    <Router>
      <div className="App">
        <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
          <div className="container-fluid">
            <a className="navbar-brand" href="/">
              🎬 Movie App
            </a>
            <button
              className="navbar-toggler"
              type="button"
              data-bs-toggle="collapse"
              data-bs-target="#navbarNav"
            >
              <span className="navbar-toggler-icon"></span>
            </button>
            <div className="collapse navbar-collapse" id="navbarNav">
              <div className="ms-auto w-25">
                <SearchBar onSearch={handleSearch} />
              </div>
            </div>
          </div>
        </nav>

        <Routes>
          <Route path="/" element={<HomePage />} />
          <Route path="/search" element={<SearchResultsPage />} />
          <Route path="/movie/:id" element={<MovieDetail />} />
        </Routes>

        <footer className="bg-dark text-light text-center py-4 mt-5">
          <p>&copy; 2024 Movie App. Data provided by TMDB.</p>
        </footer>
      </div>
    </Router>
  );
};

export default App;
