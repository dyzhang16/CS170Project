using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingPanel : MonoBehaviour
{

    public GameObject level;
    public Transitions blackScreen;

    [YarnCommand("PanelOpen")]
    public void PanelOpen(){
        //disable music
        if (MusicManagerScript.instance != null){
            MusicManagerScript.instance.stopMusic();
        }

        //enable panel
        StartCoroutine(startFade());
    }

    IEnumerator startFade(){
        yield return new WaitForSeconds(1f);
        GetComponent<CanvasGroup>().alpha = 1;
        Destroy(level);
        yield return new WaitForSeconds(5f);
        blackScreen.gameObject.GetComponent<Animator>().SetTrigger("StartLong");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainMenu");
    }
}
