using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionScript : MonoBehaviour
{

    public GameObject previewPanel;
    public GameObject elementNamePanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in elementNamePanel.transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));

        children = new List<GameObject>();
        foreach (Transform child in previewPanel.transform) children.Add(child.gameObject);
        children.ForEach(child => child.SetActive(false));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
