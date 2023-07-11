using System;
using Microsoft.EntityFrameworkCore;
using Weatherstation.Data.Context;
using Weatherstation.Data.Enums;
using Weatherstation.Data.Models;

namespace Weatherstation.Data.Interfaces
{
	public interface IWeatherEntryRepo
    {

        public void Create(WeatherEntry weatherEntry);

        public Task<WeatherEntry> ReadLastAsync(WeatherValueType weatherValueType);

        public List<WeatherEntry> Read(WeatherValueType weatherValueType);

        public List<WeatherEntry> Read(WeatherValueType weatherValueType, DateOnly date);

        public List<WeatherEntry> Read(WeatherValueType weatherValueType, DateTime begin, DateTime end);

        public void Update(WeatherEntry weatherEntry);

        public void Delete(WeatherEntry weatherEntry);
    }
}


