using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;

public class TextEntryTrialManager : MonoBehaviour
{
    public string keyboardType;
    public GameObject keyboard;
    public Keyboard keyboardScript;
    public Stopwatch stopwatch= new Stopwatch();
   


    [Header("Runtime")]
    public int totalKeystrokes;
    bool isTyping;


    private List<KeystoreItems> keystoreItems;
    // Start is called before the first frame update
    void Start()
    {
        ReferenceManager.Instance._textEntryTrialManager = this;
        keystoreItems = new List<KeystoreItems>();
        ReferenceManager.Instance.currentKeyboard = keyboardType;

    }

    private void OnEnable()
    {
        Keyboard.OnKey += OnKeyPressed;
    }

    private void OnDisable()
    {
        Keyboard.OnKey -= OnKeyPressed;
    }

   

    void OnKeyPressed(string c)
    {
        if (isTyping)
        {
            KeystoreItems item = new KeystoreItems();
            long s = stopwatch.ElapsedMilliseconds;
            item.time = s;
            item.character = c;
            keystoreItems.Add(item);
        }
       
    }

    public void SelectNewPhrase()
    {
        int n = UnityEngine.Random.Range(0, ReferenceManager.Instance._dataManager.PhrasesData.Count);
        while (ReferenceManager.Instance._dataManager.PhrasesData[n].hasUsed)
        {
            n = UnityEngine.Random.Range(0, ReferenceManager.Instance._dataManager.PhrasesData.Count);
        }
        ReferenceManager.Instance._dataManager.PhrasesData[n].hasUsed = true;
        ReferenceManager.Instance._uiManager.referenceText.text = ReferenceManager.Instance._dataManager.PhrasesData[n].phrase;
       



    }

    public void OnStartPhraseEntry()
    {
        ReferenceManager.Instance._uiManager.debugText.text = "";
        totalKeystrokes = 0;
        keyboardScript.objectiveInputField.text = "";
        keyboard.SetActive(true);
       
        stopwatch.Start();
        isTyping = true;
    }

    public void OnPhraseDone(InputField inputText)
    {
        
        keyboard.SetActive(false);
        ReferenceManager.Instance.currentTrial++;
        if (ReferenceManager.Instance.currentTrial == ReferenceManager.Instance.blockSize)
        {
            CalculatePerformance(inputText.text, true);
            ReferenceManager.Instance.fileManager.OnBlockSave();
            ReferenceManager.Instance.fileManager.OnKeystrokeSave();
            ReferenceManager.Instance.currentBlock++;
            ReferenceManager.Instance.currentTrial = 0;
           


        }
        else
        {
            CalculatePerformance(inputText.text, false);
           
        }
        

    }

    void CalculatePerformance(string inputText, bool isBlockDone)
    {

        long Long_s = stopwatch.ElapsedMilliseconds;
        float _seconds = Long_s / (float)1000;
        stopwatch.Stop();
        stopwatch.Reset();
        isTyping = false;
        int characters;
        string s = inputText;
        int count = 0;
        foreach (char c in s)
        {
            count++;
        }
        characters = count-1;
        float speedWpm;
        speedWpm = (characters /_seconds) * 12.0f;
        //ReferenceManager.Instance._uiManager.debugText.text = ReferenceManager.Instance._uiManager.debugText.text + "Total KeyStore: " + totalKeystrokes;
        //ReferenceManager.Instance._uiManager.debugText.text = ReferenceManager.Instance._uiManager.debugText.text + "speedWpm: " + speedWpm;
        float kspc = (float)totalKeystrokes / (float)count;
        int MSD = findDistance(ReferenceManager.Instance._uiManager.referenceText.text, inputText);
        float errorRate = CalculateErrorRate(ReferenceManager.Instance._uiManager.referenceText.text, inputText, MSD);
        //Data Writting
        TrialData data = new TrialData();
        data.keyboard = keyboardType;
        data.condition = ReferenceManager.Instance.condition;
        data.block = "B0"+(ReferenceManager.Instance.currentBlock+1).ToString();
        data.trial = "S0"+ReferenceManager.Instance.currentTrial.ToString();
        data.keystrokes = (totalKeystrokes-1).ToString();
        data.characters = characters.ToString();
        data.time = _seconds.ToString();
        data.MSD = MSD.ToString();
        data.speed = speedWpm.ToString();
        data.error_rate = errorRate.ToString();
        data.SPC = kspc.ToString();
        ReferenceManager.Instance._dataManager.Blocks[ReferenceManager.Instance.currentBlock].Trials.Add(data);

        KeystrokeData keystrokeData = new KeystrokeData();
        keystrokeData.originalString = ReferenceManager.Instance._uiManager.referenceText.text;
        keystrokeData.typedString = inputText;
        foreach (KeystoreItems item in keystoreItems)
        {
            
            keystrokeData.keystrokes.Add(new KeystoreItems { time = item.time, character = item.character });
        }

        
        ReferenceManager.Instance._dataManager.Blocks[ReferenceManager.Instance.currentBlock].TrialKeystrokes.Add(keystrokeData);

        ReferenceManager.Instance._uiManager.resultsScreen.ShowResults(ReferenceManager.Instance._uiManager.referenceText.text, inputText, characters, _seconds, speedWpm, errorRate, MSD, kspc, isBlockDone);
        keystoreItems.Clear();
    }

    /// <summary>
    /// Returns the number of steps required to transform the source string
    /// into the target string.
    /// </summary>
    /// <summary>
    /// Compute the distance between two strings.
    /// </summary>
    public int findDistance(String a, String b)
    {
        int[,] d = new int[a.Length + 1, b.Length + 1];
        // Initialising first column:
        for (int i = 0; i <= a.Length; i++)
        {
            d[i, 0] = i;
        }
        // Initialising first row:
        for (int j = 0; j <= b.Length; j++)
        {
            d[0, j] = j;
        }
        // Applying the algorithm:
        int insertion;
        int deletion;
        int replacement;
        for (int i = 1; i <= a.Length; i++)
        {
            for (int j = 1; j <= b.Length; j++)
            {
                if (a[i - 1] == (b[j - 1]))
                {
                    d[i, j] = d[i - 1, j - 1];
                }
                else
                {
                    insertion = d[i, j - 1];
                    deletion = d[i - 1, j];
                    replacement = d[i - 1, j - 1];
                    // Using the sub-problems
                    d[i, j] = 1 + this.findMin(insertion, deletion, replacement);
                }
            }
        }
        int msd = d[a.Length, b.Length];
        if(a.Equals(b))
        {
            return 0;
        }
        else
        {
            msd = msd-1;
            return msd;
        }
       
    }
    // Helper funciton used by findDistance()
    public int findMin(int x, int y, int z)
    {
        if (x <= y && x <= z)
        {
            return x;
        }
        if (y <= x && y <= z)
        {
            return y;
        }
        else
        {
            return z;
        }
    }

    float  CalculateErrorRate(string source, string target, int msd)
    {
        if(msd==0)
            return 0;
        int stringLenghth = (source.Length < target.Length) ? target.Length : source.Length;
        stringLenghth--;
      //  ReferenceManager.Instance._uiManager.debugText.text = ReferenceManager.Instance._uiManager.debugText.text + " String Length: " + stringLenghth;
       // ReferenceManager.Instance._uiManager.debugText.text = ReferenceManager.Instance._uiManager.debugText.text + " MSD: " + msd;
        float errorRate = ((float)msd / (float)stringLenghth) * 100;
        ReferenceManager.Instance._uiManager.debugText.text = ReferenceManager.Instance._uiManager.debugText.text+ "Error Rate: " + errorRate;
        return errorRate;
         
    }
}
