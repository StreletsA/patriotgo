using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageTextScript : MonoBehaviour
{

    public Text TextObject;
    public string MessageText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TextObject.text = MessageText;
    }
}
