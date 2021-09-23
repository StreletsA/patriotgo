using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMetaData
{
    private string objectName;
    private List<GameObject> modelPrefabs;

    private static GameMetaData m_Instance;
    public GameMetaData()
    {
        objectName = null;
        modelPrefabs = null;
    }
    
   
    public void setObjectName(string objectName)
    {
        this.objectName = objectName;
    }

    public string getObjectName()
    {
        return objectName;
    }

    public void setModelPrefabs(List<GameObject> modelPrefabs)
    {
        this.modelPrefabs = modelPrefabs;
    }

    public List<GameObject> getModelPrefabs()
    {
        return modelPrefabs;
    }

    public static GameMetaData GetInstance()
    {
        if (m_Instance == null)
            m_Instance = new GameMetaData();

        return m_Instance;
    }
}
