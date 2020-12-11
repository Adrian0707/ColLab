using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class Liquid : ScriptableObject
{
    [Range(1, 100)]public int amount;
    [Range(1, 100)] public float intensity;
    [Range(1, 100)] public float size;
    public Color color;
    [SerializeField] public Color originalColor;

    private void OnEnable()
    {
        color = originalColor;
    }
    public void Init(int amount, Color color, float intensity = 39, float size = 1)
    {
        this.amount = amount;
        this.color = color;
        this.originalColor = color;
        this.intensity = intensity;
        this.size = size;

    }
    public static Liquid CreateInstance(int amount, Color color, float intensiity = 39, float size = 1)
    {
        var data = ScriptableObject.CreateInstance<Liquid>();
        data.Init(amount, color, intensiity, size);
        return data;
    }
}