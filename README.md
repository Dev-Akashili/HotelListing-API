# HotelListing-API

This a sample web api built with ASP .NET Core that displays countries some hotels you can find them.

You can perform CRUD operations using swagger docs or postman while testing the api

# Getting Started

**.NET SDK** `6.x`
- The backend API is .NET6 (LTS)

## Database setup

1. Run `docker-compose up`

2. If you haven't already, install the local Entity Framework tooling
  - Anywhere in the repo: `dotnet tool restore`
  
3. Navigate to the directory and in the terminal run migrations:
  - `dotnet ef database update`
  - The above runs against the default local server, using the connection string in `appsettings.Development.json`
