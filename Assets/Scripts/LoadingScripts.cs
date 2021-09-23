using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class LoadingScripts : MonoBehaviour
{
    public float tim = 5f;

    public void Start()
    {
        StartCoroutine(AskForPermissions());   
    }

    
    // Update is called once per frame
    void Update()
    {
        if (tim > 0) {
            GetComponent<Animation>().Play("load_anim");
            tim -= Time.deltaTime;
            Debug.Log(tim);
        }
        else
        {
            SceneManager.LoadScene("SampleScene");
        }
    }


    private IEnumerator AskForPermissions()
    {

        List<bool> permissions = new List<bool>() { false, false};
        List<bool> permissionsAsked = new List<bool>() { false, false };
        List<Action> actions = new List<Action>()
    {
        new Action(() => {
            permissions[0] = Permission.HasUserAuthorizedPermission(Permission.FineLocation);
            if (!permissions[0] && !permissionsAsked[0])
            {
                Permission.RequestUserPermission(Permission.FineLocation);
                permissionsAsked[0] = true;
                return;
            }
        }),
        new Action(() => {
            permissions[1] = Permission.HasUserAuthorizedPermission(Permission.Camera);
            if (!permissions[1] && !permissionsAsked[1])
            {
                Permission.RequestUserPermission(Permission.Camera);
                permissionsAsked[1] = true;
                return;
            }
        })
    };
        for (int i = 0; i < permissionsAsked.Count;)
        {
            actions[i].Invoke();
            if (permissions[i])
            {
                ++i;
            }
            yield return new WaitForEndOfFrame();
        }

    }
}
