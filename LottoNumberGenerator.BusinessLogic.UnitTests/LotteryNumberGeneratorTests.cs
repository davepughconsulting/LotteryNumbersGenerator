using LottoNumberGenerator.BusinessLogic;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace LottoNumberGenerator.BusinessLogic.UnitTests
{
    /// <summary>
    /// LotteryNumberGeneratorTests
    /// </summary>
    public class LotteryNumberGeneratorTests
    {
        /// <summary>
        /// The object that is under test
        /// </summary>
        private readonly ILotteryNumberGenerator objectUnderTest = new LotteryNumberGenerator();

        // Test names take the form of UnitOfCodeUnderTest_TestScenario_ExpectedResult

        /// <summary>
        /// Ensures the correct number of lottery numbers are returned, that they are unique, success is true and ErrorMessage is null
        /// </summary>
        /// <param name="countOfNumbers">The count of numbers to create</param>
        [Theory]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(10)] // Lets check it will still work for potential future changes in requirements
        
        public void GenerateLotteryNumbers_DifferentRequiredCountOfNumbers_CorrectCountOfNumbersReturned(int countOfNumbers)
        {
            // Arrange

            // Act
            GeneratedLotteryNumbersResult testResult = objectUnderTest.GenerateLotteryNumbers(countOfNumbers);

            // Assert
            Assert.True(testResult.IsSuccess); 
            Assert.Null(testResult.ErrorMessage); 
            Assert.Equal(countOfNumbers, testResult.LotteryNumbers.Count); // Ensure correct count of number
            Assert.Equal(countOfNumbers, testResult.LotteryNumbers.Keys.Distinct().Count()); // Ensure correct count of unique number - i.e. all should be unique
        }

        /// <summary>
        /// Ensures the default implementation returns the correct count of lottery numbers (6), that they are unique, success is true and ErrorMessage is null
        /// </summary>
        [Fact]
        // Test names take the form of MethodNameUnderTest_TestScenario_ExpectedResult
        public void GenerateLotteryNumbers_RequiredCountOfNumbersNotSupplied_CorrectCountOfNumbersReturned()
        {
            // Arrange

            // Act
            GeneratedLotteryNumbersResult testResult = objectUnderTest.GenerateLotteryNumbers();

            // Assert
            Assert.True(testResult.IsSuccess);
            Assert.Null(testResult.ErrorMessage);
            Assert.Equal(6, testResult.LotteryNumbers.Count); // Ensure correct count of number
            Assert.Equal(6, testResult.LotteryNumbers.Keys.Distinct().Count()); // Ensure correct count of unique number - i.e. all should be unique
        }

        /*
        Note: The following tests are tests that test private methds, which is not always advised, however the testing below enables the low level
        functionality which is key to achieving the required functionaility without impacting the design of the system and making types more
        accessible than they need to be. Therefore I believe this is a worthwhile trade off that adds real test value
        */

        /// <summary>
        /// Tests the private method DetermineTextColour to ensure that under all boundary scenarios it returns the correct colour
        /// and therefore inadvertently that it does not return the Unknown type
        /// </summary>
        /// <param name="lotteryNumber">The lottery number</param>
        /// <param name="expectedTextColour">The colour that is expected to be returned</param>
        [Theory]
        [InlineData(1, TextColour.Grey)]
        [InlineData(9, TextColour.Grey)]
        [InlineData(10, TextColour.Blue)]
        [InlineData(19, TextColour.Blue)]
        [InlineData(20, TextColour.Pink)]
        [InlineData(29, TextColour.Pink)]
        [InlineData(30, TextColour.Green)]
        [InlineData(39, TextColour.Green)]
        [InlineData(40, TextColour.Yellow)]
        [InlineData(49, TextColour.Yellow)]

        public void DetermineTextColour_BoundaryTests_ExpectCorrectColourReturned(int lotteryNumber, TextColour expectedTextColour)
        {
            // Arrange
            MethodInfo determineTextColourMethodInfo = this.objectUnderTest.GetType().GetMethod("DetermineTextColour", BindingFlags.Static | BindingFlags.NonPublic);
            if (determineTextColourMethodInfo == null)
            {
                throw new Exception($"Unable to find method DetermineTextColour on {objectUnderTest.GetType().Name}");
            }

            // Act
            TextColour testResult = (TextColour)determineTextColourMethodInfo.Invoke(objectUnderTest, new object[] { lotteryNumber });

            // Assert
            Assert.Equal(expectedTextColour, testResult);
        }

        /// <summary>
        /// Tests that the EndNumber of LOtteryNumberGenerator and the TextColour enum are kept in sync - any future chance in either will
        /// cause this test to fail and further illustrate the required linkeage between the two
        /// </summary>
        [Fact]
        public void EndNumber_IsSynchedWithMaxTextColourEnum_ExpectAreEqual()
        {
            // Arrange
            FieldInfo endNumberFieldInfo = this.objectUnderTest.GetType().GetField("LotteryEndNumber", BindingFlags.NonPublic | BindingFlags.Instance);
            if (endNumberFieldInfo == null)
            {
                throw new Exception($"Unable to find field LotteryEndNumber on {objectUnderTest.GetType().Name}");
            }

            int maxEnumValue = Enum.GetValues(typeof(TextColour)).Cast<int>().Max();

            // Act

            // Assert
            Assert.Equal(maxEnumValue, endNumberFieldInfo.GetValue(this.objectUnderTest));
        }

        /// <summary>
        /// Tests that the StartNumber of LotteryNumberGenerator and the TextColour.Unknown enum are kept in sync - any future chance in either will
        /// cause this test to fail and further illustrate the required linkeage between the two, which is also noted in code comments
        /// </summary>
        [Fact]
        public void StartNumber_IsSynchedWithTextColourEnum_ExpectAreEqual()
        {
            // Arrange
            FieldInfo endNumberFieldInfo = this.objectUnderTest.GetType().GetField("LotteryStartNumber", BindingFlags.NonPublic | BindingFlags.Instance);
            if (endNumberFieldInfo == null)
            {
                throw new Exception($"Unable to find field LotteryStartNumber on {objectUnderTest.GetType().Name}");
            }

            int initialEnumValue = Enum.GetValues(typeof(TextColour)).Cast<int>().First();

            // Act

            // Assert
            // Here we add 1 as that is where the intial bounds will start from i.e. 1-9
            Assert.Equal(initialEnumValue + 1, endNumberFieldInfo.GetValue(this.objectUnderTest));
        }
    }
}
