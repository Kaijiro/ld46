using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text newText;
    public Text highText;
    private float newScore = 0f;

    private float highScore = 0f;

    void Awake()
    {
        newScore = PlayerPrefs.GetFloat("newScore");
        highScore = PlayerPrefs.GetFloat("highScore");
    }

    // Use this for initialization
    void Start()
    {
        newScore = PlayerPrefs.GetFloat("newScore");
        highScore = PlayerPrefs.GetFloat("highScore");

        int displayNew = Mathf.FloorToInt(newScore);
        int displayHigh = 0;

        newText.text = "You managed to keep LOKAT's catisfaction alive for  " + displayNew.ToString() + "s";

        if (newScore > highScore)
        {
            highScore = newScore;
            PlayerPrefs.SetFloat("highScore", highScore);
            displayHigh = Mathf.FloorToInt(highScore);
            highText.text = "Congratulations, you got a new record : " + displayHigh.ToString() + "s";

        } else
        {
            displayHigh = Mathf.FloorToInt(highScore);
            highText.text = "Your record : " + displayHigh.ToString() + "s";
        }

        
    }

}
