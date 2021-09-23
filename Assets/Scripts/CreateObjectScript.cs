using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObjectScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        GameObject pref = Resources.Load("RadioModels/" + GameMetaData.GetInstance().getObjectName() + "Target") as GameObject;
        //Debug.Log(GameMetaData.GetInstance().getObjectName());

        Vector3 position = transform.position;
        position.z += 5;
        transform.localScale = pref.transform.localScale;
        GameObject model = Instantiate(pref, position, Quaternion.identity) as GameObject;
        model.transform.SetParent(transform);

        //Instantiate(pref, transform);

    }

    
}
