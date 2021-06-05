
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DontDestroy : MonoBehaviour
{
    public static DontDestroy instance;
    
    public Slider musicSlider;
    
    string sceneName;
    public AudioSource[] musicSource;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }





        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        /*
        GameObject[] objs = GameObject.FindGameObjectsWithTag("streetmusic"); // checks if there is another track playing
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);

        }
       */

       // Debug.Log("current scene is: " + sceneName);
    }

    void Start()
    {
        musicSlider.value = 0.25f; // default music volume

        // Debug.Log("playing tutorial music");
        if (sceneName == "Tutorial")
        {
            // Debug.Log("playing tutorial music");
            musicSource[0].Play();// start tut music
            
        }
        

    }

   /* bool streetChecker(string name)// for street music that plays across multiple scenes
    {
        if (name == "Park" || name == "StreetIntro" || name == "StreetCoffee" || name == "CityOffice" || name == "CityArcade" ||  name == "CoffeeScene") return true;
        else return false;
    }
   */

    void sceneChecker(string name)
    {
        
        switch (name)
        {
            case "Tutorial":
                musicSource[0].Play();// start tut music
                // Debug.Log("playing tutorial music");
                break;
            case "Park":
                musicSource[0].Stop(); // stop tutorial music
                musicSource[1].Play(); // start street music
                // Debug.Log("playing street music");
                break;
            case "StreetCoffee":
                musicSource[2].Stop(); // stop coffee music after exiting 
                if (!musicSource[1].isPlaying)
                {
                    musicSource[1].Play(); //if street music is not playing then play (after exiting coffee)
                }
                break;
            case "CoffeeScene":
                musicSource[1].Stop();
                musicSource[2].Play();
                break;


        }
    }

    public void updateSound()
    {
        for(int i = 0; i < musicSource.Length; i++)
        {
            musicSource[i].volume = musicSlider.value;
        }
    }

    
}
