// See https://aka.ms/new-console-template for more information
using Weatherstation.Data.Models;
using Weatherstation.Data.UnitOfWork;

internal class Program
{
	private static void Main(string[] args)
	{
		UnitOfWork unitOfWork = new UnitOfWork();
		Console.WriteLine("Hello, World!");

		Random random = new Random();

		for (int i = 0; i < 10000; i++)
		{
			if (i % 1000 == 0) { Console.WriteLine(i); }

			unitOfWork.Create(new WeatherEntry()
			{
				Timestamp = DateTime.Now.AddHours(-i),
				Value = random.Next(-10, 40),
				ValueType = Weatherstation.Data.Enums.WeatherValueType.Temperature
			});

		}

		for (int i = 0; i < 10000; i++)
		{
			if (i % 1000 == 0) { Console.WriteLine(i); }

			unitOfWork.Create(new WeatherEntry()
			{
				Timestamp = DateTime.Now.AddHours(-i),
				Value = random.Next(950, 1038),
				ValueType = Weatherstation.Data.Enums.WeatherValueType.Airpressure
			});

		}

		for (int i = 0; i < 10000; i++)
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