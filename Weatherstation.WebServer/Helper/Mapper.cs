using MudBlazor;
using Weatherstation.Data.Enums;
using Weatherstation.Data.Models;

namespace Weatherstation.WebServer.Helper
{
    public class Mapper
    {
        public static ChartSeries MapToCS(List<WeatherEntry> weatherEntries, string name)
        {

            double[] doubles = new double[weatherEntries.Count];
            for (int i = 0; i < weatherEntries.Count; i++)
            {
                doubles[i] = weatherEntries[i].Value;
            }

            new ChartSeries()
            {
                Name = name,
                Data = doubles
            };

            return new ChartSeries();
        }


    }
}
