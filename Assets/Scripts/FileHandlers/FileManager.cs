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

    public bool OnSave()
    {
        string fname =  "ah.txt";
        string path = Path.Combine(Application.persistentDataPath, fname);
        StreamWriter sw = new StreamWriter(path, false);
        sw.WriteLine("Table successfully written to file!");
        sw.Close();
        if (File.Exists(path))
        {
            return true;
        }
        else
        {
            return false;
        }
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
