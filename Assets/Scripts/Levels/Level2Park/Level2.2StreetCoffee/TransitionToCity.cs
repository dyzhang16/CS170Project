using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class TransitionToCity : Transitions
{
    public DialogueRunner dia;

    public void OnCollisionEnter(Collision collision){
        if (collision.gameObject.CompareTag("Player") && GameManager.instance.firstFriendMeeting >= 2){
            LoadNextScene(exitScene);
        } else {
            dia.StartDialogue("coffeeNotGivenYet");
        }
    }
}
