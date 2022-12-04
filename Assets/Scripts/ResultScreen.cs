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
    public GameObject button;
    public TextMeshProUGUI blockText;
    public float waitTime=5f;

    private void Start()
    {
        
    }
    public void Okay()
    {
        if (ReferenceManager.Instance.currentBlock == ReferenceManager.Instance.totalBlocks)
        {
            this.gameObject.SetActive(false);
            ReferenceManager.Instance._uiManager.EndOfTrailScreen.SetActive(true);
        }
        else
        {
            ReferenceManager.Instance._uiManager.OnResultsOk();
            this.gameObject.SetActive(false);
        }
       
    }

    public void ShowResults(string presentedString, string transsribedString, int charactersString, float timeString, float speedString, float errorRateString, int msdString, float spcString, bool isBlockDone)
    {
        if(isBlockDone)
        {
            blockText.text = ReferenceManager.Instance.currentBlock+1 + " Block Completed";
            button.gameObject.SetActive(true);
            blockText.gameObject.SetActive(true);
        }
        else
        {
            button.gameObject.SetActive(false);
            blockText.gameObject.SetActive(false);
            Invoke("Okay", waitTime);
        }
        presentedText.text = presentedString;
        transcribedText.text = transsribedString;
        characters.text = charactersString + " (" + charactersString / 5.00f + " words)";
        time.text = timeString + "s (" + timeString / (float)60.00f + " minutes)";
        speed.text = speedString + " wpm";
        errorRate.text = errorRateString + " %";
        msd.text = msdString + " ";
        spc.text = spcString + " ";
        ReferenceManager.Instance._uiManager.OnPhraseDone();
        gameObject.SetActive(true);
        
       
    }
}
