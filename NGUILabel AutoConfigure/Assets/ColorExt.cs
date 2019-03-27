using UnityEngine;

/// <summary>
///     Static helper for Colors
/// </summary>
public static class ColorExt
{
    /// <summary>
    ///     returns the same color with a new alpha value
    /// </summary>
    /// <param name="c"></param>
    /// <param name="a"></param>
    /// <returns></returns>
    public static Color SetAlpha(this Color c, float a)
    {
        return new Color(c.r, c.g, c.b, a);
    }

    /// <summary>
    ///     Color To String
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public static string Serialize(this Color c)
    {
        return string.Format("{0},{1},{2},{3}", c.r, c.g, c.b, c.a);
    }

    /// <summary>
    ///     String to Color
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static Color Parse(string color)
    {
        var s = color.Split(',');

        return new Color(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]), float.Parse(s[3]));
    }

    public static Color HexStrToColor(string hex)
    {
        Color tColor = default(Color);
        ColorUtility.TryParseHtmlString(hex, out tColor);

        return tColor;
    }

    /// <summary>
    /// Colors to hex string. exmp  0xFFFFFF
    /// </summary>
    /// <returns>The to hex string.</returns>
    /// <param name="color">Color.</param>
    public static string ColorToHexStr(Color color)
    {
        int i = 0xFFFFFF & (NGUIMath.ColorToInt(color) >> 8);
        return NGUIMath.DecimalToHex24(i);
    }

    /// <summary>
    /// FFFFFFFF 带alpha
    /// </summary>
    /// <returns>The to hex.</returns>
    /// <param name="color">Color.</param>
    public static string ColorToHex(Color color)
    {
        int r = Mathf.RoundToInt(color.r * 255.0f);
        int g = Mathf.RoundToInt(color.g * 255.0f);
        int b = Mathf.RoundToInt(color.b * 255.0f);
        int a = Mathf.RoundToInt(color.a * 255.0f);
        string hex = string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", r, g, b, a);
        return hex;
    }

    /// <summary>
    /// FFFFFFFF 带alpha
    /// </summary>
    /// <returns>The to color.</returns>
    /// <param name="hex">Hex.</param>
    public static Color HexToColor(string hex)
    {
        hex = hex.Replace("#", string.Empty);
        byte br = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte bg = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte bb = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        byte cc = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
        float r = br / 255f;
        float g = bg / 255f;
        float b = bb / 255f;
        float a = cc / 255f;
        return new Color(r, g, b, a);
    }
}