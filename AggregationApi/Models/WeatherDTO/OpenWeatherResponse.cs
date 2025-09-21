namespace AggregationApi.Models.WeatherDTO
{
    public class OpenWeatherResponse
    {
        public List<WeatherItem> Weather { get; set; } // Array with main condition
        public MainInfo Main { get; set; }             // Temperature, humidity
        public WindInfo Wind { get; set; }             // Wind speed
    }

    public class WeatherItem
    {
        public string Main { get; set; }    // e.g., "Clear", "Clouds"
        public string Description { get; set; } // e.g., "clear sky"
        public string Icon { get; set; }       // Icon code for UI
    }

    public class MainInfo
    {
        public double Temp { get; set; }       // Celsius
        public double Feels_Like { get; set; }
        public double Temp_Min { get; set; }
        public double Temp_Max { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }      // %
    }

    public class WindInfo
    {
        public double Speed { get; set; }      // m/s
        public int Deg { get; set; }           // wind direction
    }
    public class WeatherData
    {
        public string Condition { get; set; }    // e.g., "Clear", "Rain"
        public double Temperature { get; set; }  // Celsius
        public double Humidity { get; set; }     // Percentage
        public double WindSpeed { get; set; }    // m/s
    }
}