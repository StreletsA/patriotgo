using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadioNameButtonScript : MonoBehaviour
{
    public List<GameObject> namePanelList;
    public GameObject elementNamePanel;
    public GameObject previewPanel;
    public GameObject notFoundPanel;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(onClick);

        var colors = GetComponent<Button>().colors;

        bool all = true;
        bool notAll = false;

        int count = 0;
        
        foreach (GameObject preview in namePanelList)
        {

            if (isFound(preview.tag))
                count++;

        }

        if (count == namePanelList.Count)
        {
            colors.normalColor = Color.yellow;
            colors.pressedColor = Color.yellow;
            colors.selectedColor = Color.yellow;
            colors.highlightedColor = Color.yellow;
        }
        else if (count > 0)
        {
            colors.normalColor = Color.blue;
            colors.pressedColor = Color.blue;
            colors.selectedColor = Color.blue;
            colors.highlightedColor = Color.blue;
        }
        else
        {
            //colors.normalColor = new Color(255, 3, 0);
            colors.normalColor = Color.red;
            colors.pressedColor = Color.red;
            colors.selectedColor = Color.red;
            colors.highlightedColor = Color.red;
        }

        GetComponent<Button>().colors = colors;

    }

    void onClick()
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in elementNamePanel.transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));

        
            foreach(GameObject preview in namePanelList)
            {

                GameObject prefab = Resources.Load("ElementName_Button") as GameObject;
                GameObject elementNameButton = Instantiate(prefab, elementNamePanel.transform);

                string elementName = preview.GetComponent<PreviewScript>().elementName;

                elementNameButton.tag = preview.tag;
                elementNameButton.GetComponentInChildren<Text>().text = elementName;

                elementNameButton.GetComponent<Button>().onClick.AddListener(() =>
                {

                    List<GameObject> previewChildren = new List<GameObject>();
                    foreach (Transform child in previewPanel.transform) previewChildren.Add(child.gameObject);
                    previewChildren.ForEach(child => child.active = false);


                    if (isFound(preview.tag))
                        preview.active = true;
                    else
                        notFoundPanel.active = true;

                });

            }
        

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
