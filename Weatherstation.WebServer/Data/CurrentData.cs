using System.ComponentModel;
using Weatherstation.Data.Models;
using Weatherstation.Data.UnitOfWork;
using Weatherstation.Data.Enums;
using Microsoft.AspNetCore.Components;

namespace Weatherstation.WebServer.Data
{
	public class CurrentData
    {
        [Parameter]
        public WeatherEntry Temperature { get; set; } = new();
        [Parameter]
        public WeatherEntry Humidity { get; set; } = new();
        [Parameter]
        public WeatherEntry Airpressure { get; set; } = new();


        public event Action OnDataChanged;

        private Timer _timer;

		

		public CurrentData()
        {
            Task.Run(() => { new Timer(Refresh, null, 0, 1000); }) ;

            //_timer = new Timer(Refresh, null, 0, 1000);
        }

        public void Refresh(object _)
        {
            using (UnitOfWork _UoW = new())
            {
                Temperature = _UoW.GetLastAsync(WeatherValueType.Temperature).Result;
                Humidity = _UoW.GetLastAsync(WeatherValueType.Humidity).Result;
                Airpressure = _UoW.GetLastAsync(WeatherValueType.Airpressure).Result;

                //Temperature.Value += 1;
                //Humidity.Value += 1;
                //Airpressure.Value += 1;
            }

            OnDataChanged?.Invoke();
            
        }

	}
}
