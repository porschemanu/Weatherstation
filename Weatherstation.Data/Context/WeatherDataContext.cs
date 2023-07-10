using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Weatherstation.Data.Context
{
    public class WeatherDataContext : DbContext
    {
        public WeatherDataContext(DbContextOptions<WeatherDataContext> options) : base(options)
        {

        }

        public DbSet<Models.WeatherEntry> WeatherEntries { get; set; }
    }
}
