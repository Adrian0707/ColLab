using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class IntValue : ScriptableObject
{
    public int initialValue;

    public int RuntimeValue;

    private void OnEnable()
    {
        this.RuntimeValue = this.initialValue;
    }
    
    public void SetValueSlider(int value)
    {
        RuntimeValue = value;
    }
}
