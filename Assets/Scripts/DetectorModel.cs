using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectorModel : MonoBehaviour
{

    private Vector3 unvisible;
    private Vector3 visible;

    // Start is called before the first frame update
    void Start()
    {

        visible = new Vector3(1, 1, 1);
        unvisible = new Vector3(0, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerPrefs.HasKey("showen"))
        {
            transform.localScale = visible;
            GetComponent<Animation>().Play();
        }

        else
        {
            transform.localScale = unvisible;
        }

    }
}
