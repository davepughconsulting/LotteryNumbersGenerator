using System;
using Xunit;
using LottoNumberGenerator.BusinessLogic;

namespace LottoNumberGenerator.UI.UnitTests
{
    /// <summary>
    /// ColourConverterTests
    /// </summary>
    public class ColourConverterTests
    {
        /// <summary>
        /// Ensure the colour converter converts to the correct ConsoleColor
        /// </summary>
        /// <param name="textColour">The <see cref="TextColour"/> to convert</param>
        /// <param name="expectedColour">The expected <see cref="ConsoleColor"/></param>
        [Theory]
        [InlineData(TextColour.Blue, ConsoleColor.Blue)]
        [InlineData(TextColour.Green, ConsoleColor.Green)]
        [InlineData(TextColour.Grey, ConsoleColor.DarkGray)]
        [InlineData(TextColour.Pink, ConsoleColor.Magenta)]
        [InlineData(TextColour.Yellow, ConsoleColor.Yellow)]

        public void ConvertTextColourToConsoleColour_WithValidColour_ExpectCorrectColourReturned(TextColour textColour, ConsoleColor expectedColour)
        {
            // Arrange

            // Act

            // Assert
            Assert.Equal(expectedColour, Converters.ConvertTextColourToConsoleColour(textColour));
        }

        /// <summary>
        /// Ensures the ColourConverter throws an <see cref="InvalidCastException"/> when passed TextColour.Unknown 
        /// </summary>
        [Fact]
        public void ConvertTextColourToConsoleColour_WithInValidColour_ExpectCorrectColourReturned()
        {
            // Arrange

            // Act

            // Assert
            Assert.Throws<InvalidCastException>(() => Converters.ConvertTextColourToConsoleColour(TextColour.Unknown));
        }
    }
}
