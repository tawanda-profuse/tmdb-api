# Setup Guide

This is a full-stack movie application that showcases popular movies, allows searching by title, and displays detailed information about each movie. The application uses the TMDB (The Movie Database) API as the data source.

**Tech Stack:**

- **Frontend:** React 18 with TypeScript, Bootstrap 5, React Router v6, Axios
- **Backend:** ASP.NET Core 10 (.NET 10) with Swagger/OpenAPI

## System Requirements

- Windows, macOS, or Linux
- Node.js 16+ (for frontend)
- .NET SDK 10.0.103 (for backend)
- TMDB API Key (register at https://www.themoviedb.org/settings/api)

## Setup Instructions

### 1. Backend Setup (Server-side)

#### Step 1.1: Configure TMDB API Key

1. Get your TMDB API key from https://www.themoviedb.org/settings/api
2. Initialize User Secrets by first navigating to the `serverside` folder:

```bash
cd serverside
dotnet user-secrets init
```

This adds a `UserSecretsId` to the `.csproj` file. Then, set your API key:

```bash
dotnet user-secrets set "TmdbApiKey" "your-real-api-key"
```

This stores it securely in:

```Code
%APPDATA%\Microsoft\UserSecrets\
```

This ensures that the API key is not committed, not available in the repo, and automatically loaded in the Development environment.

#### Step 1.2: Restore NuGet Packages and Run the Server

```bash
cd serverside
dotnet restore
dotnet run
```

The server will start on `https://localhost:5001` by default.

**Access Swagger UI:** Open `https://localhost:5001` in your browser to see the API documentation.

#### Step 1.3: Verify API Endpoints
Test the endpoints using Swagger UI:
- **GET /api/popular** - Get top 20 popular movies
- **GET /api/search?query=birdbox** - Search movies by title
- **GET /api/movie/{id}** - Get a specific movie by ID

### 2. Frontend Setup (Client-side)

#### Step 2.1: Install Dependencies
```bash
cd clientside/zig-movie-app
npm install
```

#### Step 2.2: Configure Backend URL

Create a `.env` file with the following content:

```
REACT_APP_API_URL=<the-actual-api-url>
```

#### Step 2.3: Start the Development Server

```bash
npm start
```

The application will open on `http://localhost:3000`

## Set Environment Variable for Production

**On Windows (PowerShell)**:

```Powershell
setx TmdbApiKey "your-real-api-key"
```

**On Linux / macOS**:

```bash
export TmdbApiKey=your-real-api-key
```

## Architecture & Design Patterns

### Server-side Architecture

- **Singleton Pattern:** HttpClient is registered as a singleton to reuse connections
- **Dependency Injection:** All services use constructor-based DI
- **Repository Pattern:** MovieRepository abstracts data access
- **CORS Enabled:** Allows requests from any origin

### Server-side Structure

```
serverside/
├── Models/               # Data models
│   └── Movie.cs
├── Services/             # Business logic (Singleton HttpClient)
│   ├── ITmdbService.cs
│   └── TmdbService.cs
├── Repositories/         # Repository pattern for data access
│   ├── IMovieRepository.cs
│   └── MovieRepository.cs
├── Controllers/          # API endpoints
│   └── MoviesController.cs
│   └── ValuesController.cs
├── Tests/                # Unit tests
│   └── MoviesControllerTests.cs
│   └── MoviesRepositoryTests.cs
├── Startup.cs            # DI and middleware configuration
└── Program.cs            # Entry point
```

### Client-side Structure

```
clientside/zig-movie-app/src/
├── components/           # React components
│   ├── HomePage.tsx
│   ├── SearchBar.tsx
│   ├── SearchResultsPage.tsx
│   └── MovieDetail.tsx
├── services/             # API service layer
│   └── MovieService.ts
├── types/                # TypeScript interfaces
│   └── Movie.ts
└── App.tsx              # Main app with routing
```

## Running Tests

### Backend Unit Tests

```bash
cd serverside
dotnet test
```

### Frontend Testing

```bash
cd clientside/zig-movie-app
npm test
```

## Features

### Homepage

- Displays top 20 most popular movies
- Shows poster image, title, release date, and rating
- Click "View Details" to navigate to movie detail page

### Search

- Search bar in the navbar
- Enter movie title to search
- Results displayed in grid layout
- Click "View Details" for more information

### Movie Detail Page

- Large poster image
- Movie title linked to official website
- Rating and release year badges
- Complete overview/description
- Runtime and genres
- Link to official movie website

### Design

- Bootstrap 5 styling
- Responsive grid layout
- Smooth card hover effects
- Mobile-friendly interface
- Dark navbar with search integration

## Troubleshooting

### CORS Issues

If you see CORS errors, ensure:

1. Server is running on `https://localhost:5001`
2. `.env` in client app has the correct URL
3. Server has CORS policy configured (should be pre-configured)

### API Key Issues

- Verify your TMDB API key is valid.
- Check that it's properly set.
- Restart the server after updating the key.

### Port Already in Use

If ports are already in use:

- Server: Modify `launchSettings.json`
- Client: Set `PORT` environment variable before running `npm start`

### Certificate Issues

If you see SSL certificate warnings:

1. This is normal for localhost development
2. Accept the warning in your browser
3. For production, use a valid certificate

## Additional Notes

- The application uses HTTPS for all communications
- Data is fetched on-demand from TMDB API
- No data is cached locally (stateless client/server)
- All requests go through the ASP.NET Core backend
- The frontend communicates only with the backend API
