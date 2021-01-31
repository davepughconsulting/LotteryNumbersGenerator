using System;
using LottoNumberGenerator.BusinessLogic;

namespace LottoNumberGenerator.UI
{
    /// <summary>
    /// Converters class - holds converters to convert between business logic and UI logic
    /// </summary>
    internal static class Converters
    {
        /// <summary>
        /// Converts from <see cref="TextColour"/> to <see cref="ConsoleColor"/> 
        /// </summary>
        /// <param name="textColour">The <see cref="TextColour"/> to convert</param>
        /// <returns>A <see cref="ConsoleColor"/></returns>
        internal static ConsoleColor ConvertTextColourToConsoleColour (TextColour textColour)
        {
            return textColour switch
            {
                TextColour.Grey => ConsoleColor.DarkGray,
                TextColour.Blue => ConsoleColor.Blue,
                TextColour.Pink => ConsoleColor.Magenta,
                TextColour.Green => ConsoleColor.Green,
                TextColour.Yellow => ConsoleColor.Yellow,
                _ => throw new InvalidCastException($"Unexpected {nameof(TextColour)} was passed to {nameof(ConvertTextColourToConsoleColour)} - value passed was {textColour}"),
            };
        }
    }
}
