using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Flask : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Liquid> liquids;
    public GameObject liquidPrefab;
    [SerializeField] private int summaryAmount = 0;
    private int order = 0;
    public Color mixedColor;
    public Signal2 colorCompare;
    public Animator animator;
    [SerializeField]private float mixLvl;
    private bool blocked;
    public ColorCalculator colorCalculator;
    void Start()
    {
        blocked = false;
        mixLvl = 0;
        liquids.Reverse();
        
        Refresh();
        MixColors();
    }

    public void MixColors()
    {
        mixedColor = colorCalculator.MixColors(liquids);
    }
    void Update()
    {
       if(mixLvl >= 0.7)
        {
            blocked = true;
            animator.SetBool("Shake", false);
            if (mixLvl >= 1)
            {
                setMixedColor();
                mixLvl = 0;
                StopAllCoroutines();
                colorCompare.Raise();

            }
        }
        
    }
    void OnMouseDown()
    {
        if (!blocked)
        {
            animator.SetBool("Shake", true);
            StopCoroutine("ColorUnMixCo");
            StartCoroutine("ColorMixCo");
        }
    }

    void OnMouseUp()
    {
        if (!blocked)
        {
            animator.SetBool("Shake", false);
            StopCoroutine("ColorMixCo");
            StartCoroutine("ColorUnMixCo");
        }
    }
    public void Unblock()
    {
        blocked = false;
    }

    void setMixedColor()
    {
        foreach (var liquid in liquids)
        {
            liquid.color = mixedColor;
        }
        Refresh();
    }

/*    void NormalMix()
    {
        mixedColor = new Color(0, 0, 0, 0);
        foreach (var liquid in liquids)
        {
            mixedColor += liquid.color * liquid.amount / summaryAmount;
        }
    }
    void HVMix()
    {
        mixedColor = new Color(0, 0, 0, 0);
        foreach (var liquid in liquids)
        {
            mixedColor += liquid.color * liquid.amount / summaryAmount;
        }
        float h, v, s;
        Color.RGBToHSV(mixedColor, out h, out s, out v);
        mixedColor = Color.HSVToRGB(h, s, 1);
    }

    public void OnlyHMix()
    {
        mixedColor = new Color(0, 0, 0, 0);
        foreach (var liquid in liquids)
        {
            mixedColor += liquid.color * liquid.amount / summaryAmount;

        }
        float h, v, s;
        Color.RGBToHSV(mixedColor, out h, out s, out v);
        mixedColor = Color.HSVToRGB(h, 1, 1);
    }*/
    public void Refresh()
    {
        order = 0;
        summaryAmount = 0;
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (var liquid in liquids)
        {
            liquidPrefab.GetComponent<LiquidInFlask>().liquid = liquid;
            liquidPrefab.GetComponent<LiquidInFlask>().sortingOrder = order;
            order--;
            liquidPrefab.GetComponent<LiquidInFlask>().currentLiquid = summaryAmount;
            summaryAmount += liquid.amount;
            GameObject newGameObject = GameObject.Instantiate(liquidPrefab);
            newGameObject.transform.position = transform.position;
            newGameObject.transform.rotation = transform.rotation;
            newGameObject.transform.SetParent(transform);
            newGameObject.transform.localScale = transform.localScale;
        }
    }
    IEnumerator ColorMixCo()
    {
        for (; mixLvl <= 1; mixLvl += 0.003f)
        {
            foreach (var liquid in liquids)
            {
                liquid.color = Color.Lerp(liquid.originalColor, mixedColor, mixLvl);
                liquid.intensity = mixLvl*50 + 39;
                liquid.size = liquid.size =  mixLvl * mixLvl * 2 + 1;
            }
            yield return null;
        }
    }
    IEnumerator ColorUnMixCo()
    {
        for (; mixLvl >= 0; mixLvl -= 0.002f)
        {
            foreach (var liquid in liquids)
            {
                liquid.color = Color.Lerp(liquid.originalColor, mixedColor, mixLvl);
                liquid.intensity = mixLvl*50 + 39;
                liquid.size = mixLvl * mixLvl * 2 + 1;
            }
            yield return null;
        }
    }
}
