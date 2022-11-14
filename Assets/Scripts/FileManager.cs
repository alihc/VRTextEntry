using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
}
