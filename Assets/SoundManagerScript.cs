using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour { 
    public static AudioClip pickFlower;
    public static AudioClip flowerSuccess;
    public static AudioClip wallBump;
    public static AudioClip click;
    static AudioSource audioSrc;


    // Start is called before the first frame update
    void Start()
    {
        pickFlower = Resources.Load<AudioClip>("pickup_flower_2");
        flowerSuccess = Resources.Load<AudioClip>("flower_success");
        wallBump = Resources.Load<AudioClip>("wallbump_1");
       // click = Resources.Load<AudioClip>("button_click");


        audioSrc = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "pickup_flower_2":
                audioSrc.PlayOneShot(pickFlower);
                break;
            case "flower_success":
                audioSrc.PlayOneShot(flowerSuccess);
                break;
            case "wall_bump":
                audioSrc.PlayOneShot(wallBump);
                break;
            case "click":
                audioSrc.PlayOneShot(click);
                break;
        }
    }
}
