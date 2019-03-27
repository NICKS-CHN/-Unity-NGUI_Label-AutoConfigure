using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// 读取配置来设置UILabel的值
/// </summary>
public static class UILabelConfigHelper
{

    /// <summary>
    /// 获取 GameObject 上所有 UILabel ，以其 FontID 更新其值。
    /// </summary>
    /// <param name="go">Go.</param>
    [Obsolete("预设中设置ID后会自动更新文本样式，运行时不再更新设置！")]
    public static void ApplyConfigToUILabel(GameObject go)
    {
        UpdateUILabelByConfig(go);
    }

    /// <summary>
    /// 以 FontID 更新 UILabel 的值（编辑器下用）
    /// </summary>
    /// <param name="uiLabel">User interface label.</param>
    /// <param name="fontID">Font I.</param>
    public static string UpdateUILabelByFontID(UILabel uiLabel, int fontID)
    {
        LoadPreLoadStaticData();
        return UpdateUILabelByConfig(uiLabel, fontID);
    }


    /// <summary>
    /// 得到FontI的颜色Hex值
    /// </summary>
    /// <param name="colorId"></param>
    /// <param name="DefaultId"></param>
    /// <returns></returns>
    public static string GetColorHexStr(int colorId, int DefaultId = -1)
    {
        ClientUIFontDto colorData = TempFontData.GetFontData(colorId);
        if (colorData == null)
        {
            if (DefaultId == -1)
                return "FFFFFFFF";
            return GetColorHexStr(DefaultId);
        }
        return colorData.colorTint.Replace("#", string.Empty);
    }

    /// <summary>
    /// 改变字体大小（必须存在字大小的字体数据）
    /// </summary>
    /// <param name="fontId"></param>
    /// <param name="fontSize"></param>
    /// <returns></returns>
    public static int ChangeFontIdSize(int fontId, int fontSize)
    {
        ClientUIFontDto fontData = TempFontData.GetFontData(fontId);
        if (fontData != null && fontData.fontSize != fontSize)
        {
            int temp = fontId;
            fontId = int.Parse(fontId.ToString().Substring(0, fontId.ToString().Length - 3) + fontSize.ToString());
            fontData = TempFontData.GetFontData(fontId);
            if (fontData == null)
                fontId = temp;
        }
        return fontId;
    }

    #region method

    /// <summary>
    /// 编辑器下编辑字体时预加载字体配置表
    /// </summary>
    private static void LoadPreLoadStaticData()
    {
        //TODO:加载并缓存字体配置表
        TempFontData.InitData();
    }

    private static void UpdateUILabelByConfig(GameObject go)
    {
        if (null == go)
            return;
        UILabel[] mUILabelList = go.GetComponentsInChildren<UILabel>(true);
        UpdateUILabelByConfig(mUILabelList);
    }

    private static void UpdateUILabelByConfig(UILabel[] uiConfigLabelList)
    {
        if (null == uiConfigLabelList || uiConfigLabelList.Length <= 0)
            return;

        foreach (var uiLabel in uiConfigLabelList)
        {
            UpdateUILabelByConfig(uiLabel);
        }
    }

    private static void UpdateUILabelByConfig(UILabel uiConfigLabel)
    {
        if (null == uiConfigLabel)
            return;
        UpdateUILabelByConfig(uiConfigLabel, uiConfigLabel.FontID);
    }

    private static string UpdateUILabelByConfig(UILabel uiConfigLabel, int fontID)
    {
        if (null == uiConfigLabel || fontID <= 0)
            return null;

        ClientUIFontDto tClientUIFontDto = TempFontData.GetFontData(fontID);
        return UpdateUILabelByConfig(uiConfigLabel, tClientUIFontDto);
    }

    private static string UpdateUILabelByConfig(UILabel uiConfigLabel, ClientUIFontDto ClientUIFontDto)
    {
        if (null == uiConfigLabel || null == ClientUIFontDto)
            return null;
        uiConfigLabel.fontSize = ClientUIFontDto.fontSize;
        uiConfigLabel.bitmapFont = FontManager.GetFontNoNull(ClientUIFontDto.fontName);
        uiConfigLabel.fontStyle = (FontStyle)ClientUIFontDto.fontStyle;
        uiConfigLabel.applyGradient = ClientUIFontDto.gradient;
        uiConfigLabel.gradientTop = ColorExt.HexStrToColor(ClientUIFontDto.gradientTop);
        uiConfigLabel.gradientBottom = ColorExt.HexStrToColor(ClientUIFontDto.gradientBottom);
        uiConfigLabel.effectStyle = (UILabel.Effect)ClientUIFontDto.effect;
        uiConfigLabel.effectColor = ColorExt.HexStrToColor(ClientUIFontDto.effectColor.ToString());
        uiConfigLabel.effectDistance = new Vector2(ClientUIFontDto.effectX, ClientUIFontDto.effectY);
        uiConfigLabel.floatSpacingX = ClientUIFontDto.spacingX;
        uiConfigLabel.floatSpacingY = ClientUIFontDto.spacingY;
        uiConfigLabel.spacingX = ClientUIFontDto.spacingX;
        uiConfigLabel.spacingY = ClientUIFontDto.spacingY;
        uiConfigLabel.color = ColorExt.HexStrToColor(ClientUIFontDto.colorTint);
        return ClientUIFontDto.shortDesc;
    }

    #endregion
}

/// <summary>
/// Font配置数据
/// TIP:临时用写死的数据填充，实际应用上一般作为表数据存放
/// </summary>
public static class TempFontData
{
    private static Dictionary<int, ClientUIFontDto> fontDataDict;

    private static List<int> tFontIds = new List<int>() {100118,100120,100122};   //随便搞几个测试数据
    public static void InitData()
    {
        if (fontDataDict != null)
            return;
        fontDataDict = new Dictionary<int, ClientUIFontDto>();

        foreach (var tFontId in tFontIds)
        {
            var data = AddClientUiFontDto(tFontId);
            fontDataDict[data.id] = data;
        }
    }

    public static ClientUIFontDto GetFontData(int fontID)
    {
        if (fontDataDict == null)
            InitData();

        if (fontDataDict.ContainsKey(fontID))
            return fontDataDict[fontID];

        return null;
    }

    private static ClientUIFontDto AddClientUiFontDto(int FontId)
    {
        var data = new ClientUIFontDto();
        data.id = FontId;
        data.shortDesc = "通用深";
        data.fontName = "SIMYOU";
        data.colorTint = "#2E303FFF";
        data.fontSize = 22;
        data.fontStyle = 0;
        data.gradient = false;
        data.gradientTop = "";
        data.gradientBottom = "";
        data.effect = 0;
        data.effectColor = "";
        data.effectX = 0;
        data.effectY = 0;
        data.spacingX = 0;
        data.spacingY = 0;
        return data;
    }
}

