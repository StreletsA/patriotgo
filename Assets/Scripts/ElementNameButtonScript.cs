using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementNameButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        var colors = GetComponent<Button>().colors;

        if (isFound(tag))
        {
            colors.normalColor = Color.yellow;
            colors.pressedColor = Color.yellow;
            colors.selectedColor = Color.yellow;
            colors.highlightedColor = Color.yellow;
        }
        else
        {
            colors.normalColor = Color.red;
            colors.pressedColor = Color.red;
            colors.selectedColor = Color.red;
            colors.highlightedColor = Color.red;
        }

        GetComponent<Button>().colors = colors;

    }

    private bool isFound(string modelTag)
    {

        string json = PlayerPrefs.GetString("json");
        JsonWorker.Model[] models = JsonWorker.JsonHelper.FromJson<JsonWorker.Model>(json);

        foreach (JsonWorker.Model m in models)
        {
            if (m.Name == modelTag)
            {
                Debug.Log(m.Name);
                if (m.IsFound)
                    return true;

                return false;

            }
        }

        return false;
    }
}
