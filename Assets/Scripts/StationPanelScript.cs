using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationPanelScript : MonoBehaviour
{

    public Button nameBut;
    public GameObject elementsPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setName(string stationName)
    {
        nameBut.GetComponentInChildren<Text>().text = stationName;
    }

    public GameObject getElementsPanel()
    {
        return elementsPanel;
    }
}
