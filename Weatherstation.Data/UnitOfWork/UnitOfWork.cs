using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weatherstation.Data.Context;
using Weatherstation.Data.Enums;
using Weatherstation.Data.Interfaces;
using Weatherstation.Data.Models;
using Weatherstation.Data.Repositories;

namespace Weatherstation.Data.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork,IDisposable
	{
		private readonly WeatherDataContext _context;
		private WeatherEntryRepo _repo;

		public UnitOfWork()
		{
			DbContextOptionsBuilder<WeatherDataContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<WeatherDataContext>();
			dbContextOptionsBuilder.UseSqlServer("Server=localhost;database=BBS_Weatherstation;trusted_connection=true;Encrypt=false;");

			_context = new WeatherDataContext(dbContextOptionsBuilder.Options);
			_repo = new WeatherEntryRepo(_context);

		}

		public UnitOfWork(WeatherDataContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			_repo = new WeatherEntryRepo(context);
		}

		public void Create(WeatherEntry weatherEntry)
		{
			_repo.Create(weatherEntry);
		}

		private (List<WeatherEntry> avg, List<WeatherEntry> min, List<WeatherEntry> max) GetDays(WeatherValueType weatherValueType, DateOnly date, int days)
		{
			List<WeatherEntry> avg = new();
			List<WeatherEntry> min = new();
			List<WeatherEntry> max = new();
			List<DateOnly> dates = new();
			for (int i = 0; i <= days; i++)
			{
				dates.Add(DateOnly.FromDateTime(DateTime.Now.AddDays(i * -1)));
			}

			foreach (DateOnly item in dates)
			{
				List<WeatherEntry> temp = _repo.Read(weatherValueType, item);
				List<double> doubles = new();

				foreach (WeatherEntry entry in temp)
				{
					doubles.Add(entry.Value);
				}

				avg.Add(new WeatherEntry()
				{
					Timestamp = item.ToDateTime(TimeOnly.MaxValue),
					Value = Math.Round(doubles.Average(), 2)
				});

				min.Add(new WeatherEntry()
				{
					Timestamp = item.ToDateTime(TimeOnly.MaxValue),
					Value = Math.Round(doubles.Min(), 2)
				});

				max.Add(new WeatherEntry()
				{
					Timestamp = item.ToDateTime(TimeOnly.MaxValue),
					Value = Math.Round(doubles.Max(), 2)
				});
			}
			return (avg, min, max);
		}

		public (List<WeatherEntry> avg, List<WeatherEntry> min, List<WeatherEntry> max) Get30Days(WeatherValueType weatherValueType, DateOnly date)
		{
			return GetDays(weatherValueType, date, 30);
		}

		public (List<WeatherEntry> avg, List<WeatherEntry> min, List<WeatherEntry> max) Get7Days(WeatherValueType weatherValueType, DateOnly date)
		{
			return GetDays(weatherValueType, date, 7);
		}

		public List<WeatherEntry> GetAll(WeatherValueType weatherValueType)
		{
			return _repo.Read(weatherValueType);
		}

		public (List<WeatherEntry> avg, List<WeatherEntry> min, List<WeatherEntry> max) GetCustom(WeatherValueType weatherValueType, DateOnly date)
		{
			throw new NotImplementedException();
		}

		public List<WeatherEntry> GetDay(WeatherValueType weatherValueType, DateTime date)
		{
			List<WeatherEntry> entries = new();

			int hours = date.Hour;

			for (int i = 1; i <= hours; i++)
			{
				List<WeatherEntry> temp = _repo.Read(weatherValueType, date.AddHours(-i), date);
				List<double> doubles = new();

				foreach (WeatherEntry entry in temp)
				{
					doubles.Add(entry.Value);
				}
				if (doubles.Count > 0)
				{
					entries.Add(new WeatherEntry()
					{
						Value = Math.Round(doubles.Average(), 2),
						Timestamp = date,
						ValueType = weatherValueType
					});
				}
			}
			return entries;
		}

		public async Task<WeatherEntry> GetLastAsync(WeatherValueType weatherValueType)
		{
			return await _repo.ReadLastAsync(weatherValueType);
		}

		public (List<WeatherEntry> avg, List<WeatherEntry> min, List<WeatherEntry> max) GetMonth(WeatherValueType weatherValueType, DateOnly date)
		{
			throw new NotImplementedException();
		}

		public List<WeatherEntry> GetToday(WeatherValueType weatherValueType)
		{
			return _repo.Read(weatherValueType, DateOnly.FromDateTime(DateTime.Now));
		}

		public void Dispose()
		{
			_context.Dispose();
			_repo.Dispose();
		}
	}
}
