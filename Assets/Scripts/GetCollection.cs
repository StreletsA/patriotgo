using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static JsonWorker;

public class GetCollection : MonoBehaviour
{

    public List<GameObject> elementPanels;

    private Dictionary<int, string> tmp;
    private int ind = 0;
    private List<GameObject> buttons;
    private Dictionary<string, List<Model>> dict;

    

    // Start is called before the first frame update
    void Start()
    {

        GameMetaData gmd = GameMetaData.GetInstance();
        if (gmd.getModelPrefabs() != null && !PlayerPrefs.HasKey("json"))
            setDefaultCollection(gmd.getModelPrefabs());


        PlayerPrefs.DeleteKey("showen");

        tmp = new Dictionary<int, string>();
        dict = new Dictionary<string, List<Model>>();

        buttons = new List<GameObject>();
        Model[] models;

        string json = PlayerPrefs.GetString("json");
        Debug.Log(json);
        models = JsonWorker.JsonHelper.FromJson<Model>(json);

        foreach (Model model in models)
        {

            Debug.Log(model.Radiostation);

            if (!dict.ContainsKey(model.Radiostation))
            {
                List<Model> tmp = new List<Model>();
                tmp.Add(model);
                dict.Add(model.Radiostation, tmp);
            }
            else
            {
                dict[model.Radiostation].Add(model);
            }

        }

        completeElementPanels();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createButton2(Model model, GameObject panel)
    {


        GameObject element = Instantiate(Resources.Load("ElementButton") as GameObject) as GameObject;
        element.transform.SetParent(panel.transform, false);

        element.GetComponent<ElementButtonScript>().ElementName.text = model.RuName;

        if(model.IsFound)
            element.GetComponent<ElementButtonScript>().FindImage.sprite = Resources.Load<Sprite>("nice");
        else
            element.GetComponent<ElementButtonScript>().FindImage.sprite = Resources.Load<Sprite>("notfound");

        Button button = element.GetComponent<Button>();
        //tmp.Add(ind, model.Name);

        /*
        button.onClick.AddListener(() =>
        {
            GameMetaData.GetInstance().setObjectName(model.Name);
            SceneManager.LoadScene("ModelViewScene");
        });
        */

        buttons.Add(element);
    }

    public void createButton(Model model, GameObject panel)
    {


        GameObject newButton = new GameObject("button", typeof(Button), typeof(RectTransform), typeof(LayoutElement));
        newButton.transform.SetParent(panel.transform);

        RectTransform rect = newButton.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 1);
        rect.anchorMax = new Vector2(0, 1);
        rect.anchoredPosition = new Vector2(70, -65);
        rect.sizeDelta = new Vector2(1000, 100);

        Button button = newButton.GetComponent<Button>();
        button.transform.SetParent(newButton.transform);
        /*RectTransform buttonRect = button.GetComponent<RectTransform>();
        buttonRect.anchorMin = new Vector2(0, 0);
        buttonRect.anchorMax = new Vector2(1, 1);
        buttonRect.anchoredPosition = new Vector2(0, 0);
        buttonRect.sizeDelta = new Vector2(0, 0);*/

        GameObject newText = new GameObject("Text", typeof(Text));
        newText.transform.SetParent(newButton.transform);
        Debug.Log(model.RuName);
        newText.GetComponent<Text>().text = model.RuName;
        newText.GetComponent<Text>().font = Font.CreateDynamicFontFromOSFont("Arial", 26);
        newText.GetComponent<Text>().fontSize = 56;
        RectTransform rt = newText.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(1, 1);
        rt.anchoredPosition = new Vector2(0, 0);
        rt.sizeDelta = new Vector2(0, 0);
        if(model.IsFound)
            newText.GetComponent<Text>().color = Color.green;
        else
            newText.GetComponent<Text>().color = Color.red;
        newText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;

        //tmp.Add(ind, model.Name);
        button.onClick.AddListener(() =>
        {
            GameMetaData.GetInstance().setObjectName(model.Name);
            SceneManager.LoadScene("ModelViewScene");
        });

        buttons.Add(newButton);
    }


    public void completeElementPanels()
    {

        if (elementPanels == null) return;

        foreach(string radiostation in dict.Keys)
        {
            foreach(GameObject elementPanel in elementPanels)
            {
                Debug.Log(elementPanel.name + " --- " + radiostation);
                if(elementPanel.name == "ElementsPanel_" + radiostation)
                {
                    foreach(Model model in dict[radiostation])
                    {
                        createButton2(model, elementPanel);
                    }

                    
                }
            }
        }

    }

    private void setDefaultCollection(List<GameObject> modelPrefabs)
    {

        try
        {

            List<JsonWorker.Model> models = new List<JsonWorker.Model>();

            foreach (GameObject pref in modelPrefabs)
            {

                JsonWorker.Model model = new JsonWorker.Model();

                model.Name = pref.tag;
                model.IsFound = false;
                model.Latitude = (float)pref.GetComponent<LocativeTarget>().latitude;
                model.Longitude = (float)pref.GetComponent<LocativeTarget>().longitude;
                model.Radiostation = pref.tag.Substring(pref.tag.IndexOf("_") + 1);
                model.ModelName = pref.tag.Replace(" ", "_").Replace(tag[0], tag[0].ToString().ToLower()[0]) + ".fbx";
                model.RuName = pref.gameObject.GetComponent<ModelDetector>().ru_name;

                models.Add(model);
            }

            string json = JsonWorker.JsonHelper.ToJson<JsonWorker.Model>(models.ToArray());

            PlayerPrefs.SetString("json", json);

        }
        catch (Exception e)
        {

        }

    }
    private void OnDisable()
    {
        foreach(GameObject b in buttons)
        {
            b.SetActive(false);
        }
    }

}
