using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // using TMPro


public class EndScore : MonoBehaviour
{
    public float seconds;
    public Text text;   // ou public TextMeshProUGUI tmpText;
    public string seconds_str;
    // Start is called before the first frame update
    void Start()
    {
        seconds = PlayerPrefs.GetFloat("seconds",0f);
        seconds_str = seconds.ToString("N2"); // "N2"->format:pour 2 chiffres après la virgule
        text.text = "Done!\n " + "TIME: " + seconds_str + " secs";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
