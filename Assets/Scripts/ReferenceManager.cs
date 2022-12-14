using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    public static ReferenceManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    [Header("Settings")]
    public int totalBlocks = 5;
    public int blockSize = 5;
    [Header("runTime")]
    public string P_Code;
    public bool isLeftHanded=false;
    public int currentTrial;
    public int currentBlock;
    public string currentKeyboard;
    public string condition;
    public bool hasFinishedFirstType = false;


    [Header("Links")]
    public FileManager fileManager;
    public DataManager _dataManager;
    public UiManager _uiManager;
    public TextEntryTrialManager _textEntryTrialManager;

    public static string Condition1String= "Standard_1H";
    public static string Condition2String= "Split_2H";
    public static string AppName = "KeyboardVRExperiment";

}
