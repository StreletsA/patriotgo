using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControl : MonoBehaviour
{

    public float speedRotateX = 5;
    public float speedRotateY = 5;
    public float speedRotateZ = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButton(0))
            return;

        float rotX = Input.GetAxis("Mouse X") * speedRotateX * Mathf.Deg2Rad;
        float rotY = Input.GetAxis("Mouse Y") * speedRotateY * Mathf.Deg2Rad;
        float rotZ = Input.GetAxis("Mouse Z") * speedRotateZ * Mathf.Deg2Rad;

        if (Mathf.Abs(rotX) > Mathf.Abs(rotY))
            transform.RotateAroundLocal(transform.up, -rotX);
        else
        {
            var prev = transform.rotation;
            transform.RotateAround(Camera.main.transform.right, rotY);
            if (Vector3.Dot(transform.up, Camera.main.transform.up) < 0.5f)
                transform.rotation = prev;
        }
    }
}
