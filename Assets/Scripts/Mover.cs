using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mover : MonoBehaviour
{

    public List<Camera> camerasForHiding;
    public List<Camera> camerasForShowing;
    public GameObject canvasForHiding;
    public GameObject showCanvas;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(onClick);
    }

    private void onClick()
    {
        foreach(Camera cam in camerasForHiding)
            cam.gameObject.SetActive(false);
        foreach (Camera cam in camerasForShowing)
            cam.gameObject.SetActive(true);

        canvasForHiding.SetActive(false);
        showCanvas.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
