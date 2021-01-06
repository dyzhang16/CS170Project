using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class YarnChangeScene : MonoBehaviour
{
    public string sceneToChange;

    [YarnCommand("ChangeScene")]
    public void changeScene()
    {
        Debug.Log("fuck");
        SceneManager.LoadScene(sceneToChange);
    }

}
