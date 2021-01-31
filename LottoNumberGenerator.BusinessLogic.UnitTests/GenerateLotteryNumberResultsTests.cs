using System.Linq;
using LottoNumberGenerator.BusinessLogic;
using Xunit;

namespace LottoNumberGenerator.BusinessLogic.UnitTests
{
    public class GenerateLotteryNumberResultsTests
    {
        // Test names take the form of UnitOfCodeUnderTest_TestScenario_ExpectedResult

        /// <summary>
        /// Ensure that when the same number is attempted to be saved multiple times the second number is not saved
        /// </summary>
        [Fact]
        public void TrySaveLotteryNumbers_SaveSameNumerTwice_CorrectCountOfNumbersReturned()
        {
            // Arrange
            GeneratedLotteryNumbersResult testResult = new GeneratedLotteryNumbersResult(6);

            // Act

            // Assert
            Assert.True(testResult.TrySaveLotteryNumber(1, TextColour.Grey));
            Assert.False(testResult.TrySaveLotteryNumber(1, TextColour.Grey));
            Assert.True(testResult.TrySaveLotteryNumber(2, TextColour.Grey));
            Assert.Equal(2, testResult.LotteryNumbers.Count); // Ensure correct count of number
            Assert.Equal(2, testResult.LotteryNumbers.Keys.Distinct().Count()); // Ensure correct count of unique number - i.e. all should be unique
        }

        /// <summary>
        /// Ensures the check that determines whether the required count of numbers is stored
        /// </summary>
        /// <param name="countOfNumbers">The count of numbers required</param>
        [Theory]
        [InlineData(6)]
        [InlineData(7)]
        public void HasRequiredNumbersCount_SaveSixNUmbers_ReturnTrue(int countOfNumbers)
        {
            // Arrange
            GeneratedLotteryNumbersResult testResult = new GeneratedLotteryNumbersResult(countOfNumbers);

            // Act
            for (int i = 1; i <= countOfNumbers; i++)
            {
                testResult.TrySaveLotteryNumber(i, TextColour.Grey);
            }

            // Assert
            Assert.True(testResult.HasRequiredNumbersCount()); // Ensure HasRequiredNumbersCount returns true
        }

        [Theory]
        [InlineData(2, true)]
        [InlineData(1, false)]
        public void AreLotteryNumbersValid(int secondNumber, bool expectedResult)
        {
            // Arrange
            GeneratedLotteryNumbersResult testResult = new GeneratedLotteryNumbersResult(6);

            // Act
            testResult.TrySaveLotteryNumber(1, TextColour.Grey);
            testResult.TrySaveLotteryNumber(secondNumber, TextColour.Grey);
            testResult.TrySaveLotteryNumber(3, TextColour.Grey);
            testResult.TrySaveLotteryNumber(4, TextColour.Grey);
            testResult.TrySaveLotteryNumber(5, TextColour.Grey);
            testResult.TrySaveLotteryNumber(6, TextColour.Grey);

            // Assert
            Assert.Equal(expectedResult, testResult.AreLotteryNumbersValid());
        }
    }
}
