using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstOpeningScript : MonoBehaviour
{

    public GameObject firstCanvas;

    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("not_first"))
        {
            PlayerPrefs.SetInt("not_first", 1);
            firstCanvas.SetActive(true);
        }
    }
}
