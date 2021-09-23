using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        if (!PlayerPrefs.HasKey("score"))
        {
            PlayerPrefs.SetInt("score", 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        GetComponentInChildren<Text>().text = PlayerPrefs.GetInt("score").ToString(); 
    }
}
