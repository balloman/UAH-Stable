using System;
using System.Linq;
using System.Threading.Tasks;

namespace Stable_Frontend.Data
{
    public class WeatherForecastService {
        private static readonly string[] Categories = new[]
        {
            "Campus Environment", "Food", "Parking", "Food", "Campus Life"
        };
        
        private static readonly string[] Names = new[]
        {
            "Parker Smith", "Mary Beeds", "James Henderson", "Riley Stuart", "William Roach", "Jesse Stummer", "Keira Balmy", "Eva Honduras", "Christopher Sworten", "Gena Reeves"
        };
        private static readonly string[] Titles = new[]
        {
            "Dirty Campus Maps", "Stop Requiring Meal Plans", "Parking Lot Access Is Too Limited For Students Living On Campus", "Open Campus Restaurants!", "COVID Restriction is too loose",
            "Covid Policies Are Taking Our Freedom Away!"
        };

        public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            var rng = new Random();
            return Task.FromResult(Enumerable.Range(0, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                Upvote = rng.Next(0, 60),
                Downvote = rng.Next(0, 20),
                Category = Categories[index],
                Title = Titles[index],
                Name = Names[rng.Next(0,Names.Length)],
            }).ToArray()); 
        }
    }
}
