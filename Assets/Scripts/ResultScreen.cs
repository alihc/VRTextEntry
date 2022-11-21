using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ResultScreen : MonoBehaviour
{
    public TextMeshProUGUI presentedText;
    public TextMeshProUGUI transcribedText;
    public TextMeshProUGUI characters;
    public TextMeshProUGUI time;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI errorRate;
    public TextMeshProUGUI msd;
    public TextMeshProUGUI spc;
    
    public void Okay()
    {
        ReferenceManager.Instance._uiManager.OnResultsOk();
        this.gameObject.SetActive(false);
    }

    public void ShowResults(string presentedString, string transsribedString, int charactersString, int timeString, float speedString, float errorRateString, int msdString, float spcString)
    {

        presentedText.text = presentedString;
        transcribedText.text = transsribedString;
        characters.text = charactersString + " (" + charactersString / 5.00f + ")";
        time.text = timeString + "s (" + timeString / 60.00f + " minutes)";
        speed.text = speedString + " wpm";
        errorRate.text = errorRateString + " %";
        msd.text = msdString + " ";
        spc.text = spcString + " ";
        ReferenceManager.Instance._uiManager.OnPhraseDone();
        gameObject.SetActive(true);
    }
}