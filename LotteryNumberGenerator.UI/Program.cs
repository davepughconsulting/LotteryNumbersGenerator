using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LottoNumberGenerator.BusinessLogic;

[assembly: InternalsVisibleTo("LottoNumberGenerator.UI.UnitTests")]

namespace LottoNumberGenerator.UI
{
    /// <summary>
    /// Program class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The entry point of the application
        /// </summary>
        static void Main()
        {
            // Configure simple configuration
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            // Setup Inversion of Control/ DI
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ILotteryNumberGenerator, LotteryNumberGenerator>()
                .BuildServiceProvider();

            // Set default required lottery numbers incase config is not present
            int requiredNumberOfLotteryNumbers = 6;
            // Allow override of number of required lottery numbers so that a rebuild/retest of application is not required
            if (configuration["requiredNumberOfLotteryNumbers"] != null) 
            {
                int.TryParse(configuration["requiredNumberOfLotteryNumbers"], out requiredNumberOfLotteryNumbers);
            }

            try
            {
                // The below could be called also with no param to get default functionaility of 6 numbers also
                GeneratedLotteryNumbersResult generatedLotteryNumbers = serviceProvider.GetService<ILotteryNumberGenerator>().GenerateLotteryNumbers(requiredNumberOfLotteryNumbers);
                if (generatedLotteryNumbers.IsSuccess)
                {
                    foreach (var lotteryNumber in generatedLotteryNumbers.LotteryNumbers)
                    {
                        TextColour textColour = lotteryNumber.Value;
                        ConsoleColor consoleColour = Converters.ConvertTextColourToConsoleColour(textColour);
                        Console.ForegroundColor = consoleColour;
                        Console.WriteLine(lotteryNumber.Key);
                    }

                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    WriteErrorMessage(generatedLotteryNumbers.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                WriteErrorMessage(ex.Message);
            }

            Console.WriteLine("Press <Return> to continue");
            Console.ReadLine();
        }

        /// <summary>
        /// Writes the required error message to the console
        /// </summary>
        /// <param name="errorMessage">The required error message</param>
        private static void WriteErrorMessage(string errorMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("An error has occurred while generating lottery numbers");
            Console.WriteLine($"Error: {errorMessage ?? "No error message found"}");
        }
    }
}
