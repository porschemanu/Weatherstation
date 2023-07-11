// See https://aka.ms/new-console-template for more information
using Weatherstation.Data.Models;
using Weatherstation.Data.UnitOfWork;

internal class Program
{
	private static void Main(string[] args)
	{
		Console.WriteLine("BBS_Wittlich WetterdatenGenerator Version 1.0");
		Console.WriteLine("Limit eingeben");
        int _limit;
        bool _ = int.TryParse(Console.ReadLine(), out _limit);

		if(_)
		{
            UnitOfWork unitOfWork = new UnitOfWork();
            Random random = new Random();

            for (int i = 0; i < _limit; i++)
            {
                if (i % 1000 == 0) { Console.WriteLine(i); }

                unitOfWork.Create(new WeatherEntry()
                {
                    Timestamp = DateTime.Now.AddHours(-i),
                    Value = random.Next(-10, 40),
                    ValueType = Weatherstation.Data.Enums.WeatherValueType.Temperature
                });
            }

            for (int i = 0; i < _limit; i++)
            {
                if (i % 1000 == 0) { Console.WriteLine(i); }

                unitOfWork.Create(new WeatherEntry()
                {
                    Timestamp = DateTime.Now.AddHours(-i),
                    Value = random.Next(950, 1038),
                    ValueType = Weatherstation.Data.Enums.WeatherValueType.Airpressure
                });
            }

            for (int i = 0; i < _limit; i++)
            {
                if (i % 1000 == 0) { Console.WriteLine(i); }

                unitOfWork.Create(new WeatherEntry()
                {
                    Timestamp = DateTime.Now.AddHours(-i),
                    Value = random.Next(40, 80),
                    ValueType = Weatherstation.Data.Enums.WeatherValueType.Humidity
                });
            }
        }
	}
}