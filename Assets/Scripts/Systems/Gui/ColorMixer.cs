using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class ColorMixer : MonoBehaviour
{
    public IntValue intValue;
    public Image imageLeft;
    public Light2D light2DL;
    public Image imageRight;
    public Light2D light2DR;
    public Image imageCenter;
    public Light2D light2DC;
    public ColorCalculator colorCalculator;

    private void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        List<Liquid> liquids = new List<Liquid>();
        Color[] colors = new Color[2];
        switch (intValue.RuntimeValue)
        {
            case 0:
                colors[0] = Color.HSVToRGB(Random.Range(0f, 1f), 1, 1);
                colors[1] = Color.HSVToRGB(Random.Range(0f, 1f), 1, 1);
                break;
            case 1:
                colors[0] = Color.HSVToRGB(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                colors[1] = Color.HSVToRGB(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                break;
            case 2:
                colors[0] = Color.HSVToRGB(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                colors[1] = Color.HSVToRGB(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                break;
            default:
                colors[0] = Color.HSVToRGB(Random.Range(0f, 1f), 1, 1);
                colors[1] = Color.HSVToRGB(Random.Range(0f, 1f), 1, 1);
                break;
        }
        liquids.Add(Liquid.CreateInstance(50, colors[0]));
        liquids.Add(Liquid.CreateInstance(50, colors[1]));
        imageLeft.color = liquids[0].color;
        light2DL.color = liquids[0].color;
        imageRight.color = liquids[1].color;
        light2DR.color = liquids[1].color;
        imageCenter.color = colorCalculator.MixColors(liquids);
        light2DC.color = imageCenter.color;
    }

}
