using System;
using System.IO;
using UnityEngine;

public class JsonWorker
{

    string androidPath = Path.Combine(Application.streamingAssetsPath, "models.json");
    //string unityPath = "/GameDevelopment/Unity Projects/New Unity Project (7)/Assets/StreamingAssets/models.json";
    string unityPath = Path.Combine(Application.dataPath, "StreamingAssets/models.json");

    [System.Obsolete]
    public Model[] GetModels()
    {
        Model[] models = null;

        string path = androidPath;
        #if UNITY_EDITOR
        path = unityPath;
        #endif

        WWW www = new WWW(path);
        while (!www.isDone) { }

        string json = www.text;
        //string json = File.ReadAllText(path);

        //Debug.Log(json);

        

        //StreamReader sr = new StreamReader(path);
        //json = sr.ReadToEnd();
        //sr.Close();



        models = JsonHelper.FromJson<Model>(json);
        

        return models;
        
    }

    [Obsolete]
    public void AddModel(string modelName)
    {

        Model[] models = GetModels();

        foreach(Model m in models)
        {

            if(m.ModelName == modelName)
            {
                m.IsFound = true;
                break;
            }

        }

        string json = JsonHelper.ToJson<Model>(models, true);




        string path = androidPath;
#if UNITY_EDITOR
        path = unityPath;
#endif



        //StreamWriter sw = new StreamWriter(path);

        //sw.WriteLine(json);

        //sw.Close();

        File.WriteAllText(path, json);
       

    }

    [Obsolete]
    public void cleanCollection()
    {

        Model[] models = GetModels();

        foreach(Model model in models)
        {
            
            model.IsFound = false;
        }

        string json = JsonHelper.ToJson<Model>(models, true);

        string path = androidPath;
#if UNITY_EDITOR
        path = unityPath;
#endif



        //StreamWriter sw = new StreamWriter(path);

        //sw.WriteLine(json);

        //sw.Close();
        Debug.Log(json);
        File.WriteAllText(path, json);

    }

    [Serializable]
    public class Model
    {

        public string Name;
        public float Latitude;
        public float Longitude;
        public bool IsFound;
        public string Radiostation;
        public string ModelName;
        public string RuName;

    }

    public static class JsonHelper
    {

        // Help: https://ru.stackoverflow.com/questions/658811/%D0%9A%D0%B0%D0%BA-%D1%81%D0%B5%D1%80%D0%B8%D0%B0%D0%BB%D0%B8%D0%B7%D0%BE%D0%B2%D0%B0%D1%82%D1%8C-%D0%B8-%D0%B4%D0%B5%D1%81%D0%B5%D1%80%D0%B8%D0%B0%D0%BB%D0%B8%D0%B7%D0%BE%D0%B2%D0%B0%D1%82%D1%8C-json-%D0%B8-json-%D0%BC%D0%B0%D1%81%D1%81%D0%B8%D0%B2-%D0%B2-unity3d

        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }

}
