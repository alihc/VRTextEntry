using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        OnRead();
        
    }

    public string OnInfoSave()
    {
        string directory = ReferenceManager.Instance._dataManager.UserData.initials + "-" + System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        string fname = directory+ ".txt";
        string path = Path.Combine(Application.persistentDataPath, fname);
        StreamWriter sw = new StreamWriter(path, false);
        sw.WriteLine("App,Participant,Initials,Age,Sex,Hand,VrExp, VrExpWeek_Hour");
        sw.WriteLine(
            ReferenceManager.AppName + "," +
            ReferenceManager.Instance.P_Code + "," +
            ReferenceManager.Instance._dataManager.UserData.initials+ "," +
            ReferenceManager.Instance._dataManager.UserData.age + "," + 
            ReferenceManager.Instance._dataManager.UserData.sex + "," +
            ReferenceManager.Instance._dataManager.UserData.handPreference + "," +
             ReferenceManager.Instance._dataManager.UserData.previousVRExp + "," +
            ReferenceManager.Instance._dataManager.UserData.Exp_per_Hour);
        
        sw.Close();
        if (File.Exists(path))
        {
            return directory;
        }
        else
        {
            return directory;
        }
    }

    public void OnQuestionareSave()
    {
        string directory = ReferenceManager.Instance._dataManager.path;
        string fname = directory + ".txt";
        string path = Path.Combine(Application.persistentDataPath, fname);
        StreamWriter sw = new StreamWriter(path, true);
        sw.WriteLine("Standard_Rating,Split_Rating,Preffered_Method");
        sw.WriteLine(
            ReferenceManager.Instance._dataManager.UserData.Standard_rating + "," +
            ReferenceManager.Instance._dataManager.UserData.Split_rating + "," +
            ReferenceManager.Instance._dataManager.UserData.Preferred_Keyboard);

        sw.Close();
       
    }

    public void OnKeystrokeSave()
    {

        string directory = ReferenceManager.Instance._dataManager.path;
        string fname = directory+"-" + ReferenceManager.Instance.currentKeyboard + "-00" + (ReferenceManager.Instance.currentBlock+1) + ".sd1";
        string path = Path.Combine(Application.persistentDataPath, fname);
        StreamWriter sw = new StreamWriter(path, false);
        for (int i = 0; i < ReferenceManager.Instance.blockSize; i++)
        {
            sw.WriteLine(ReferenceManager.Instance._dataManager.Blocks[ReferenceManager.Instance.currentBlock].TrialKeystrokes[i].originalString);
            sw.WriteLine(ReferenceManager.Instance._dataManager.Blocks[ReferenceManager.Instance.currentBlock].TrialKeystrokes[i].typedString);
            for (int j = 0; j < ReferenceManager.Instance._dataManager.Blocks[ReferenceManager.Instance.currentBlock].TrialKeystrokes[i].keystrokes.Count; j++)
            {
                sw.WriteLine(ReferenceManager.Instance._dataManager.Blocks[ReferenceManager.Instance.currentBlock].TrialKeystrokes[i].keystrokes[j].time.ToString()+" "+ReferenceManager.Instance._dataManager.Blocks[ReferenceManager.Instance.currentBlock].TrialKeystrokes[i].keystrokes[j].character);
                
            }
            sw.WriteLine("#");
        }


        sw.Close();

    }
    public void OnBlockSave()
    {
       
        string directory = ReferenceManager.Instance._dataManager.path;
        string fname = directory + "-" + ReferenceManager.Instance.currentKeyboard + "-00"+ (ReferenceManager.Instance.currentBlock+1) + ".sd2";
        string path = Path.Combine(Application.persistentDataPath, fname);
        StreamWriter sw = new StreamWriter(path, false);
        sw.WriteLine("App,Participant,Keyboard,Condition,Block,Session,Keystrokes,Characters,Time(s),MSD,Speed(wpm),Error_rate(%),KSPC");
        for (int i=0; i<ReferenceManager.Instance.blockSize;i++)
        {
            sw.WriteLine(
                ReferenceManager.AppName + "," +
                ReferenceManager.Instance.P_Code + "," +
                ReferenceManager.Instance._dataManager.Blocks[ReferenceManager.Instance.currentBlock].Trials[i].keyboard + "," +
                ReferenceManager.Instance._dataManager.Blocks[ReferenceManager.Instance.currentBlock].Trials[i].condition + "," +
                ReferenceManager.Instance._dataManager.Blocks[ReferenceManager.Instance.currentBlock].Trials[i].block + "," +
                ReferenceManager.Instance._dataManager.Blocks[ReferenceManager.Instance.currentBlock].Trials[i].trial + "," +
                ReferenceManager.Instance._dataManager.Blocks[ReferenceManager.Instance.currentBlock].Trials[i].keystrokes + "," +
                ReferenceManager.Instance._dataManager.Blocks[ReferenceManager.Instance.currentBlock].Trials[i].characters + "," +
                ReferenceManager.Instance._dataManager.Blocks[ReferenceManager.Instance.currentBlock].Trials[i].time + "," +
                ReferenceManager.Instance._dataManager.Blocks[ReferenceManager.Instance.currentBlock].Trials[i].MSD + "," +
                ReferenceManager.Instance._dataManager.Blocks[ReferenceManager.Instance.currentBlock].Trials[i].speed + "," +
                ReferenceManager.Instance._dataManager.Blocks[ReferenceManager.Instance.currentBlock].Trials[i].error_rate + "," +
                ReferenceManager.Instance._dataManager.Blocks[ReferenceManager.Instance.currentBlock].Trials[i].SPC);
        }
        
       
        sw.Close();
       

    }

   

    public void OnRead()
    {
        TextAsset txt = (TextAsset)Resources.Load("phrases", typeof(TextAsset));
        List<string> phrases = new List<string>(txt.text.Split('\n'));
        for(int i = 0; i < phrases.Count; i++)
        {
            //Debug.Log("Phrase: " + phrases[i]);
            TextPhraseData newPhrase= new TextPhraseData();
            newPhrase.phrase = phrases[i];
            newPhrase.hasUsed = false;
            ReferenceManager.Instance._dataManager.PhrasesData.Add(newPhrase);
        }
    }
}
