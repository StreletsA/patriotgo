using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static JsonWorker;

public class GetMessagesScript : MonoBehaviour
{

    public GameObject messagesCanvas;

    private Dictionary<string, List<Model>> dict;
    private int messagesCount;

    private Dictionary<string, string> ru_radiostations = new Dictionary<string, string>();

    void Start()
    {
        
        PlayerPrefs.DeleteKey("newmessage");

        ru_radiostations["R-187P"] = "Р-187П";
        ru_radiostations["R-168-5UN"] = "Р-168-5УН";
        ru_radiostations["R-168-25U-2"] = "Р-168-25У-2";
        ru_radiostations["R-159"] = "Р-159";
        ru_radiostations["TA-57"] = "ТА-57";
        ru_radiostations["TA-02"] = "ТА-02";
        ru_radiostations["TK-2"] = "ТК-2";

        messagesCount = 0;
        dict = new Dictionary<string, List<Model>>();

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
                createMessage("Вы собрали " + ru_radiostations[key]);
                messagesCount++;
            }
        }

        

        if (messagesCount == 0)
        {
            GameObject message = Instantiate(Resources.Load("NoMessages") as GameObject) as GameObject;
            message.transform.SetParent(transform, false);
        }
        
    }

    private void Update()
    {

          
        
        
    }

    private void createMessage(string messageText)
    {
        GameObject message = Instantiate(Resources.Load("MessagePanel") as GameObject) as GameObject;
        message.transform.SetParent(transform, false);
        message.GetComponent<MessageTextScript>().MessageText = messageText;
    }
}
