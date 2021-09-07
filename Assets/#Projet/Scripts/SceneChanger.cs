using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public bool easyMode;
    public void Change(string sceneName)
    {
        if (sceneName == "SampleScene")
        {
            SceneManager.LoadScene("SampleScene"); // il passe à  la sample scene 
        }
        if(sceneName=="EasyScene")
        {
            easyMode = true;

            SceneManager.LoadScene("SampleScene");
        }
        


    }
    
}
