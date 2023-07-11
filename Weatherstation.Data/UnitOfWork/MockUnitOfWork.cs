using System;
using Weatherstation.Data.Enums;
using Weatherstation.Data.Interfaces;
using Weatherstation.Data.Models;

namespace Weatherstation.Data.UnitOfWork
{
	public class MockUnitOfWork : IUnitOfWork
	{
        public List<WeatherEntry> WeatherEntries { get; set; } = new();

        private void InitializeListRandom(int count)
        {
            Random rand = new();
            for (int i = 0; i < count; i++)
            {
                WeatherEntries.Add(new WeatherEntry()
                {
                    ID = i,
                    Value = rand.Next(0,100),
                    Timestamp = DateTime.UtcNow.AddHours(-i),
                    ValueType = WeatherValueType.Temperature
                });
            }

            for (int i = 0; i < count; i++)
            {
                WeatherEntries.Add(new WeatherEntry()
                {
                    ID = i,
                    Value = rand.Next(45, 85),
                    Timestamp = DateTime.UtcNow.AddHours(-i),
                    ValueType = WeatherValueType.Humidity
                });
            }

            for (int i = 0; i < count; i++)
            {
                WeatherEntries.Add(new WeatherEntry()
                {
                    ID = i,
                    Value = rand.Next(950, 1050),
                    Timestamp = DateTime.UtcNow.AddHours(-i),
                    ValueType = WeatherValueType.Airpressure
                });
            }
        }

		public MockUnitOfWork()
		{
            InitializeListRandom(1000);
		}

        public void Create(WeatherEntry weatherEntry)
        {
            WeatherEntries.Add(weatherEntry);
        }

        public (List<WeatherEntry> avg, List<WeatherEntry> min, List<WeatherEntry> max) Get30Days(WeatherValueType weatherValueType, DateOnly date)
        {
            return (WeatherEntries.Average, WeatherEntries.Min, WeatherEntries.Max);
        }

        public (List<WeatherEntry> avg, List<WeatherEntry> min, List<WeatherEntry> max) Get7Days(WeatherValueType weatherValueType, DateOnly date)
        {
            throw new NotImplementedException();
        }

        public List<WeatherEntry> GetAll(WeatherValueType weatherValueType)
        {
            throw new NotImplementedException();
        }

        public (List<WeatherEntry> avg, List<WeatherEntry> min, List<WeatherEntry> max) GetCustom(WeatherValueType weatherValueType, DateOnly date)
        {
            DateTime begin = date.ToDateTime(TimeOnly.MinValue);
            DateTime end = date.ToDateTime(TimeOnly.MaxValue);
            WeatherEntries.Where(x => x.ValueType == weatherValueType).Where(x => x.Timestamp < end && x.Timestamp > begin)
        }

        public List<WeatherEntry> GetDay(WeatherValueType weatherValueType, DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<WeatherEntry> GetLastAsync(WeatherValueType weatherValueType)
        {
            throw new NotImplementedException();
        }

        public (List<WeatherEntry> avg, List<WeatherEntry> min, List<WeatherEntry> max) GetMonth(WeatherValueType weatherValueType, DateOnly date)
        {
            throw new NotImplementedException();
        }

        public List<WeatherEntry> GetToday(WeatherValueType weatherValueType)
        {
            throw new NotImplementedException();
        }
    }
}

