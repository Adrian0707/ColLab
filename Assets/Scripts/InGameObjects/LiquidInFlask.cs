using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering.Universal;

public class LiquidInFlask : MonoBehaviour
{
    public Transform maskTransform;
    public Transform lightTransform;
    public Liquid liquid;
    [Range(0, 100)] public int currentLiquid;
    [Range(0, 100)] public int scale;
    public int sortingOrder;
    public Light2D liqLight;
    public GameObject objLight;
    Vector3[] vector3s;
    void Start()
    {
        transform.GetComponent<SortingGroup>().sortingOrder = sortingOrder;
        maskTransform.localScale = new Vector3(maskTransform.localScale.x, - (liquid.amount + currentLiquid) / 100f, maskTransform.localScale.z);
        objLight.transform.localPosition = new Vector3(maskTransform.localPosition.x, currentLiquid / (100f) * 0.93f, maskTransform.localPosition.z);

        liqLight.shapePath[2] = new Vector3(
            liqLight.shapePath[2].x,
            (liquid.amount) / 100f * 0.93f,
            liqLight.shapePath[2].z
            );
        liqLight.shapePath[3] = new Vector3(
            liqLight.shapePath[3].x,
            (liquid.amount) / 100f * 0.93f,
            liqLight.shapePath[3].z
            );
    }

    private void Update()
    {
        transform.GetComponent<SpriteRenderer>().color = liquid.color;
        liqLight.color = liquid.color;
        liqLight.intensity = liquid.intensity / 100f;
        lightTransform.localScale = new Vector3(liquid.size, liquid.size, lightTransform.localScale.z);

       // liqLight.volumeOpacity = liquid.vOpacity / 100f;
    }


}
