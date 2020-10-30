using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeMainScene : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        SceneManager.LoadScene("Scene1");
    }
}
