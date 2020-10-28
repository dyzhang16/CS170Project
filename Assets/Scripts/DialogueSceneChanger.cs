using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using UnityEngine.SceneManagement;  

public class DialogueSceneChanger : MonoBehaviour
{
    void Start()
    {

    }
    void Update()
    {
        
    }
        void onMouseDown()
    {
         SceneManager.LoadScene("Scene3");                  //https://forum.unity.com/threads/how-to-load-a-scene-with-a-button.508078/
    }
}
