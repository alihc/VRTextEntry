using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Feedback : MonoBehaviour
{
    private void OnEnable()
    {
        SliderHelper.MyMethod1Event += OnMetod1Value;
        SliderHelper.MyMethod2Event += OnMetod2Value;
    }

    private void OnDisable()
    {
        SliderHelper.MyMethod1Event -= OnMetod1Value;
        SliderHelper.MyMethod2Event -= OnMetod2Value;
    }

    public GameObject method1ButtonSelected, method2ButtonSelected;

    public void OnMetod1Value(int value)
    {
        ReferenceManager.Instance._dataManager.UserData.Standard_rating = value.ToString();
    }

    public void OnMetod2Value(int value)
    {
        ReferenceManager.Instance._dataManager.UserData.Split_rating = value.ToString();
    }

    public void OnPrefferedMethod(bool isMethod1)
    {
        method1ButtonSelected.SetActive(isMethod1);
        method2ButtonSelected.SetActive(!isMethod1);
        ReferenceManager.Instance._dataManager.UserData.Preferred_Keyboard = isMethod1 ? "Standard" : "Split";
    }

    public void OnDone()
    {
        ReferenceManager.Instance.fileManager.OnQuestionareSave();
        gameObject.SetActive(false);
    }
}
