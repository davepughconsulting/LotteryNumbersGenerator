namespace LottoNumberGenerator.BusinessLogic
{
    /// <summary>
    /// Contains the possible values for text colour
    /// </summary>
    /// <remarks>This enum is linked to functionality within the LotteryNumberGenerator - any changes should be tested using the supplied unit tests
    /// </remarks>
    public enum TextColour
    {
        /// <summary>
        /// The Unknown TextColour
        /// </summary>
        Unknown = 0, // 0 is the default value for this element anyway, but have explicitly set it for future readability

        /// <summary>
        /// The Grey TetxColour
        /// </summary>
        Grey = 9, // Each subsequent type is the maximum value for its range, the range starts from the previous type and adds 1 i.e 1 -9, 10-19 etc
        
        /// <summary>
        /// The Blue TextColour
        /// </summary>
        Blue = 19,

        /// <summary>
        /// The Pink TextColour
        /// </summary>
        Pink = 29,

        /// <summary>
        /// The Green TextColour
        /// </summary>
        Green = 39,

        /// <summary>
        /// The Yellow TextColour
        /// </summary>
        Yellow = 49
    }
}
