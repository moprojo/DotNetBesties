using System;
using System.Drawing;

namespace DotNetBesties.Helpers.Extensions;

/// <summary>
/// Extension methods for <see cref="Color"/> operations.
/// </summary>
public static class ColorExtensions
{
    #region Conversion

    /// <summary>
    /// Extracts ARGB components from the color.
    /// </summary>
    /// <param name="color">The color to extract components from.</param>
    /// <returns>A tuple containing alpha, red, green, and blue values.</returns>
    public static (int a, int r, int g, int b) ToARGB(this Color color)
        => Format.ColorHelper.ColorToARGB(color);

    /// <summary>
    /// Extracts RGB components from the color.
    /// </summary>
    /// <param name="color">The color to extract components from.</param>
    /// <returns>A tuple containing red, green, and blue values.</returns>
    public static (int r, int g, int b) ToRGB(this Color color)
        => Format.ColorHelper.ColorToRGB(color);

    /// <summary>
    /// Converts the color to a hex string in #RRGGBB format.
    /// </summary>
    /// <param name="color">The color to convert.</param>
    /// <returns>A hex string representation of the color.</returns>
    public static string ToHex(this Color color)
        => Format.StringHelper.ColorToHexString(color);

    /// <summary>
    /// Converts the color to a hex string in #AARRGGBB format (including alpha).
    /// </summary>
    /// <param name="color">The color to convert.</param>
    /// <returns>A hex string representation of the color with alpha.</returns>
    public static string ToHexWithAlpha(this Color color)
        => Format.ColorHelper.ToHexWithAlpha(color);

    /// <summary>
    /// Converts the color to an RGB string (e.g., "rgb(255, 0, 0)").
    /// </summary>
    /// <param name="color">The color to convert.</param>
    /// <returns>An RGB string representation of the color.</returns>
    public static string ToRgbString(this Color color)
        => Format.StringHelper.ColorToRgbString(color);

    /// <summary>
    /// Converts the color to an ARGB string (e.g., "rgba(255, 0, 0, 255)").
    /// </summary>
    /// <param name="color">The color to convert.</param>
    /// <returns>An ARGB string representation of the color.</returns>
    public static string ToArgbString(this Color color)
        => Format.StringHelper.ColorToARgbString(color);

    #endregion

    #region Manipulation

    /// <summary>
    /// Inverts the color by subtracting each RGB component from 255.
    /// </summary>
    /// <param name="color">The color to invert.</param>
    /// <returns>The inverted color with the same alpha value.</returns>
    public static Color Invert(this Color color)
        => Format.ColorHelper.Invert(color);

    /// <summary>
    /// Creates a new color with the specified alpha value.
    /// </summary>
    /// <param name="color">The source color.</param>
    /// <param name="alpha">The new alpha value (0-255).</param>
    /// <returns>A new color with the specified alpha value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when alpha is outside the valid range (0-255).</exception>
    public static Color WithAlpha(this Color color, int alpha)
        => Format.ColorHelper.WithAlpha(color, alpha);

    /// <summary>
    /// Creates a lighter version of the color by increasing brightness.
    /// </summary>
    /// <param name="color">The source color.</param>
    /// <param name="factor">The lightening factor (0.0 to 1.0). Default is 0.2 (20% lighter).</param>
    /// <returns>A lighter version of the color.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when factor is outside the valid range (0.0-1.0).</exception>
    public static Color Lighten(this Color color, double factor = 0.2)
        => Format.ColorHelper.Lighten(color, factor);

    /// <summary>
    /// Creates a darker version of the color by decreasing brightness.
    /// </summary>
    /// <param name="color">The source color.</param>
    /// <param name="factor">The darkening factor (0.0 to 1.0). Default is 0.2 (20% darker).</param>
    /// <returns>A darker version of the color.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when factor is outside the valid range (0.0-1.0).</exception>
    public static Color Darken(this Color color, double factor = 0.2)
        => Format.ColorHelper.Darken(color, factor);

    #endregion

    #region Analysis

    /// <summary>
    /// Calculates the relative luminance of the color.
    /// </summary>
    /// <param name="color">The color to calculate luminance for.</param>
    /// <returns>A value between 0 (darkest) and 1 (lightest).</returns>
    public static double GetLuminance(this Color color)
        => Format.ColorHelper.GetLuminance(color);

    /// <summary>
    /// Determines whether the color is considered "dark" based on luminance.
    /// </summary>
    /// <param name="color">The color to check.</param>
    /// <returns><c>true</c> if the color is dark; otherwise, <c>false</c>.</returns>
    public static bool IsDark(this Color color)
        => Format.ColorHelper.IsDark(color);

    /// <summary>
    /// Determines whether the color is considered "light" based on luminance.
    /// </summary>
    /// <param name="color">The color to check.</param>
    /// <returns><c>true</c> if the color is light; otherwise, <c>false</c>.</returns>
    public static bool IsLight(this Color color)
        => Format.ColorHelper.IsLight(color);

    /// <summary>
    /// Determines the best contrasting color (black or white) for text on this background color.
    /// </summary>
    /// <param name="backgroundColor">The background color.</param>
    /// <returns><see cref="Color.Black"/> for light backgrounds, <see cref="Color.White"/> for dark backgrounds.</returns>
    public static Color GetContrastingTextColor(this Color backgroundColor)
        => Format.ColorHelper.GetContrastingTextColor(backgroundColor);

    #endregion

    #region Blending

    /// <summary>
    /// Blends two colors together using linear interpolation.
    /// </summary>
    /// <param name="color1">The first color.</param>
    /// <param name="color2">The second color.</param>
    /// <param name="ratio">The blend ratio (0.0 = all color1, 1.0 = all color2). Default is 0.5 (equal blend).</param>
    /// <returns>The blended color.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when ratio is outside the valid range (0.0-1.0).</exception>
    public static Color Blend(this Color color1, Color color2, double ratio = 0.5)
        => Format.ColorHelper.Blend(color1, color2, ratio);

    #endregion
}
