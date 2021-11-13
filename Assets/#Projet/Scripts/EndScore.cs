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
        if(seconds > 20)
        {
            text.text = "Really??????\n " + seconds_str + " sec";
        }
        if (seconds < 15)
        {
            text.text = "CROWN FLOWERS!\n " + seconds_str + " sec";
        }
        else
        {
            text.text = "... not bad\n " + seconds_str + " sec";
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
