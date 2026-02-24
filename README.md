# Interview Coding Challenge - Movie Application

A full-stack movie application showcasing popular movies from TMDB, with search functionality and detailed movie information pages.

## 🎬 Project Overview

This is a complete implementation of the interview coding challenge with:
- **Frontend:** React 18 with TypeScript, Bootstrap 5, and modern routing
- **Backend:** ASP.NET Core 10 (.NET 10) with Swagger documentation
- **Database:** TMDB API for movie data
- **Architecture:** Repository Pattern, Dependency Injection, Singleton HttpClient

### ✨ Features Implemented

✅ Homepage listing top 20 popular movies  
✅ Search functionality by movie title  
✅ Movie detail pages with comprehensive information  
✅ Poster images, ratings, genres, runtime  
✅ Links to official movie websites  
✅ Full CORS support  
✅ Swagger UI API documentation  
✅ Unit tests for server-side controllers  
✅ TypeScript for type safety  
✅ Bootstrap 5 responsive design  
✅ Dependency Injection with IoC  
✅ Repository pattern for data access  

## 🚀 Quick Start

### Prerequisites
- Node.js 16+
- .NET SDK 10.0.103
- TMDB API Key from https://www.themoviedb.org/settings/api

### Backend Setup
```bash
cd serverside
# Add your TMDB API key to appsettings.json
dotnet restore
dotnet run
```
Server runs on `https://localhost:5001`  
Swagger UI: `https://localhost:5001`

### Frontend Setup
```bash
cd clientside/zig-movie-app
npm install
npm start
```
App opens on `http://localhost:3000`

## 📋 Detailed Setup Guide

For complete setup instructions, environment variables, architecture details, and troubleshooting, see [SETUP_GUIDE.md](./SETUP_GUIDE.md).

## 🏗️ Project Structure

```
interview-coding-challenge/
├── clientside/zig-movie-app/      # React TypeScript Frontend
│   ├── public/                    # Static files
│   ├── src/
│   │   ├── components/           # React components
│   │   │   ├── HomePage.tsx
│   │   │   ├── SearchBar.tsx
│   │   │   ├── SearchResultsPage.tsx
│   │   │   └── MovieDetail.tsx
│   │   ├── services/             # API service
│   │   │   └── MovieService.ts
│   │   ├── types/                # TypeScript interfaces
│   │   │   └── Movie.ts
│   │   ├── App.tsx              # Main app with routing
│   │   └── index.tsx
│   ├── package.json
│   ├── tsconfig.json
│   └── .env
│
├── serverside/                    # ASP.NET Core Backend
│   ├── Controllers/
│   │   └── MoviesController.cs   # API endpoints
│   ├── Models/
│   │   └── Movie.cs             # Data models
│   ├── Services/
│   │   ├── ITmdbService.cs
│   │   └── TmdbService.cs       # Singleton HttpClient
│   ├── Repositories/
│   │   ├── IMovieRepository.cs
│   │   └── MovieRepository.cs   # Repository pattern
│   ├── Tests/
│   │   └── MoviesControllerTests.cs
│   ├── Startup.cs               # DI & CORS config
│   ├── Program.cs
│   ├── appsettings.json
│   ├── global.json
│   └── serverside.csproj
│
├── SETUP_GUIDE.md               # Detailed setup instructions
└── README.md                    # This file
```

## 📡 API Endpoints

### GET /api/popular
Returns top 20 most popular movies
```bash
curl https://localhost:5001/api/popular
```

### GET /api/search?query={query}
Searches movies by title
```bash
curl "https://localhost:5001/api/search?query=avatar"
```

### GET /api/movie/{id}
Returns details for a specific movie
```bash
curl https://localhost:5001/api/movie/550
```

## 🛠️ Design Patterns & Architecture

### Singleton Pattern
HttpClient is registered as a singleton to efficiently reuse connections and reduce overhead.

### Dependency Injection
All services use constructor-based dependency injection for loose coupling and testability.

### Repository Pattern
MovieRepository abstracts the data access layer, providing a clean interface for controllers.

### CORS Configuration
Enabled for all origins to allow frontend to communicate with backend.

## 🧪 Testing

### Run Backend Tests
```bash
cd serverside
dotnet test
```

Tests include:
- Popular movies retrieval
- Movie search functionality
- Movie detail fetching
- Error handling and edge cases

### Run Frontend Tests
```bash
cd clientside/zig-movie-app
npm test
```

## 📚 Technology Stack

### Frontend
- React 18
- TypeScript
- React Router v6 for navigation
- Bootstrap 5 for styling
- Axios for HTTP requests

### Backend
- ASP.NET Core 10 (.NET 10)
- C# 12
- Swashbuckle for Swagger/OpenAPI
- Xunit for testing
- Moq for mocking

## 🔐 Configuration

### Environment Variables

**Client-side (.env):**
```
REACT_APP_API_URL=https://localhost:5001/api
```

**Server-side (appsettings.json):**
```json
{
  "TmdbApiKey": "YOUR_TMDB_API_KEY"
}
```

## 📖 Documentation

Full API documentation is available via Swagger UI at `https://localhost:5001` when the server is running.

## ⚠️ Requirements Met

✅ Homepage listing popular movies from TMDB API  
✅ Search bar that searches for movies by title  
✅ Links for each movie title to navigate to detail page  
✅ Detail page with poster, title (linked to official site), and description  
✅ All data retrieved from server-side API endpoints  
✅ Correct API routes:
   - `api/popular` - Top 20 most popular movies
   - `api/search?query=...` - Search movies by title
   - `api/movie/{id}` - Get movie details  
✅ Unit tests for server-side controllers  
✅ TypeScript for client-side code  
✅ Bootstrap for styling  
✅ CORS enabled  
✅ Singleton HttpClient pattern  
✅ Dependency Injection throughout  
✅ Repository Pattern for data access  
✅ Swagger UI for API documentation  

## 🐛 Troubleshooting

**CORS Errors:**
- Ensure server is running on `https://localhost:5001`
- Check `.env` has correct API URL
- Verify CORS is enabled in Startup.cs

**API Key Issues:**
- Get key from https://www.themoviedb.org/settings/api
- Add to `appsettings.json`
- Restart server after changes

**Port Already in Use:**
- Modify `launchSettings.json` for server
- Set `PORT` environment variable for client

For more troubleshooting, see [SETUP_GUIDE.md](./SETUP_GUIDE.md#troubleshooting).

## 📝 Prerequisites

1. Visual Studio Code (client-side)
2. Visual Studio Community or VS Code with C# support (server-side)
3. .NET SDK 10.0.103
4. Node.js 16+
5. Git
6. TMDB API Key

## 🔗 Useful Links

- [TMDB API Documentation](https://developer.themoviedb.org/docs)
- [.NET Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [React Documentation](https://react.dev)
- [Bootstrap Documentation](https://getbootstrap.com/docs)
- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)

---

**Last Updated:** 2026-02-24  
**Status:** ✅ Complete

    3. /api/movie/1145 - Returns a single movie by Movie Id (for use by the detail page)
7. Add Unit tests for the Controllers and Repos
8. Client side code should be written in TypeScript

## Hints

- HINT 1: Enable/Allow CORS (Cross-Origin Resource Sharing) on the server-side (https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-2.2)
- HINT 2: Use the DAL Project (Data Access Layer) on the server side to implement the repository pattern (https://docs.microsoft.com/en-us/previous-versions/msp-n-p/ff649690(v=pandp.10))


## Tech

The technologies used to build this app are: 
Client-Side - ReactJS, Vite, TypeScript
Server-Side - .NET Core Web Api (.NET 8 or greater)

## Bonus Points
    
1. Using Singleton Pattern for the Http Client that access the Movie API (Server-Side)
2. Using Inversion of Control (aka Dependency Injection) throughout on the (Server-Side)
3. Using React Hooks, Zustand  or Redux to manage state (Client-Side)
4. Using React Router DOM for routing (Client-Side)
5. Use bootstrap for styling the website (Client-Side)
6. Add Swagger UI to document your API create a new API key and it will be listed under the heading "API Key (v3 auth)" 
 (Server-Side)

## Obtaining an API Token from www.themoviedb.org
1.	Create a profile on www.themoviedb.org
2.	Once you’re logged into your profile, click on your username/profile menu in the top right corner and select “Settings” 
3.	Under “Settings”, click on “API” in the left hand navigation
4.	Under the API Settings, you can


***If you have any questions, difficulties or would simply like to discuss the requirements, please do not hesitate to contact us at support@thezig.io. Asking questions for clarification or requesting help of any kind, is NOT AT ALL disqualifying, in fact we encourage it, it's called teamwork :-)*** 

We look forward to receiving your submission.

Good Luck

