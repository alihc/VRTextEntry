using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndOfTrial : MonoBehaviour
{
    public TextMeshProUGUI messsageText;
    public string stringForStandard;
    public string stringForSplit;
    public GameObject doneButton, nextButton;
    // Start is called before the first frame update
    void Start()
    {
        if(ReferenceManager.Instance.currentKeyboard.Equals("standard"))
        {
            messsageText.text = stringForStandard;
        }
        else
        {
            messsageText.text = stringForSplit;
        }
        if(ReferenceManager.Instance.hasFinishedFirstType)
        {
            doneButton.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(false);
        }
        else
        {
            doneButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(true);
        }
    }

    public void OnDone()
    {
        ReferenceManager.Instance._uiManager.finalQuestionair.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnNext()
    {
        ReferenceManager.Instance.hasFinishedFirstType = true;
        ReferenceManager.Instance.currentBlock = 0;
        ReferenceManager.Instance.currentTrial = 0;
        if (ReferenceManager.Instance.condition.Equals(ReferenceManager.Condition1String))
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
}
