using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderUtility : MonoBehaviour
{
    private void Start()
    {
       // GoToScene(1);
    }
    public void GoToScene(int a)
    {
        SceneManager.LoadScene(a);
    }
}
