using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class Transitions : MonoBehaviour
{

    public Animator transition;
    public float transitionSpeed = 1f;

    public string exitScene;

    // public GameObject buttonPanel;
    // public GameObject menuPanel;

    [YarnCommand("ChangeScene")]
    public void ChangeScene(string sceneToChange){
        LoadNextScene(sceneToChange);
    }

    public void ExitGame(){
        Application.Quit();
    }

    public void LoadNextScene(string sceneToChange){
        GameManager.instance.saveItems();
        StartCoroutine(FadeScene(sceneToChange));
    }

    IEnumerator FadeScene(string sceneToChange)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionSpeed);

        SceneManager.LoadScene(sceneToChange);
    }

    IEnumerator FadeArea(){
        // transition.SetTrigger("");

        yield return new WaitForSeconds(transitionSpeed);

        // transition.SetTrigger("");
    }

    void OnTriggerEnter(Collider collider)                          //https://www.youtube.com/watch?v=Bc9lmHjqLZc
    {
        if (collider.gameObject.CompareTag("Player")){
            LoadNextScene(exitScene);
        }
    }
}
