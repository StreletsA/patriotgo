using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CleanCollection : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(clean);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void clean()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("score", 0);
        SceneManager.LoadScene("SampleScene");
        
    }
}
