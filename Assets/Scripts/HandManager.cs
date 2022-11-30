using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public GameObject[] leftHandObjects;
    public GameObject[] rightHandObjects;
    // Start is called before the first frame update
    void Start()
    {
        SetHands();
    }

   void SetHands()
    {
        bool isLeft = ReferenceManager.Instance.isLeftHanded;
        for (int i=0;i<leftHandObjects.Length;i++)
        {
            leftHandObjects[i].SetActive(isLeft);
            rightHandObjects[i].SetActive(!isLeft);
        }
    }
}
