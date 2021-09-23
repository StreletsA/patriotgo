using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HelpScript : MonoBehaviour
{
    private Button btn;

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(toGame);
    }

    private void toGame()
    {

        SceneManager.LoadScene("HelpScene");
    }
}
