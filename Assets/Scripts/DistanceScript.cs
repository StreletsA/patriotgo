using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceScript : MonoBehaviour
{

    //private List<LocativeTarget> locativeTargets;
    public List<LocativeTarget> locativeTargets;
    public Text distanceText;

    private LocativeGPS locative;
    private List<float> distances;

    // Start is called before the first frame update
    void Start()
    {
        locative = Camera.main.GetComponent<LocativeGPS>();
        distances = new List<float>();
        //locativeTargets = new List<LocativeTarget>(FindObjectsOfType<LocativeTarget>());

    }

    // Update is called once per frame
    void Update()
    {

        foreach (LocativeTarget lt in locativeTargets)
        {
            distances.Add
                (
                    measure
                        (
                            (float)lt.latitude,
                            (float)lt.longitude,
                            (float)locative.latitude,
                            (float)locative.longitude
                        )
                );
        }

        distances.Sort();

        //Debug.Log(((int)distances[0]).ToString());
        distanceText.text = ((int)distances[0]).ToString();

        //distances = new List<float>();
        distances.RemoveAll(delegate (float x) { return true;  });

    }

    float measure(float lat1, float lon1, float lat2, float lon2)
    {
        // generally used geo measurement function
        float R = 6378.137f; // Radius of earth in KM
        float dLat = lat2 * Mathf.PI / 180 - lat1 * Mathf.PI / 180;
        float dLon = lon2 * Mathf.PI / 180 - lon1 * Mathf.PI / 180;
        float a = Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) +
        Mathf.Cos(lat1 * Mathf.PI / 180) * Mathf.Cos(lat2 * Mathf.PI / 180) *
        Mathf.Sin(dLon / 2) * Mathf.Sin(dLon / 2);
        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        float d = R * c;
        return d * 1000; // meters
    }
}
