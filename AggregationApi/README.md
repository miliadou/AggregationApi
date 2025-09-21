# Aggregator API

A .NET API that aggregates data from multiple external services:

- **Weather** (OpenWeather API)  
- **News** (NewsAPI)  
- **Spotify** (New Releases)  

It fetches aggregated data for a city with optional filtering, sorting, and limits for Spotify tracks.

## **Base URL**

https://localhost:44308/api

## **Endpoints**

### POST `/aggregator`

Fetch aggregated data from Weather, News, and Spotify APIs.

## **Request Body**

{
  "City": "London",
  "CountryCode": "gb",
  "NewsSortBy": "author",
  "SpotifyLimit": 5
}

## **Response Body**

{
  "City": "London",
  "Weather": {
    "Temperature": 20,
    "Description": "Clear sky"
  },
  "News": [
    {
      "Author": "Alice",
      "Title": "News 1",
      "PublishedAt": "2025-09-21T12:00:00Z",
      "Url": "https://example.com/news1"
    }
  ],
  "SpotifyTopTracks": [
    {
      "Name": "Track 1",
      "Artist": "Artist 1",
      "Url": "https://open.spotify.com/track/abc123"
    }
  ]
}

## **Set up**
Set up API keys using Secret Manager
dotnet user-secrets set "WeatherApiKey" "<your-openweather-key>" 
dotnet user-secrets set "NewsApiKey" "<your-newsapi-key>" 
dotnet user-secrets set "ClientId" "<your-spotify-clientId-key>" 
dotnet user-secrets set "ClientSecret" "<your-spotify-clientSecret-key>" 