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
        string fname =  ReferenceManager.Instance._dataManager.UserData.initials +"-"+ System.DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") +".txt";
        string path = Path.Combine(Application.persistentDataPath, fname);
        StreamWriter sw = new StreamWriter(path, false);
        sw.WriteLine("Initials Age Sex VrExp");
        sw.WriteLine(ReferenceManager.Instance._dataManager.UserData.initials+" "+ ReferenceManager.Instance._dataManager.UserData.age + " "+ ReferenceManager.Instance._dataManager.UserData.sex + " " + ReferenceManager.Instance._dataManager.UserData.previousVRExp);
        
        sw.Close();
        if (File.Exists(path))
        {
            return path;
        }
        else
        {
            return path;
        }
    }

    public void OnTialSave()
    {

        string path = ReferenceManager.Instance._dataManager.path;
        StreamWriter sw = new StreamWriter(path, true);
        sw.WriteLine("condition block trial characters time MSD speed error_rate SPC");
        for (int i=0; i<ReferenceManager.Instance._dataManager.TrialDatas.Count;i++)
        {
            sw.WriteLine(ReferenceManager.Instance._dataManager.TrialDatas[i].condition + " " +
                ReferenceManager.Instance._dataManager.TrialDatas[i].block + " " +
                ReferenceManager.Instance._dataManager.TrialDatas[i].trial + " " +
                ReferenceManager.Instance._dataManager.TrialDatas[i].characters + " " +
                ReferenceManager.Instance._dataManager.TrialDatas[i].time + " " +
                ReferenceManager.Instance._dataManager.TrialDatas[i].MSD + " " +
                ReferenceManager.Instance._dataManager.TrialDatas[i].speed + " " +
                ReferenceManager.Instance._dataManager.TrialDatas[i].error_rate + " " +
                ReferenceManager.Instance._dataManager.TrialDatas[i].SPC);
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
