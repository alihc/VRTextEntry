using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
public class SliderHelper : MonoBehaviour
{
    public bool isMethod1;
    public Image slider;
    public int currentValue = 0;
    public TextMeshProUGUI text;
    public delegate void Method1(int value);
    public static event Method1 MyMethod1Event;

    public delegate void Method2(int value);
    public static event Method2 MyMethod2Event;

    public void OnAdd()
    {
        if (currentValue < 10)
        {
            currentValue++;
            slider.fillAmount = currentValue / 10.0f;
        }
        text.text = currentValue.ToString();
        if (isMethod1)
        {
            MyMethod1Event.Invoke(currentValue);
        }
        else
        {
            MyMethod2Event.Invoke(currentValue);
        }

    }
    public void OnRemove()
    {
        if (currentValue > 1)
        {
            currentValue--;
            slider.fillAmount = currentValue / 10.0f;
        }
        text.text = currentValue.ToString();
        if (isMethod1)
        {
            MyMethod1Event.Invoke(currentValue);
        }
        else
        {
            MyMethod2Event.Invoke(currentValue);
        }
    }
}
