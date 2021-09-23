using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ModelSpawner : MonoBehaviour
{

    public double radius = 10;
    public List<GameObject> modelPrefabs = null;
    public Text distanceText;
    //public GameObject compassPanel;

    public Text PlayerLatitude;
    public Text PlayerLongitude;
    public Text ModelLatitude;
    public Text ModelLongitude;
    public Text Answer;
    public Text Radius;
    public Text ModelName;

    private GameObject prefab;
    private LocativeGPS locative;
    private double latitude;
    private double longitude;

    void Start()
    {

        GameMetaData gmd = GameMetaData.GetInstance();
        gmd.setModelPrefabs(modelPrefabs);

        Input.location.Start(2, 0.1f);

        Input.compass.enabled = true;

        int tmp = PlayerPrefs.GetInt("score");
        string json = null;

        bool not_first = false;
        if (PlayerPrefs.HasKey("not_first"))
            not_first = true;

        if (PlayerPrefs.HasKey("json"))
            json = PlayerPrefs.GetString("json");

        PlayerPrefs.DeleteAll();

        if (not_first)
            PlayerPrefs.SetInt("not_first", 1);

        PlayerPrefs.SetInt("score", tmp);
        if (json != null)
            PlayerPrefs.SetString("json", json);

        locative = new LocativeGPS();

        if (!PlayerPrefs.HasKey("json"))
            setDefaultCollection();

        //PlayerPrefs.DeleteKey("R-187P");
        
    }

    void Update()
    {

        string json = PlayerPrefs.GetString("json");

        double minDistance = 1e10;

        float minLat = 0;
        float minLong = 0;
        /*
        if (PlayerPrefs.HasKey("showen"))
            compassPanel.SetActive(false);
        else
            compassPanel.SetActive(true);
        */
        foreach (JsonWorker.Model model in JsonWorker.JsonHelper.FromJson<JsonWorker.Model>(json))
        {

            if (PlayerPrefs.HasKey("showen"))
            {
                distanceText.text = "";
            }


            else if (!PlayerPrefs.HasKey("showen") && !model.IsFound)
            {
                latitude = Input.location.lastData.latitude;
                longitude = Input.location.lastData.longitude;

                double distance = getDistanceFromLatLonInKm(model.Latitude, model.Longitude, latitude, longitude) * 1000 - radius;

                if(distance < minDistance)
                {
                    minDistance = distance;
                    minLat = model.Latitude;
                    minLong = model.Longitude;
                }
            }

            if (!PlayerPrefs.HasKey("showen") && !model.IsFound && inZone(model.Latitude, model.Longitude, model.ModelName) && !PlayerPrefs.HasKey(model.Name))
            {
                spawn(model.Name);
            }
        }

        rotateCompass((float)latitude, (float)longitude, minLat, minLong);

        if (minDistance < 1e10 && minDistance > 0)
            distanceText.text = ((int)(minDistance)).ToString() + " m";
        else
            distanceText.text = "";

    }

    void rotateCompass(float llat1, float llong1, float llat2, float llong2)
    {
        // pi - число pi, rad - радиус сферы (Земли)
        float rad = 6372795;
        
        // в радианах
        float lat1 = llat1 * Mathf.PI / 180;
        float lat2 = llat2 * Mathf.PI / 180;
        float long1 = llong1 * Mathf.PI / 180;
        float long2 = llong2 * Mathf.PI / 180;

        // косинусы и синусы широт и разницы долгот
        float cl1 = Mathf.Cos(lat1);
        float cl2 = Mathf.Cos(lat2);
        float sl1 = Mathf.Sin(lat1);
        float sl2 = Mathf.Sin(lat2);
        float delta = long2 - long1;
        float cdelta = Mathf.Cos(delta);
        float sdelta = Mathf.Sin(delta);

        // вычисления длины большого круга
        float y = Mathf.Sqrt(Mathf.Pow(cl2 * sdelta, 2) + Mathf.Pow(cl1 * sl2 - sl1 * cl2 * cdelta, 2));
        float x = sl1 * sl2 + cl1 * cl2 * cdelta;
        float ad = Mathf.Atan2(y, x);
        float dist = ad * rad;

        // вычисление начального азимута
        x = (cl1 * sl2) - (sl1 * cl2 * cdelta);
        y = sdelta * cl2;
        float z = Mathf.Atan(-y / x) * (180/Mathf.PI);


        if (x < 0)
            z = z + 180;
        
        float z2 = (z + 180) % 360 - 180;
        z2 = -z2 * (Mathf.PI / 180);
        float anglerad2 = z2 - ((2 * Mathf.PI) * Mathf.Floor((z2 / (2 * Mathf.PI))));
        float angledeg = (anglerad2 * 180) / Mathf.PI;


        //compassPanel.transform.localRotation = new Quaternion(0, 0, angledeg - Input.compass.magneticHeading, 0);
    }

    private bool inZone(double modelLat, double modelLong, string modelName)
    {

        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;

        double distance = getDistanceFromLatLonInKm(modelLat, modelLong, latitude, longitude) * 1000;

        PlayerLatitude.text = latitude.ToString();
        PlayerLongitude.text = longitude.ToString();
        ModelLatitude.text = modelLat.ToString();
        ModelLongitude.text = modelLong.ToString();
        Answer.text = distance.ToString();
        Radius.text = radius.ToString();
        ModelName.text = modelName;

        if (distance <= radius)
        {
            return true;
        }

        return false;
    }


    private double getDistanceFromLatLonInKm(double lat1, double lon1, double lat2, double lon2)
    {
        double R = 6371; // Radius of the earth in km
        double dLat = deg2rad(lat2 - lat1);  // deg2rad below
        double dLon = deg2rad(lon2 - lon1);
        double a =
          Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
          Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) *
          Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        double d = R * c; // Distance in km
        return d;
    }

    private double deg2rad(double deg)
    {
        return deg * (Math.PI / 180);
    }

    private void spawn(string modelTag)
    {
        prefab = Resources.Load("RadioModels/" + modelTag + "Target", typeof(GameObject)) as GameObject;

        System.Random rand = new System.Random();

        //Vector3 position = new Vector3(rand.Next(-30,20) + 5, 0, rand.Next(10,25));
        Vector3 position = transform.position;

        position.x -= rand.Next(5, 10);
        //position.y += 10;
        position.z -= rand.Next(5, 10);

        transform.localScale = prefab.transform.localScale;

        GameObject model = Instantiate(prefab, position, Quaternion.identity) as GameObject;
        ///model.transform.Rotate(new Vector3(0, 0, 90));
        model.transform.SetParent(transform);
        //model.transform.localRotation = Quaternion.Euler(90f, 180f, 0f);

        PlayerPrefs.SetInt(modelTag, 1);
        PlayerPrefs.SetInt("showen", 1);
    }

    private void setDefaultCollection()
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

}
