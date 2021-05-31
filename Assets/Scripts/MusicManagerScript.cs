
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;

public class MusicManagerScript : MonoBehaviour
{
    public static MusicManagerScript instance;

    public float musicVolume;
    
    string sceneName;
    public AudioSource[] musicSource;

    private AudioSource tutorial_music;
    private AudioSource street_music;
    private AudioSource coffee_music;
    private AudioSource office_music;
    private AudioSource menu_music;


    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this){
            Destroy(this.gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        tutorial_music = musicSource[0];
        street_music = musicSource[1];
        coffee_music = musicSource[2];
        office_music = musicSource[3];
        menu_music = musicSource[4];

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
            case "MainMenu":
                tutorial_music.Stop();
                street_music.Stop();
                office_music.Stop();
                coffee_music.Stop();
                menu_music.Play();
        
                break;
            
            case "Tutorial":
                menu_music.Stop();
                tutorial_music.volume = musicVolume;
                tutorial_music.Play();// start tut music
                street_music.Stop();
                menu_music.Stop();
                break;
            case "Park":
                tutorial_music.Stop(); // stop tutorial music
                if (!street_music.isPlaying){
                    street_music.volume = musicVolume;
                    street_music.Play(); // start street music
                }
                menu_music.Stop();
                break;
            case "StreetCoffee":
                coffee_music.Stop(); // stop coffee music after exiting 
                if (!street_music.isPlaying)
                {
                    street_music.volume = musicVolume;
                    street_music.Play(); //if street music is not playing then play (after exiting coffee)
                }
                menu_music.Stop();
                break;
            case "CoffeeScene":
                street_music.Stop();
                coffee_music.volume = musicVolume;
                coffee_music.Play();
                menu_music.Stop();
                break;
            case "CityOffice":
                if (!street_music.isPlaying){
                    street_music.volume = musicVolume;
                    street_music.Play();
                }
                office_music.Stop();
                menu_music.Stop();
                break;
            case "OfficeScene":
                street_music.Stop(); // stops street music
                if (!office_music.isPlaying){
                    office_music.volume = musicVolume;
                    office_music.Play();
                }
                menu_music.Stop();
                break;
            case "Office":
                street_music.Stop(); // stops street music
                office_music.volume = musicVolume;
                office_music.Play();
                menu_music.Stop();
                break;
        }
    }

    public void updateVolume(float val){
        musicVolume = val;
        tutorial_music.volume = musicVolume; 
        street_music.volume = musicVolume;
        coffee_music.volume = musicVolume;
        office_music.volume = musicVolume;
        menu_music.volume = musicVolume;
    }
    [YarnCommand("StopMusic")]
    public void stopMusic(){
        menu_music.Stop();
        tutorial_music.Stop();
        street_music.Stop();
        coffee_music.Stop();
        office_music.Stop();
    }
}
