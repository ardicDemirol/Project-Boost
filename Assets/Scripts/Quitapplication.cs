using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quitapplication : MonoBehaviour
{
    

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("We out");
            Application.Quit();
        }
    }
}
