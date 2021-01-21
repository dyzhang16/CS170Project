using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class UICommands : MonoBehaviour
{

    public RawImage blackscreen;

    [YarnCommand("ChangeScene")]
    public void ChangeScene(string sceneToChange){
        StartCoroutine(FadeImage(false, sceneToChange));
    }

    public void ExitGame(){
        Application.Quit();
    }

    IEnumerator FadeImage(bool fadeAway, string sceneToChange)
    {
        blackscreen.enabled = true;
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                blackscreen.color = new Color(0, 0, 0, i);
                yield return new WaitForSeconds(0.005f);
            }
            SceneManager.LoadScene(sceneToChange);
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                blackscreen.color = new Color(0, 0, 0, i);
                yield return new WaitForSeconds(0.005f);
            }
            SceneManager.LoadScene(sceneToChange);
        }
    }
}
