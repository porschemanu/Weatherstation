using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weatherstation.Data.Context;
using Weatherstation.Data.Enums;
using Weatherstation.Data.Models;

namespace Weatherstation.Data.Repositories
{
    public class WeatherEntryRepo : IDisposable
    {
        private readonly WeatherDataContext _context;

        public WeatherEntryRepo()
        {

            DbContextOptionsBuilder<WeatherDataContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<WeatherDataContext>();
            dbContextOptionsBuilder.UseSqlServer("Server=localhost;database=BBS_Weatherstation;trusted_connection=true;Encrypt=false;");

            _context = new WeatherDataContext(dbContextOptionsBuilder.Options);
        }

        public WeatherEntryRepo(WeatherDataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Create(WeatherEntry weatherEntry)
        {
            _context.WeatherEntries.Add(weatherEntry);
            _context.SaveChanges();
        }

        public async Task<WeatherEntry> ReadLastAsync(WeatherValueType weatherValueType)
        {
            WeatherEntry? _ = await _context.WeatherEntries.Where(x => x.ValueType == weatherValueType).OrderBy(x => x.Timestamp).LastOrDefaultAsync();
            if (_ != null)
            {
                return _;
            }
            else
            {
                return new WeatherEntry();
            }
        }

        public List<WeatherEntry> Read(WeatherValueType weatherValueType)
        {
            return _context.WeatherEntries.Where(x => x.ValueType == weatherValueType).ToList();
        }

        public List<WeatherEntry> Read(WeatherValueType weatherValueType, DateOnly date)
        {
            DateTime begin = date.ToDateTime(TimeOnly.MinValue);
            DateTime end = date.ToDateTime(TimeOnly.MaxValue);
            return _context.WeatherEntries.Where(x => x.ValueType == weatherValueType).Where(x => x.Timestamp < end && x.Timestamp > begin).ToList();
        }

		public List<WeatherEntry> Read(WeatherValueType weatherValueType, DateTime begin, DateTime end)
		{
            var test = _context.WeatherEntries.Where(x => x.ValueType == weatherValueType).Where(x => x.Timestamp < end && x.Timestamp > begin).ToList();
            return test;
		}

		public void Update(WeatherEntry weatherEntry)
        {
            _context.WeatherEntries.Update(weatherEntry);
        }

        public void Delete(WeatherEntry weatherEntry)
        {
            _context.WeatherEntries.Remove(weatherEntry);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
