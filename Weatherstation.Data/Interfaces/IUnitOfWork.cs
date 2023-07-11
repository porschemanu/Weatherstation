using System;
using Weatherstation.Data.Enums;
using Weatherstation.Data.Models;

namespace Weatherstation.Data.Interfaces
{
	public interface IUnitOfWork
	{
        public void Create(WeatherEntry weatherEntry);

        public (List<WeatherEntry> avg, List<WeatherEntry> min, List<WeatherEntry> max) Get30Days(WeatherValueType weatherValueType, DateOnly date);

        public (List<WeatherEntry> avg, List<WeatherEntry> min, List<WeatherEntry> max) Get7Days(WeatherValueType weatherValueType, DateOnly date);

        public List<WeatherEntry> GetAll(WeatherValueType weatherValueType);

        public (List<WeatherEntry> avg, List<WeatherEntry> min, List<WeatherEntry> max) GetCustom(WeatherValueType weatherValueType, DateOnly date);

        public List<WeatherEntry> GetDay(WeatherValueType weatherValueType, DateTime date);

        public Task<WeatherEntry> GetLastAsync(WeatherValueType weatherValueType);

        public (List<WeatherEntry> avg, List<WeatherEntry> min, List<WeatherEntry> max) GetMonth(WeatherValueType weatherValueType, DateOnly date);

        public List<WeatherEntry> GetToday(WeatherValueType weatherValueType);
    }
}

