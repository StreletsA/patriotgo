using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static JsonWorker;

public class ModelDetector : MonoBehaviour, IPointerClickHandler
{

    public bool isRotated = true;
    public Vector3 preferedScale = new Vector3(0.1f, 0.1f, 0.1f);
    public float rotateX = 0;
    public float rotateY = 0.5f;
    public float rotateZ = 0;
    public string ru_name;

    private Dictionary<string, List<Model>> dict;
    private int modelsCount;

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();

        
    }

    // Start is called before the first frame update
    void Start()
    {

        modelsCount = 0;
        dict = new Dictionary<string, List<Model>>();

        if (!PlayerPrefs.HasKey("json"))
        {
            JsonWorker.Model m = new JsonWorker.Model();
            m.Name = tag;
            m.IsFound = false;
            m.Radiostation = tag.Substring(tag.IndexOf(" ") + 1);
            m.ModelName = tag.Replace(" ", "_").Replace(tag[0], tag[0].ToString().ToLower()[0]) + ".fbx";
            m.Latitude = (float)GetComponent<LocativeTarget>().latitude;
            m.Longitude = (float)GetComponent<LocativeTarget>().longitude;
            JsonWorker.Model[] ms = { m };
            PlayerPrefs.SetString("json", JsonWorker.JsonHelper.ToJson<JsonWorker.Model>(ms, true));

        }

        string json = PlayerPrefs.GetString("json");
        JsonWorker.Model[] models = JsonWorker.JsonHelper.FromJson<JsonWorker.Model>(json);

        bool inJson = false;
        foreach(JsonWorker.Model m in models)
        {
            if(m.Name == tag)
            {
                inJson = true;
                break;
            }
        }

        if (!inJson)
        {
            JsonWorker.Model m = new JsonWorker.Model();
            m.Name = tag;
            m.IsFound = false;
            m.Radiostation = tag.Substring(tag.IndexOf(" ") + 1);
            m.ModelName = tag.Replace(" ", "_").Replace(tag[0], tag[0].ToString().ToLower()[0]) + ".fbx";
            m.Latitude = 0;
            m.Longitude = 0;
            m.RuName = ru_name;

            //models[models.Length] = m;

            List<JsonWorker.Model> ms = new List<JsonWorker.Model>(models);
            ms.Add(m);
            models = ms.ToArray();

            PlayerPrefs.SetString("json", JsonWorker.JsonHelper.ToJson<JsonWorker.Model>(models, true));
        }

        List<JsonWorker.Model> tmp = new List<JsonWorker.Model>(models);
        foreach(JsonWorker.Model m in tmp)
        {
            if(m.Name == tag)
            {
                if(m.IsFound == true)
                {
                    PlayerPrefs.DeleteKey(tag);
                    Destroy(this.gameObject);
                }
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
        if (isRotated)
            transform.Rotate(rotateX, rotateY, rotateZ);

        


    }
    
    private void OnMouseDown()
    {
        // add to collection
        //JsonWorker j = new JsonWorker();
        //j.AddModel(tag + ".fbx");
        //transform.localScale *= 2;

        //GetComponent<Animation>().Play("tap_anim");
        FindObjectOfType<Button>().GetComponent<Animation>().Play("backback_anim");

        string json = PlayerPrefs.GetString("json");
        JsonWorker.Model[] models = JsonWorker.JsonHelper.FromJson<JsonWorker.Model>(json);

        bool destroy = false;

        foreach (JsonWorker.Model m in models)
        {
            if(m.Name == tag)
            {
                if (!m.IsFound)
                {
                    PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 10);
                    m.IsFound = true;
                    destroy = true;
                }
                
            }
        }

        PlayerPrefs.SetString("json", JsonWorker.JsonHelper.ToJson<JsonWorker.Model>(models, true));

        


        if (destroy)
        {
            PlayerPrefs.DeleteKey(tag);
            PlayerPrefs.DeleteKey("showen");
            Destroy(this.gameObject);
        }

    }
    

}
