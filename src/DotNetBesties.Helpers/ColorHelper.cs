using System;
using System.Drawing;

namespace DotNetBesties.Helpers
{
    public static class ColorHelper
    {
        public static Color RgbToColor(int r, int g, int b)
        {
            return Color.FromArgb(r, g, b);
        }

        public static Color ARGBToColor(int a, int r, int g, int b)
        {
            return Color.FromArgb(a, r, g, b);
        }

        public static Color HexToColor(string hex)
        {
            var (r, g, b) = HexToRGB(hex);
            return Color.FromArgb(r, g, b);
        }

        public static (int r, int g, int b) HexToRGB(string hex)
        {
            if (hex.StartsWith("#"))
                hex = hex.Substring(1);

            if (hex.Length != 6)
                throw new ArgumentException("Invalid hex color format.");

            var r = Convert.ToInt32(hex.Substring(0, 2), 16);
            var g = Convert.ToInt32(hex.Substring(2, 2), 16);
            var b = Convert.ToInt32(hex.Substring(4, 2), 16);

            return (r, g, b);
        }

        public static (int a, int r, int g, int b) ColorToARGB(Color color)
        {
            return (color.A, color.R, color.G, color.B);
        }
    }
}