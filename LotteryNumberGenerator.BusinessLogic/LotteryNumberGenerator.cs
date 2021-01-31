using System;
using System.Linq;

namespace LottoNumberGenerator.BusinessLogic
{
    /// <summary>
    /// Lottery Number Generator class
    /// </summary>
    public class LotteryNumberGenerator : ILotteryNumberGenerator
    {
        /// <summary>
        /// The required start number
        /// </summary>
        private readonly int LotteryStartNumber;

        /// <summary>
        /// The required end number i.e. 49
        /// </summary>
        private readonly int LotteryEndNumber;

        public LotteryNumberGenerator()
        {
            // Initiate start and end numbers
            LotteryStartNumber = (int)TextColour.Unknown + 1;
            LotteryEndNumber = Enum.GetValues(typeof(TextColour)).Cast<int>().Max();
        }

        /// <summary>
        /// Generates a sequence of unique (in the sequence) lottery numbers with an associated required colour
        /// </summary>
        /// <remarks>Default implementation is to return 6 lottery numbers</remarks>
        /// <returns></returns>
        public GeneratedLotteryNumbersResult GenerateLotteryNumbers()
        {
            return GenerateLotteryNumbers(6);
        }

        /// <summary>
        /// Generates a sequence of the required count of numbers, unique (in the sequence) lottery numbers with an associated required colour
        /// </summary>
        /// <param name="numbersToCreate">The number of lottery numbers to create</param>
        /// <returns>A <see cref="GeneratedLotteryNumbersResult"/></returns>
        public GeneratedLotteryNumbersResult GenerateLotteryNumbers(int numbersToCreate)
        {
            GeneratedLotteryNumbersResult lotteryNumbersResult = new GeneratedLotteryNumbersResult(numbersToCreate);
            Random rand = new Random();

            // Loop while we haven't built up the required number of lottery numbers yet
            while (!lotteryNumbersResult.HasRequiredNumbersCount())
            {
                int randomNumber = rand.Next(LotteryStartNumber, LotteryEndNumber);
                TextColour requiredTextColour = DetermineTextColour(randomNumber);
                if (requiredTextColour != TextColour.Unknown)
                {
                    // Try adding the number, if the number is not unique the loop will continue and generate the next number to try
                    lotteryNumbersResult.TrySaveLotteryNumber(randomNumber, requiredTextColour);
                }
                else
                {
                    // This failure scenario is not testable as when the TextColour enum is correct & StartNUmber and EndNUmber are in synch with it
                    // it will never return back TextColour.Unknown. This untestability is somewhat mitigated by the fact that the testing of the testing of the 
                    // DetermineTextColour method with boundary testing to ensure it only ever returns back valid Colours
                    lotteryNumbersResult.IsSuccess = false;
                    lotteryNumbersResult.ErrorMessage = $"Could not determine required colour for number {randomNumber}";
                }
            }

            // The following is belt and braces approach to ensure we have valid lottery numbers before returning
            // again a failure in this scenario is impossible to unit test as there is code above that ensures the count is correct and that the
            // numbers are unique. This is mitigated by the fact that the AreLotterNumbersValid is tested seperately
            if (!lotteryNumbersResult.AreLotteryNumbersValid())
            {
                lotteryNumbersResult.IsSuccess = false;
                lotteryNumbersResult.ErrorMessage = $"Numbers generated were not each unique, values generated were {string.Join(",", lotteryNumbersResult.LotteryNumbers.Keys)}";
            }

            lotteryNumbersResult.IsSuccess = true;

            return lotteryNumbersResult;
        }

        /// <summary>
        /// Determines the required text colour from the number passed in
        /// </summary>
        /// <param name="number">The number to generate the colour for</param>
        /// <returns>A <see cref="TextColour"/></returns>
        private static TextColour DetermineTextColour(int number)
        {
            int startNumber = 0;
            int endNumber;
            // Loop through each colour in the TextColour enum
            foreach (TextColour colour in Enum.GetValues(typeof(TextColour)))
            {
                // Each colour has a start number associated with it, the previous colour + 1
                // And an end number - the value for that colour in the enum - so for e.g. Grey = 1-9, Blue = 10-19 and so on
                endNumber = (int)colour;
                if (number >= startNumber && number <= endNumber)
                {
                    // If the number is within that range that is the colour we require
                    return colour;
                }

                // Increment the start number to be the value of the current enum value plus 1 to initiate it for the next run through the loop for the next colour
                startNumber = (int)colour + 1;
            }

            return TextColour.Unknown;
        }
    }
}