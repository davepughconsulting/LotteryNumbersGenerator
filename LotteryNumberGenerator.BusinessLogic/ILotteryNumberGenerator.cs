namespace LottoNumberGenerator.BusinessLogic
{
    public interface ILotteryNumberGenerator
    {
        /// <summary>
        /// Generates the lottery numbers using the default functionality
        /// </summary>
        /// <returns>A <see cref="GeneratedLotteryNumbersResult"/></returns>
        GeneratedLotteryNumbersResult GenerateLotteryNumbers();

        /// <summary>
        /// Generates the lottery numbers using the numbersToCreate param as the count of numbers to create
        /// </summary>
        /// <param name="numbersToCreate">The count of numbers to create</param>
        /// <returns>A <see cref="GeneratedLotteryNumbersResult"/></returns>
        GeneratedLotteryNumbersResult GenerateLotteryNumbers(int numbersToCreate);
    }
}
