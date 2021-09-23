using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelsScript : MonoBehaviour
{

    private LocativeTarget lt;
    private float x = 0;
    private float y = 0;

    // Start is called before the first frame update
    void Start()
    {
        lt = GetComponent<LocativeTarget>();

        gpsToXY((float)lt.latitude, (float)lt.longitude);

        transform.position = new Vector3(x, y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void gpsToXY(float latitude, float longitude)
    {
        float rLat = latitude * Mathf.PI / 180;
        float rLong = longitude * Mathf.PI / 180;
        float a = 6378137;
        float b = 6356752.3142f;
        float f = (a - b) / a;
        float e = Mathf.Sqrt(2 * f - (f * f));

        x = a * rLong;
        y = a * Mathf.Log(Mathf.Tan(Mathf.PI / 4 + rLat / 2) * Mathf.Pow(((1 - e * Mathf.Sin(rLat)) / (1 + e * Mathf.Sin(rLat))), (e / 2)));

    }
}
