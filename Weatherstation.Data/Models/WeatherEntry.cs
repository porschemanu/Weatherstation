using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weatherstation.Data.Enums;

namespace Weatherstation.Data.Models
{
    public class WeatherEntry
    {
        [Key]
        public int ID { get; set; }
        public double Value { get; set; }

        public DateTime Timestamp { get; set; }

        public WeatherValueType ValueType { get; set; }
    }
}
