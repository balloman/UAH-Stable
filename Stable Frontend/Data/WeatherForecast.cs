using System;

namespace Stable_Frontend.Data
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public string Title { get; set; }

        public int Upvote { get; set; }
        public int Downvote { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
    }
}
