using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialScreen;
    public GameObject keyboardPractice;
    public GameObject referenceTextParent;
    public GameObject proceedButton;
    public Text referenceText;
    public string[] tutorialPhrases;

    int counter = 0;
    public void OnStart()
    {

        tutorialScreen.SetActive(false);
        referenceTextParent.gameObject.SetActive(true);
        Invoke("SetPhrase", 1f);
    }

    void SetPhrase()
    {
        keyboardPractice.SetActive(true);
        if (counter==tutorialPhrases.Length)
        {
            counter = 0;
            proceedButton.gameObject.SetActive(true);
        }

        referenceText.text = tutorialPhrases[counter];
    }

    public void OnNext()
    {
        counter++;
        SetPhrase();
    }

    public void OnProceed()
    {
        gameObject.SetActive(false);
        keyboardPractice.SetActive(false);
        referenceTextParent.gameObject.SetActive(false);
        ReferenceManager.Instance._uiManager.OnResultsOk();
    }
}
