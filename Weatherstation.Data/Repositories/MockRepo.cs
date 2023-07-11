using System;
using Weatherstation.Data.Enums;
using Weatherstation.Data.Interfaces;
using Weatherstation.Data.Models;

namespace Weatherstation.Data.Repositories
{
	public class MockRepo : IWeatherEntryRepo, IDisposable
	{
		public MockRepo()
		{
		}

        public void Create(WeatherEntry weatherEntry)
        {
            throw new NotImplementedException();
        }

        public void Delete(WeatherEntry weatherEntry)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<WeatherEntry> Read(WeatherValueType weatherValueType)
        {
            throw new NotImplementedException();
        }

        public List<WeatherEntry> Read(WeatherValueType weatherValueType, DateOnly date)
        {
            throw new NotImplementedException();
        }

        public List<WeatherEntry> Read(WeatherValueType weatherValueType, DateTime begin, DateTime end)
        {
            throw new NotImplementedException();
        }

        public Task<WeatherEntry> ReadLastAsync(WeatherValueType weatherValueType)
        {
            throw new NotImplementedException();
        }

        public void Update(WeatherEntry weatherEntry)
        {
            throw new NotImplementedException();
        }
    }
}

