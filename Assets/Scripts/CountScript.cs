using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountScript : MonoBehaviour
{

    public Text countText;

    // Start is called before the first frame update
    void Start()
    {
        countText.text = getCount();
    }

    // Update is called once per frame
    void Update()
    {
        countText.text = getCount();
    }

    private string getCount()
    {

        string json = null;
        int count = 0;
        int allCount = 0;

        if (PlayerPrefs.HasKey("json"))
        {
            json = PlayerPrefs.GetString("json");

            foreach (JsonWorker.Model model in JsonWorker.JsonHelper.FromJson<JsonWorker.Model>(json))
            {
                if (model.IsFound)
                    count++;

                allCount++;
            }
        }

        return count.ToString() + "/" + allCount.ToString();
    }
}
