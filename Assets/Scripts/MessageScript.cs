using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static JsonWorker;

public class MessageScript : MonoBehaviour
{

    public Text mct;

    private Button btn;
    private Dictionary<string, List<Model>> dict;
    private int messagesCount;

    void Start()
    {

        messagesCount = 0;
        dict = new Dictionary<string, List<Model>>();


    }

    void Update()
    {
        mct.text = getMessageCount().ToString();
    }

    private int getMessageCount()
    {
        messagesCount = 0;

        string json = PlayerPrefs.GetString("json");
        JsonHelper.FromJson<Model>(json);

        Model[] models = JsonHelper.FromJson<Model>(json);
        Dictionary<string, int> radiostations = new Dictionary<string, int>();
        Dictionary<string, int> found_radiostations = new Dictionary<string, int>();

        foreach (Model m in models)
        {
            if (radiostations.ContainsKey(m.Radiostation))
            {
                radiostations[m.Radiostation] = radiostations[m.Radiostation] + 1;
            }
            else
            {
                radiostations.Add(m.Radiostation, 1);
                found_radiostations.Add(m.Radiostation, 0);
            }
        }

        foreach (Model m in models)
        {
            if (m.IsFound)
            {
                found_radiostations[m.Radiostation] = found_radiostations[m.Radiostation] + 1;
            }
        }

        foreach (string key in radiostations.Keys)
        {
            if (found_radiostations[key] == radiostations[key])
            {
                messagesCount++;
            }
        }


        

        return messagesCount;

    }
}
