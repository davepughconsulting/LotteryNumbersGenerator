using System.Collections.Generic;
using System.Linq;

namespace LottoNumberGenerator.BusinessLogic
{
    /// <summary>
    /// GeneratedLotteryNUmbersResult class
    /// </summary>
    public class GeneratedLotteryNumbersResult
    {
        /// <summary>
        /// Initializes a new instance of <see cref="GeneratedLotteryNumbersResult"/>
        /// </summary>
        /// <param name="requiredLotteryNumbersCount">The count of required lottery numbers</param>
        public GeneratedLotteryNumbersResult(int requiredLotteryNumbersCount)
        {
            LotteryNumbers = new SortedList<int, TextColour>();
            this.RequiredLotteryNumbersCount = requiredLotteryNumbersCount;
        }

        /// <summary>
        /// The list that holds the results
        /// </summary>
        public SortedList<int, TextColour> LotteryNumbers { get; private set; }

        /// <summary>
        /// Holds a value indicating success or failure
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// The error message related to a failure
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// The required count of lottery numbers required
        /// </summary>
        private int RequiredLotteryNumbersCount { get; set; }

        /// <summary>
        /// Attempts to save the lottery number to the results list
        /// </summary>
        /// <param name="number">The number to save</param>
        /// <param name="textColour">The colour associated with the number</param>
        /// <returns>True or false</returns>
        public bool TrySaveLotteryNumber(int number, TextColour textColour)
        {
            if (!this.IsNumberAlreadyStored(number))
            {
                this.LotteryNumbers.Add(number, textColour);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether the requird count of lottery numbers has been met
        /// </summary>
        /// <returns>True or false</returns>
        public bool HasRequiredNumbersCount()
        {
            return this.LotteryNumbers.Count == RequiredLotteryNumbersCount;
        }

        /// <summary>
        /// Determines whether the lottery numbers are valid
        /// </summary>
        /// <returns>True or false</returns>
        public bool AreLotteryNumbersValid()
        {
            // Check we have the correct count of numbers and that they are unique
            return RequiredLotteryNumbersCount == this.LotteryNumbers.Count && this.LotteryNumbers.Keys.Distinct().Count() == RequiredLotteryNumbersCount;
        }

        /// <summary>
        /// Determines whether the supplied number has already been stored for this instance
        /// </summary>
        /// <param name="number">The number to check</param>
        /// <returns>True or false</returns>
        private bool IsNumberAlreadyStored(int number)
        {
            return LotteryNumbers.ContainsKey(number);
        }
    }
}
