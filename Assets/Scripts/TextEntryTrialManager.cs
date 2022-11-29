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
        
        int _seconds = stopwatch.Elapsed.Seconds;
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
        speedWpm = ((float)characters / (float)_seconds) * 12.0f;
        ReferenceManager.Instance._uiManager.debugText.text = ReferenceManager.Instance._uiManager.debugText.text + "Total KeyStore: " + totalKeystrokes;
        ReferenceManager.Instance._uiManager.debugText.text = ReferenceManager.Instance._uiManager.debugText.text + "speedWpm: " + speedWpm;
        float kspc = (float)totalKeystrokes / (float)count;
        int MSD = LevenshteinDistance(ReferenceManager.Instance._uiManager.referenceText.text, inputText);
        float errorRate = CalculateErrorRate(ReferenceManager.Instance._uiManager.referenceText.text, inputText, MSD);
        //Data Writting
        TrialData data = new TrialData();
        data.keyboard = keyboardType;
        data.condition = ReferenceManager.Instance.condition;
        data.block = ReferenceManager.Instance.currentBlock+1.ToString();
        data.trial = ReferenceManager.Instance.currentTrial.ToString();
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
    public static int LevenshteinDistance(string s, string t)
    {
        int n = s.Length;
        int m = t.Length;
        int[,] d = new int[n + 1, m + 1];

        // Step 1
        if (n == 0)
        {
            return m;
        }

        if (m == 0)
        {
            return n;
        }

        // Step 2
        for (int i = 0; i <= n; d[i, 0] = i++)
        {
        }

        for (int j = 0; j <= m; d[0, j] = j++)
        {
        }

        // Step 3
        for (int i = 1; i <= n; i++)
        {
            //Step 4
            for (int j = 1; j <= m; j++)
            {
                // Step 5
                int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                // Step 6
                d[i, j] = Math.Min(
                    Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                    d[i - 1, j - 1] + cost);
            }
        }
        // Step 7
        return d[n, m];
    }

    float  CalculateErrorRate(string source, string target, int msd)
    {
        if(msd==1)
            return 0;
        int stringLenghth = (source.Length < target.Length) ? target.Length : source.Length;
        stringLenghth--;
        ReferenceManager.Instance._uiManager.debugText.text = ReferenceManager.Instance._uiManager.debugText.text + " String Length: " + stringLenghth;
        ReferenceManager.Instance._uiManager.debugText.text = ReferenceManager.Instance._uiManager.debugText.text + " MSD: " + msd;
        float errorRate = ((float)msd / (float)stringLenghth) * 100;
        ReferenceManager.Instance._uiManager.debugText.text = ReferenceManager.Instance._uiManager.debugText.text+ "Error Rate: " + errorRate;
        return errorRate;
         
    }
}
