using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class ColorCalculator : ScriptableObject
{
    public IntValue intValue;

    public Color MixColors(List<Liquid> liquids)
    {
        switch (intValue.RuntimeValue)
        {
            case 0:
                return OnlyHMix(liquids);
            case 1:
                return HVMix(liquids);
            case 2:
                return NormalMix(liquids);
            default:
                return OnlyHMix(liquids);
        }
    }

    Color NormalMix(List<Liquid> liquids)
    {
        Color mixedColor = new Color(0, 0, 0, 0);
        int summaryAmount = 0;
       foreach (var liquid in liquids)
        {
            summaryAmount += liquid.amount;
        }
            foreach (var liquid in liquids)
        {
            mixedColor += liquid.color * liquid.amount / summaryAmount;
        }
        return mixedColor;
    }
    Color HVMix(List<Liquid> liquids)
    {
        Color mixedColor = new Color(0, 0, 0, 0);
        int summaryAmount = 0;
        foreach (var liquid in liquids)
        {
            summaryAmount += liquid.amount;
        }
        foreach (var liquid in liquids)
        {
            mixedColor += liquid.color * liquid.amount / summaryAmount;
        }
        float h, v, s;
        Color.RGBToHSV(mixedColor, out h, out s, out v);
        mixedColor = Color.HSVToRGB(h, s, 1);
        return mixedColor;
    }

    Color OnlyHMix(List<Liquid> liquids)
    {
        Color mixedColor = new Color(0, 0, 0, 0);
        int summaryAmount = 0;
        foreach (var liquid in liquids)
        {
            summaryAmount += liquid.amount;
        }
        foreach (var liquid in liquids)
        {
            mixedColor += liquid.color * liquid.amount / summaryAmount;

        }
        float h, v, s;
        Color.RGBToHSV(mixedColor, out h, out s, out v);
        mixedColor = Color.HSVToRGB(h, 1, 1);
        return mixedColor;
    }
}
