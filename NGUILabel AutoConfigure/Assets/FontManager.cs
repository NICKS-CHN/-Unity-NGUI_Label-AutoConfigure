
using System.Collections.Generic;
using UnityEngine;
public class FontManager
{
    private static string Default_Font = "SIMYOU";//默认字体
    private static Dictionary<string, UIFont> fontDic; //缓存项目字体资源

    public static UIFont GetFontNoNull(string fontName)
    {
        UIFont tUIFont = GetFont(fontName);
        if (null == tUIFont)
        {
            Debug.LogError(string.Format("字体资源（{0}）不存在，用回默认字体（{1}）", fontName, Default_Font));
            tUIFont = GetFont(Default_Font);
        }
        return tUIFont;
    }

    public static UIFont GetFont(string fontName)
    {
        if (string.IsNullOrEmpty(fontName))
            fontName = Default_Font;

        if (fontDic == null)
        {
            fontDic = new Dictionary<string, UIFont>();
        }

        if (fontDic.ContainsKey(fontName))
        {
            return fontDic[fontName];
        }
        GameObject fontPrefab = Resources.Load<GameObject>("FontAtlas/" + fontName);     //从资源加载里获取字体UIFont，为了方便这里直接Resources.Load
        if (fontPrefab != null)
        {
            UIFont uiFont = fontPrefab.GetComponent<UIFont>();
            if (uiFont != null)
            {
                fontDic.Add(fontName, uiFont);
                return uiFont;
            }
            return null;
        }
        return null;
    }
}
