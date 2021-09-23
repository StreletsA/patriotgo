using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Models : MonoBehaviour
{
    public List<GameObject> models;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject model in models)
        {
            Debug.Log(model.tag);
        }
    }

    
}
