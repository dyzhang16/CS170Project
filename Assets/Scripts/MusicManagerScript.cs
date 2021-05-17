
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MusicManagerScript : MonoBehaviour
{
    public static MusicManagerScript instance;

    public float musicVolume = 0.25f;

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

        instance = this;


        
        tutorial_music = musicSource[0];
        street_music = musicSource[1];
        coffee_music = musicSource[2];
        office_music = musicSource[3];
        


        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        
        // sceneChecker();
        
    }

    void Start(){
        // Scene currentScene = SceneManager.GetActiveScene();
        // sceneName = currentScene.name;

        sceneChecker();
    }

    public void sceneChecker()
    {
        Debug.Log("tis running fine");

        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        Debug.Log(sceneName);

        switch (sceneName)


        { 
            case "Tutorial":
                tutorial_music.Play();// start tut music
                street_music.Stop();

                Debug.Log("playing tutorial music");
                break;
            case "Park":
                tutorial_music.Stop(); // stop tutorial music
                if (!street_music.isPlaying){
                    street_music.Play(); // start street music
                }
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
            case "CityOffice":
                if (!street_music.isPlaying){
                    street_music.Play();
                }
                office_music.Stop();
            case "OfficeScene":
                street_music.Stop(); // stops street music
                if (!office_music.isPlaying){
                    office_music.Play();
                }
                break;
            case "Office":
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

    public void updateVolume(){
        
    }
}
