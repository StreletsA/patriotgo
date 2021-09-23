using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReloadScript : MonoBehaviour
{
    private Button btn;

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(reloadScene);
    }

    private void reloadScene()
    {
        //GetComponent<Animation>().Play("backback_anim");

        SceneManager.LoadScene("SampleScene");
    }
}
