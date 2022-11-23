﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultText : MonoBehaviour
{
    public InputField outTxt;
    Text resultText;
    // Start is called before the first frame update
    void Start()
    {
        resultText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowText()
    {
        resultText.text = outTxt.text;
    }
    public void OnInitials(InputField inpTxt)
    {
        outTxt.text = inpTxt.text;
        ReferenceManager.Instance._uiManager.OnInitialsDone(inpTxt.text);
    }
    public void OnAge(InputField inpTxt)
    {
        outTxt.text = inpTxt.text;
        ReferenceManager.Instance._uiManager.OnAgeDone(inpTxt.text);
    }
}
