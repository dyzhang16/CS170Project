using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class SoundManagerScript : MonoBehaviour {

    public static SoundManagerScript instance;

    public static AudioClip pickFlower;
    public static AudioClip flowerSuccess;
    public static AudioClip wallBump;
    public static AudioClip click;
    public static AudioClip playerTalk;
    public static AudioClip pourCoffee;
    public static AudioClip plopSound;
    public static AudioClip placeCup;
    public static AudioClip placeFilter;
    public static AudioClip placeCoffee;
    public static AudioClip openGate;
    public static AudioClip whistle;
    public static AudioClip placeFlower;
    //public static AudioClip dialogueSound;
    public static AudioClip stamp;
    public static AudioClip sign;
    public static AudioClip drawer;
    public static AudioClip slidingDocument;
    public static AudioClip officeAlert;
    public static AudioClip documentTransform;
    public static AudioClip slurp;
    public static AudioClip bounce;
    public static AudioClip score;
    public static AudioClip paperThrow;
    public static AudioClip crumple;
    public static AudioClip woosh;



    static AudioSource audioSrc;


    // Start is called before the first frame update

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        Debug.Log(sceneName);

        
        audioSrc = GetComponent<AudioSource>();
    }
    void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        //moved to awake in an attempt to fix webgl bugs
        pickFlower = Resources.Load<AudioClip>("pickup_flower_2");
        flowerSuccess = Resources.Load<AudioClip>("flower_success");
        wallBump = Resources.Load<AudioClip>("wallbump_1");
        playerTalk = Resources.Load<AudioClip>("player_talk");
        pourCoffee = Resources.Load<AudioClip>("pour_coffee");
        plopSound = Resources.Load<AudioClip>("plop");
        placeCup = Resources.Load<AudioClip>("place_cup");
        placeFilter = Resources.Load<AudioClip>("place_filter");
        placeCoffee = Resources.Load<AudioClip>("coffee_sound");
        openGate = Resources.Load<AudioClip>("open_gate");
        whistle = Resources.Load<AudioClip>("whistling_1");
        placeFlower = Resources.Load<AudioClip>("place_flower");
       //dialogueSound = Resources.Load<AudioClip>("dialogue_sound");
        stamp = Resources.Load<AudioClip>("stamp");
        sign = Resources.Load<AudioClip>("sign");
        drawer = Resources.Load<AudioClip>("drawer");
        slidingDocument = Resources.Load<AudioClip>("sliding_document");
        officeAlert = Resources.Load<AudioClip>("office_alert");
        documentTransform = Resources.Load<AudioClip>("document_transform");
        slurp = Resources.Load<AudioClip>("slurp");

        bounce = Resources.Load<AudioClip>("bounce_ball");
        score = Resources.Load<AudioClip>("ball_land");
        paperThrow = Resources.Load<AudioClip>("paper_toss");
        crumple = Resources.Load<AudioClip>("crumple");
        woosh = Resources.Load<AudioClip>("walking_3");
        // click = Resources.Load<AudioClip>("button_click");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public static void PlaySound(string clip)
    {
        if (!audioSrc.isPlaying)
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
                    audioSrc.PlayOneShot(wallBump, 0.2f);
                    break;
                case "click":
                    audioSrc.PlayOneShot(click);
                    break;
                case "player_talk":
                    audioSrc.PlayOneShot(playerTalk);
                    break;
                case "pour_coffee":
                    audioSrc.PlayOneShot(pourCoffee);
                    break;
                case "plop":
                    audioSrc.PlayOneShot(plopSound);
                    break;
                case "place_cup":
                    audioSrc.PlayOneShot(placeCup);
                    break;
                case "place_filter":
                    audioSrc.PlayOneShot(placeFilter);
                    break;
                case "place_coffee":
                    audioSrc.PlayOneShot(placeCoffee);
                    break;
                case "open_gate":
                    audioSrc.PlayOneShot(openGate, 0.2f); // lower gate volume
                    break;
                case "whistle":
                    audioSrc.PlayOneShot(whistle);
                    break;
                case "place_flower":
                    audioSrc.PlayOneShot(placeFlower);
                    break;
                /* case "dialogue_sound":
                     audioSrc.PlayOneShot(dialogueSound);
                     break;
                */
                case "sign_sound":
                    audioSrc.PlayOneShot(sign);
                    break;
                case "stamp_sound":
                    audioSrc.PlayOneShot(stamp);
                    break;
                case "drawer_sound":
                    audioSrc.PlayOneShot(drawer);
                    break;
                case "sliding_document":
                    audioSrc.PlayOneShot(slidingDocument);
                    break;
                case "office_alert":
                    audioSrc.PlayOneShot(officeAlert);
                    break;
                case "document_transform":
                    audioSrc.PlayOneShot(documentTransform);
                    break;
                case "slurp":
                    audioSrc.PlayOneShot(slurp, 0.25f); // lower slurp volume
                    break;
                case "score":
                    audioSrc.PlayOneShot(score);
                    break;
                case "bounce":
                    audioSrc.PlayOneShot(bounce);
                    break;
                case "paperThrow":
                    audioSrc.PlayOneShot(paperThrow);
                    break;
                case "crumple":
                    audioSrc.PlayOneShot(crumple);
                    break;
                case "woosh":
                    audioSrc.PlayOneShot(woosh);
                    break;
            }

        }
        else if(audioSrc.isPlaying && clip == "whistle") audioSrc.PlayOneShot(whistle); // whistle sound doesnt play when spawn (because standing on grave)


    }
    [YarnCommand("playSuccess")]
    public void playSuccess()
    {
        audioSrc.PlayOneShot(flowerSuccess);
    }

    [YarnCommand("giveDoc")]
    public void giveDoc()
    {
        audioSrc.PlayOneShot(pickFlower);
    }

   

    public void updateSfxVol(float vol)
    {
        //vol = vol / 10f;
        audioSrc.volume = vol;
        Debug.Log("sfx vol = " + vol);
    }
}
