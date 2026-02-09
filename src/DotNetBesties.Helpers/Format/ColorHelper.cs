using System;
using System.Drawing;

namespace DotNetBesties.Helpers.Format;

/// <summary>
/// Provides helper methods for color conversions between different formats (RGB, ARGB, Hex).
/// </summary>
public static class ColorHelper
{
    /// <summary>
    /// Creates a <see cref="Color"/> from RGB values.
    /// </summary>
    /// <param name="r">Red component (0-255).</param>
    /// <param name="g">Green component (0-255).</param>
    /// <param name="b">Blue component (0-255).</param>
    /// <returns>A <see cref="Color"/> instance.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when any color component is outside the valid range (0-255).</exception>
    public static Color RgbToColor(int r, int g, int b)
    {
        ValidateColorComponent(r, nameof(r));
        ValidateColorComponent(g, nameof(g));
        ValidateColorComponent(b, nameof(b));
        return Color.FromArgb(r, g, b);
    }

    /// <summary>
    /// Creates a <see cref="Color"/> from ARGB values.
    /// </summary>
    /// <param name="a">Alpha component (0-255).</param>
    /// <param name="r">Red component (0-255).</param>
    /// <param name="g">Green component (0-255).</param>
    /// <param name="b">Blue component (0-255).</param>
    /// <returns>A <see cref="Color"/> instance.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when any color component is outside the valid range (0-255).</exception>
    public static Color ARGBToColor(int a, int r, int g, int b)
    {
        ValidateColorComponent(a, nameof(a));
        ValidateColorComponent(r, nameof(r));
        ValidateColorComponent(g, nameof(g));
        ValidateColorComponent(b, nameof(b));
        return Color.FromArgb(a, r, g, b);
    }

    /// <summary>
    /// Converts a hex color string to a <see cref="Color"/>.
    /// Supports formats: #RRGGBB, RRGGBB, #RGB, RGB.
    /// </summary>
    /// <param name="hex">The hex color string.</param>
    /// <returns>A <see cref="Color"/> instance.</returns>
    /// <exception cref="ArgumentNullException">Thrown when hex is null.</exception>
    /// <exception cref="ArgumentException">Thrown when hex format is invalid.</exception>
    public static Color HexToColor(string hex)
    {
        var (r, g, b) = HexToRGB(hex);
        return Color.FromArgb(r, g, b);
    }

    /// <summary>
    /// Converts a hex color string to RGB values.
    /// Supports formats: #RRGGBB, RRGGBB, #RGB, RGB.
    /// </summary>
    /// <param name="hex">The hex color string.</param>
    /// <returns>A tuple containing red, green, and blue values (0-255).</returns>
    /// <exception cref="ArgumentNullException">Thrown when hex is null.</exception>
    /// <exception cref="ArgumentException">Thrown when hex format is invalid.</exception>
    public static (int r, int g, int b) HexToRGB(string hex)
    {
        ArgumentNullException.ThrowIfNull(hex);

        var cleanHex = hex.TrimStart('#');

        // Support 3-character shorthand (e.g., #F00 -> #FF0000)
        if (cleanHex.Length == 3)
        {
            cleanHex = $"{cleanHex[0]}{cleanHex[0]}{cleanHex[1]}{cleanHex[1]}{cleanHex[2]}{cleanHex[2]}";
        }

        if (cleanHex.Length != 6)
            throw new ArgumentException("Invalid hex color format. Expected formats: #RRGGBB, RRGGBB, #RGB, or RGB.", nameof(hex));

        try
        {
            var r = Convert.ToInt32(cleanHex.Substring(0, 2), 16);
            var g = Convert.ToInt32(cleanHex.Substring(2, 2), 16);
            var b = Convert.ToInt32(cleanHex.Substring(4, 2), 16);
            return (r, g, b);
        }
        catch (FormatException ex)
        {
            throw new ArgumentException("Invalid hex color format. Contains non-hexadecimal characters.", nameof(hex), ex);
        }
    }

    /// <summary>
    /// Extracts ARGB components from a <see cref="Color"/>.
    /// </summary>
    /// <param name="color">The color to extract components from.</param>
    /// <returns>A tuple containing alpha, red, green, and blue values.</returns>
    public static (int a, int r, int g, int b) ColorToARGB(Color color)
    {
        return (color.A, color.R, color.G, color.B);
    }

    /// <summary>
    /// Extracts RGB components from a <see cref="Color"/>.
    /// </summary>
    /// <param name="color">The color to extract components from.</param>
    /// <returns>A tuple containing red, green, and blue values.</returns>
    public static (int r, int g, int b) ColorToRGB(Color color)
    {
        return (color.R, color.G, color.B);
    }

    /// <summary>
    /// Inverts a color by subtracting each RGB component from 255.
    /// </summary>
    /// <param name="color">The color to invert.</param>
    /// <returns>The inverted color with the same alpha value.</returns>
    public static Color Invert(Color color)
    {
        return Color.FromArgb(color.A, 255 - color.R, 255 - color.G, 255 - color.B);
    }

    /// <summary>
    /// Calculates the relative luminance of a color.
    /// </summary>
    /// <param name="color">The color to calculate luminance for.</param>
    /// <returns>A value between 0 (darkest) and 1 (lightest).</returns>
    public static double GetLuminance(Color color)
    {
        // Using the relative luminance formula from WCAG 2.0
        var r = color.R / 255.0;
        var g = color.G / 255.0;
        var b = color.B / 255.0;

        r = r <= 0.03928 ? r / 12.92 : Math.Pow((r + 0.055) / 1.055, 2.4);
        g = g <= 0.03928 ? g / 12.92 : Math.Pow((g + 0.055) / 1.055, 2.4);
        b = b <= 0.03928 ? b / 12.92 : Math.Pow((b + 0.055) / 1.055, 2.4);

        return 0.2126 * r + 0.7152 * g + 0.0722 * b;
    }

    /// <summary>
    /// Determines if a color is considered "dark" based on luminance.
    /// </summary>
    /// <param name="color">The color to check.</param>
    /// <returns><c>true</c> if the color is dark; otherwise, <c>false</c>.</returns>
    public static bool IsDark(Color color)
    {
        return GetLuminance(color) < 0.5;
    }

    /// <summary>
    /// Determines if a color is considered "light" based on luminance.
    /// </summary>
    /// <param name="color">The color to check.</param>
    /// <returns><c>true</c> if the color is light; otherwise, <c>false</c>.</returns>
    public static bool IsLight(Color color)
    {
        return GetLuminance(color) >= 0.5;
    }

    /// <summary>
    /// Creates a new color with the specified alpha value.
    /// </summary>
    /// <param name="color">The source color.</param>
    /// <param name="alpha">The new alpha value (0-255).</param>
    /// <returns>A new color with the specified alpha value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when alpha is outside the valid range (0-255).</exception>
    public static Color WithAlpha(Color color, int alpha)
    {
        if (alpha < 0 || alpha > 255)
            throw new ArgumentOutOfRangeException(nameof(alpha), alpha, "Alpha must be between 0 and 255.");

        return Color.FromArgb(alpha, color.R, color.G, color.B);
    }

    /// <summary>
    /// Creates a lighter version of the color by increasing brightness.
    /// </summary>
    /// <param name="color">The source color.</param>
    /// <param name="factor">The lightening factor (0.0 to 1.0). Default is 0.2 (20% lighter).</param>
    /// <returns>A lighter version of the color.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when factor is outside the valid range (0.0-1.0).</exception>
    public static Color Lighten(Color color, double factor = 0.2)
    {
        if (factor < 0.0 || factor > 1.0)
            throw new ArgumentOutOfRangeException(nameof(factor), factor, "Factor must be between 0.0 and 1.0.");

        var r = Math.Min(255, (int)(color.R + (255 - color.R) * factor));
        var g = Math.Min(255, (int)(color.G + (255 - color.G) * factor));
        var b = Math.Min(255, (int)(color.B + (255 - color.B) * factor));

        return Color.FromArgb(color.A, r, g, b);
    }

    /// <summary>
    /// Creates a darker version of the color by decreasing brightness.
    /// </summary>
    /// <param name="color">The source color.</param>
    /// <param name="factor">The darkening factor (0.0 to 1.0). Default is 0.2 (20% darker).</param>
    /// <returns>A darker version of the color.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when factor is outside the valid range (0.0-1.0).</exception>
    public static Color Darken(Color color, double factor = 0.2)
    {
        if (factor < 0.0 || factor > 1.0)
            throw new ArgumentOutOfRangeException(nameof(factor), factor, "Factor must be between 0.0 and 1.0.");

        var r = Math.Max(0, (int)(color.R * (1.0 - factor)));
        var g = Math.Max(0, (int)(color.G * (1.0 - factor)));
        var b = Math.Max(0, (int)(color.B * (1.0 - factor)));

        return Color.FromArgb(color.A, r, g, b);
    }

    /// <summary>
    /// Blends two colors together using linear interpolation.
    /// </summary>
    /// <param name="color1">The first color.</param>
    /// <param name="color2">The second color.</param>
    /// <param name="ratio">The blend ratio (0.0 = all color1, 1.0 = all color2). Default is 0.5 (equal blend).</param>
    /// <returns>The blended color.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when ratio is outside the valid range (0.0-1.0).</exception>
    public static Color Blend(Color color1, Color color2, double ratio = 0.5)
    {
        if (ratio < 0.0 || ratio > 1.0)
            throw new ArgumentOutOfRangeException(nameof(ratio), ratio, "Ratio must be between 0.0 and 1.0.");

        var a = (int)(color1.A * (1 - ratio) + color2.A * ratio);
        var r = (int)(color1.R * (1 - ratio) + color2.R * ratio);
        var g = (int)(color1.G * (1 - ratio) + color2.G * ratio);
        var b = (int)(color1.B * (1 - ratio) + color2.B * ratio);

        return Color.FromArgb(a, r, g, b);
    }

    /// <summary>
    /// Determines the best contrasting color (black or white) for text on the given background color.
    /// </summary>
    /// <param name="backgroundColor">The background color.</param>
    /// <returns><see cref="Color.Black"/> for light backgrounds, <see cref="Color.White"/> for dark backgrounds.</returns>
    public static Color GetContrastingTextColor(Color backgroundColor)
        => IsLight(backgroundColor) ? Color.Black : Color.White;

    /// <summary>
    /// Converts the color to a hex string in #AARRGGBB format (including alpha).
    /// </summary>
    /// <param name="color">The color to convert.</param>
    /// <returns>A hex string representation of the color with alpha.</returns>
    public static string ToHexWithAlpha(Color color)
        => $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";

    /// <summary>
    /// Validates that a color component is within the valid range (0-255).
    /// </summary>
    private static void ValidateColorComponent(int value, string paramName)
    {
        if (value < 0 || value > 255)
            throw new ArgumentOutOfRangeException(paramName, value, "Color component must be between 0 and 255.");
    }
}