using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

[Serializable]
public class MyEvent : UnityEvent { }



public class Keyboard : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField objectiveInputField;
    public static Keyboard KB;
    public TextEntryTrialManager textEntryTrialManager;

    public MyEvent acceptEvent;
    public delegate void KeyEvent(string ch);
    public static event KeyEvent OnKey;


    void Start()
    {
        KB = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //adding a letter to the input
    public void AddChar(string c)
    {
        objectiveInputField.text +=c;
        if(textEntryTrialManager!=null)
        {
            textEntryTrialManager.totalKeystrokes++;
        }
        if (OnKey != null)
        {
            if(c.Equals(" "))
            {
                OnKey("space");
            }
            else
            {
                OnKey(c);
            }
        }
            
    }

    //removing a letter from the input
    public void RemoveChar()
    {
        string actualText = objectiveInputField.text;

        if (actualText.Length > 0)
        {
            objectiveInputField.text = actualText.Remove(actualText.Length - 1);
        }
        if (OnKey != null)
            OnKey("backspace");
    }

    
    public void Accept()
    {
        if (OnKey != null)
            OnKey("enter");
        acceptEvent.Invoke();
    }


}
