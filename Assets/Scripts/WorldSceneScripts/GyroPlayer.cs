using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroPlayer : MonoBehaviour
{

    private float x = 0;
    private float y = 0;
    private float latitude;
    private float longitude;

    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
        Input.location.Start(2, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        GyroModifyCamera();

        latitude = Input.location.lastData.latitude;
        longitude = Input.location.lastData.longitude;
        gpsToXY(latitude, longitude);

        transform.position = new Vector3(x, y);

    }

    void GyroModifyCamera()
    {
        transform.rotation = GyroToUnity(Input.gyro.attitude);
    }

    Quaternion GyroToUnity(Quaternion quat)
    {
        return new Quaternion(quat.x, quat.z, quat.y, -quat.w);
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
