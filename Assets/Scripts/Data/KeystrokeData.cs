using System;
using System.Collections.Generic;

[Serializable]

public class KeystrokeData 
{
    public string originalString;
    public string typedString;
    public List<KeystoreItems> keystrokes =new List<KeystoreItems>();
}

[Serializable]
public class KeystoreItems
{
    public float time;
    public string character;

   
}
