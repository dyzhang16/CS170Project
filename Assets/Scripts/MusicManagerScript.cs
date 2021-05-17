
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MusicManagerScript : MonoBehaviour
{
    //public static MusicManagerScript instance;

    public Slider musicSlider;

    string sceneName;
    public AudioSource[] musicSource;

    private AudioSource tutorial_music;
    private AudioSource street_music;
    private AudioSource coffee_music;
    private AudioSource office_music;
  

    // Start is called before the first frame update
    void Awake()
    {
        
        DontDestroyOnLoad(this.gameObject);


        
        tutorial_music = musicSource[0];
        street_music = musicSource[1];
        coffee_music = musicSource[2];
        office_music = musicSource[3];
        


        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        
        
        
    }

    void Start()
    {
        musicSlider.value = 0.25f; // default music volume

        
      
        sceneChecker(sceneName);
        Debug.Log("start");

    }

    void sceneChecker(string name)
    {
        Debug.Log("tis running fine");

        switch (name)


        { 
            case "Tutorial":
                tutorial_music.Play();// start tut music
                street_music.Stop();

                Debug.Log("playing tutorial music");
                break;
            case "Park":
                tutorial_music.Stop(); // stop tutorial music
                street_music.Play(); // start street music
                Debug.Log("playing street music");
                break;
            case "StreetCoffee":
                coffee_music.Stop(); // stop coffee music after exiting 
                if (!street_music.isPlaying)
                {
                    street_music.Play(); //if street music is not playing then play (after exiting coffee)
                }
                break;
            case "CoffeeScene":
                street_music.Stop();
                coffee_music.Play();
                break;
            case "OfficeScene":
                street_music.Stop(); // stops street music
                office_music.Play();
                break;


        }
    }

    public void updateSound()// updates all tracks to current volume
    {
        tutorial_music.volume = musicSlider.value; 
        street_music.volume = musicSlider.value;
        coffee_music.volume = musicSlider.value;
        office_music.volume = musicSlider.value;

    }


}
