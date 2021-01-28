using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class menusounds : MonoBehaviour
{
    public AudioSource click_sound;
    // start is called before the first frame update
    void start()
    {
   

    }

    // update is called once per frame
    void update()
    {

    }

    public void play_sound()
    {
        click_sound.Play();
    }
}
