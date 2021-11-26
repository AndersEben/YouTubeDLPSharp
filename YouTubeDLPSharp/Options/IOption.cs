﻿
namespace YouTubeDLPSharp.Options
{
    /// <summary>
    /// Interface for one youtube-dlp option.
    /// </summary>
    interface IOption
    {
        /// <summary>
        /// The default string representation of the option flag.
        /// </summary>
        string DefaultOptionString { get; }
        /// <summary>
        /// An array of all possible string representations of the option flag.
        /// </summary>
        string[] OptionStrings { get; }
        /// <summary>
        /// True if the option flag is set; false otherwise.
        /// </summary>
        bool IsSet { get; }
        /// <summary>
        /// Sets the option value from a given string representation.
        /// </summary>
        /// <param name="s">The string (including the option flag).</param>
        void SetFromString(string s);
    }
}
