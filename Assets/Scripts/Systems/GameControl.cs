using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class GameControl : MonoBehaviour
{
    public Flask flask;
    private Color pickerColor;
    public TextMeshProUGUI text;
    private float score = 0;
    public TextMeshProUGUI scoreCanvas;
    public float accuracy;
    public ColorPaletteController paletteController;
    public ParticleSystem particle;
    public AttachGameObjectsToParticles attach;

    public Color PickerColor { get => pickerColor; set => pickerColor = value; }

    private void Start()
    {
        if (flask.colorCalculator.intValue.RuntimeValue != 0) paletteController.controlSV = true;
        newFlask();
    }

    public void Compare()
    {
        foreach (GameObject instance in attach.m_Instances)
        {
            instance.GetComponent<Light2D>().color = pickerColor;
        }
        particle.Play();
        float dr = (flask.mixedColor.r*256 + pickerColor.r * 256) / 2;
        float DR2 = (flask.mixedColor.r * 256 - pickerColor.r * 256) * (flask.mixedColor.r * 256 - pickerColor.r * 256);
        float DG2 = (flask.mixedColor.g * 256 - pickerColor.g * 256) * (flask.mixedColor.g * 256 - pickerColor.g * 256);
        float DB2 = (flask.mixedColor.b * 256 - pickerColor.b * 256) * (flask.mixedColor.b * 256 - pickerColor.b * 256);
        float dc = Mathf.Sqrt(Mathf.Sqrt(Mathf.Sqrt((2 + dr / 256) * DR2 + 4 * DG2 + (2 + ((255 - dr) / 256)) * DB2)/130));
        accuracy = (1 - dc) * 100;

        if(accuracy < 0)
            accuracy = 0.001f;
        text.text = accuracy.ToString().Substring(0,4) + "%";
        text.color = (Color.white - flask.mixedColor);
        score += accuracy*100;
        scoreCanvas.text = score.ToString();
        StartCoroutine("FadeCo");
    }

    void newFlask()
    {
        flask.liquids.Clear();
        int amountFlask1 = Random.Range(1,80);
        int amountFlask2 = Random.Range(1, 90 - amountFlask1);
        int amountSummary = amountFlask1 + amountFlask2;
        Color[] colors = new Color[3];
        switch (flask.colorCalculator.intValue.RuntimeValue)
        {
            case 0:
                colors[0] = Color.HSVToRGB(Random.Range(0f, 1f), 1, 1);
                colors[1] = Color.HSVToRGB(Random.Range(0f, 1f), 1, 1);
                colors[2] = Color.HSVToRGB(Random.Range(0f, 1f), 1, 1);
                break;
            case 1:
                colors[0] = Color.HSVToRGB(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                colors[1] = Color.HSVToRGB(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                colors[2] = Color.HSVToRGB(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                break;
            case 2:
                colors[0] = Color.HSVToRGB(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                colors[1] = Color.HSVToRGB(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                colors[2] = Color.HSVToRGB(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                break;
            default:
                colors[0] = Color.HSVToRGB(Random.Range(0f, 1f), 1, 1);
                colors[1] = Color.HSVToRGB(Random.Range(0f, 1f), 1, 1);
                colors[2] = Color.HSVToRGB(Random.Range(0f, 1f), 1, 1);
                break;
        }
        flask.liquids.Add(Liquid.CreateInstance(amountFlask1, colors[0]));
        flask.liquids.Add(Liquid.CreateInstance(amountFlask2, colors[1]));
        flask.liquids.Add(Liquid.CreateInstance(Random.Range(1, 100 - amountSummary), colors[2]));
        flask.Refresh();
        flask.MixColors();
        flask.Unblock();
    }

    IEnumerator FadeCo()
    {
        for (float ft = 1f; ft >= 0; ft -= 0.01f)
        {
            Color c = text.color;
            c.a = ft;
            text.color = c;
            yield return null;
        }
        newFlask();
    }
}
