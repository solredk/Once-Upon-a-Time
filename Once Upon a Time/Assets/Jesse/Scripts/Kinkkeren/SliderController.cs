using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SliderController : MonoBehaviour
{
    public TextMeshProUGUI sliderText = null;

    public float maxsliderAmount;

    public void SliderChange(float value)
    {
        float localValue = value + maxsliderAmount;
        sliderText.text = localValue.ToString("0");
    }
}
