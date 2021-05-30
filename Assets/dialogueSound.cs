using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueSound : MonoBehaviour
{
    public static AudioClip diaSound;
    static AudioSource dia;
    
    // Start is called before the first frame update
    void Start()
    {
        dia = GetComponent<AudioSource>();
        dia.volume = .25f;
    }

    // Update is called once per frame
    void Awake()
    {
        diaSound = Resources.Load<AudioClip>("dialogue_sound");
    }

    public void lineStop()
    {
        dia.Stop();
    }

    public void diaSoundStart()
    {
        try
        {
            dia.PlayOneShot(diaSound, 0.5f);
        }
        catch (System.Exception e)
        {
            // Debug.LogError(e.Message);
        }

    }
}
