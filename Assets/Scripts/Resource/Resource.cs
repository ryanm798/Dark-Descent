using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource : MonoBehaviour
{
    [Header("Resource Attributes")]
    public Slider resourceBar = null;
    public float maxValue = 10f;
    public float startValue = 10f;
    protected float currentValue;
    public bool wholeNumbers = true;

    virtual protected void Start()
    {
        if (wholeNumbers)
        {
            maxValue = Mathf.Round(maxValue);
            startValue = Mathf.Round(startValue);
        }
        if (resourceBar != null)
        {
            resourceBar.wholeNumbers = wholeNumbers;
            resourceBar.minValue = 0f;
        }
        SetMaxValue(maxValue);
        SetValue(startValue);
    }

    private void SetMaxValue(float value)
    {
        if (wholeNumbers)
            value = Mathf.Round(value);
        maxValue = value;
        if (resourceBar != null)
            resourceBar.maxValue = value;
    }

    public void SetValue(float value)
    {
        if (wholeNumbers)
            value = Mathf.Round(value);
        value = Mathf.Clamp(value, 0f, maxValue);
        currentValue = value;
        if (resourceBar != null)
            resourceBar.value = value;
    }
}
