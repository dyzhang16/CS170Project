using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    string sceneName;
    // Start is called before the first frame update
    void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;


        GameObject[] objs = GameObject.FindGameObjectsWithTag("streetmusic"); // checks if there is another track playing
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);

        }


    }

    void Start()
    {


        if (streetChecker(sceneName))
        {
            Debug.Log("in street");
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);


    }

    bool streetChecker(string name)// for street music that plays across multiple scenes
    {
        if (name == "Park" || name == "StreetIntro" || name == "StreetCoffee" || name == "CityOffice" || name == "CityArcade") return true;
        else return false;
    }

    
}
