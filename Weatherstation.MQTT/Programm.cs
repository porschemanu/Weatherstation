using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Internal;
using MQTTnet.Server;
using Weatherstation.Data.Enums;
using Weatherstation.Data.Models;
using Weatherstation.Data.Repositories;

namespace Weatherstation.MQTT
{

    class Program
    {
        static WeatherEntryRepo _repo = new();
        public static async Task Main(string[] args)
        {
            Console.WriteLine("BBS Wittlich Wetterstation MQTTBroker");
			Console.WriteLine("Anwendung gestartet");
			await Run_Server_With_Logging();
        }

        public static async Task Run_Server_With_Logging()
        {
            
            var mqttFactory = new MqttFactory();

            var mqttServerOptions = new MqttServerOptionsBuilder().WithDefaultEndpoint().Build();

            using (var mqttServer = mqttFactory.CreateMqttServer(mqttServerOptions))
            {
                mqttServer.InterceptingPublishAsync += args =>
                {

                    var PayloadSegment = args.ApplicationMessage.PayloadSegment;
                    if (PayloadSegment != null)
                    {
                        string value = Encoding.ASCII.GetString(PayloadSegment.Array, PayloadSegment.Offset, PayloadSegment.Count);
                        double result;

                        // Versuch, den String in einen Double zu konvertieren
                        if (double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
                        {
                            switch (args.ApplicationMessage.Topic)
                            {
                                case "temperature":
                                    _repo.Create(new WeatherEntry()
                                    {
                                        Timestamp = DateTime.UtcNow,
                                        Value = result,
                                        ValueType = WeatherValueType.Temperature
                                    });
									Console.WriteLine($"Neuer Eintrag:Temperatur, Wert: {result}");
									break;

                                case "humidity":
                                    _repo.Create(new WeatherEntry()
                                    {
                                        Timestamp = DateTime.UtcNow,
                                        Value = result,
                                        ValueType = WeatherValueType.Humidity
                                    });
									Console.WriteLine($"Neuer Eintrag:Luftfeuchtigkeit, Wert: {result}");
									break;

                                case "airpressure":
                                    _repo.Create(new WeatherEntry()
                                    {
                                        Timestamp = DateTime.UtcNow,
                                        Value = result,
                                        ValueType = WeatherValueType.Airpressure
                                    });
									Console.WriteLine($"Neuer Eintrag:Luftdruck, Wert: {result}");
									break;

                                case "co2":
                                    _repo.Create(new WeatherEntry()
                                    {
                                        Timestamp = DateTime.UtcNow,
                                        Value = result,
                                        ValueType = WeatherValueType.CO2
                                    });
									Console.WriteLine($"Neuer Eintrag:CO2, Wert: {result}");
									break;

                                default:
                                    Console.WriteLine($"{args.ApplicationMessage.Topic}: Value Could not be identified. Maybe wrong Topic?");
                                    break;
                            }
                        }
                    }
                    return CompletedTask.Instance;
                };

                await mqttServer.StartAsync();

                Console.WriteLine("Press Enter to exit.");
                Console.ReadLine();

                // Stop and dispose the MQTT server if it is no longer needed!
                await mqttServer.StopAsync();
            }
        }

    }
}
