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
        GameManager.instance.previousScene = SceneManager.GetActiveScene().name;

        GameObject temp = GameObject.Find("Player");
        if (temp != null){
            GameManager.instance.playerPosition = temp.transform.position;
        }
        StartCoroutine(FadeScene(sceneToChange));
    }

    IEnumerator FadeScene(string sceneToChange)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionSpeed);

        SceneManager.LoadScene(sceneToChange);
    }

    IEnumerator FadeArea(){
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionSpeed);

        transition.SetTrigger("End");
    }

    [YarnCommand("Fade")]
    public void Fade(){
        StartCoroutine(FadeArea());
    }

    void OnTriggerEnter(Collider collider)                          //https://www.youtube.com/watch?v=Bc9lmHjqLZc
    {
        if (collider.gameObject.CompareTag("Player")){
            // Player p = collider.gameObject.GetComponent<Player>();
            // Debug.Log(p);
            // p.AllowMove(false);
            // p.AllowInv(false);
            // p.stopMove();
            // p.closeInv();
            LoadNextScene(exitScene);
        }
    }
}
