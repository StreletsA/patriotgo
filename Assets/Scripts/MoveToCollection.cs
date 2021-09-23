using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveToCollection : MonoBehaviour
{
    private Button btn;

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(toCollection);
    }

    private void toCollection()
    {
        //GetComponent<Animation>().Play("backback_anim");

        SceneManager.LoadScene("Collection2");
    }


}
