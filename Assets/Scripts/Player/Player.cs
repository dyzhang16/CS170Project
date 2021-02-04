﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour               //https://stackoverflow.com/questions/46760846/how-to-move-2d-object-with-wasd-in-unity
{
    static AudioSource audioSrc; // audioSource for player talk sound
    public static AudioClip playerTalk;
    

    public float speed;
    public Rigidbody rb;
    public GameObject player;

    //fade in 
    public SpriteRenderer render;

    public GameObject pausePanel;
    public bool paused;

    //animator
    //public Animator animator;

    public bool allowMovement = true;
    public bool playTalkSound = true;
    public Vector3 movement = new Vector3(0, 0, 0);

    public bool allowInv = true;

    public bool invActive = false;
    public bool moving = false;
    public bool goingToFade;

    void Start()
    {
        playerTalk = Resources.Load<AudioClip>("player_talk");// loads talk sound
        audioSrc = GetComponent<AudioSource>();

        if (goingToFade){
            transform.Find("collider").gameObject.SetActive(false);
            allowMovement = false;
            Color c = render.material.color;
            c.a = 0f;
            render.material.color = c;
            StartCoroutine(FadeIn());
        }
    }

    public void Update()
    {
        if(!paused){
            if (allowMovement)
            {
                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");

                if (h != 0 || v != 0){
                    movement.x = h;
                    movement.z = v;

                    moving = true;

                    //animantor.SetFloat("Speed", Mathf.Abs(v) + Mathf.Abs(h));

                    if (h > 0){
                        render.flipX = true;
                    } else {
                        render.flipX = false;
                    }
                } else {
                    moving = false;
                    movement = new Vector3(0, 0, 0);
                }
            }

            if (allowInv){
                if (Input.GetKeyDown(KeyCode.Tab)) {
                    Menu();
                }
            }

            
        }

        if (Input.GetKeyDown(KeyCode.P)){
            Pause();
        }
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    void Mouse()
    {
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        rb.MovePosition(Vector3.MoveTowards(transform.position, targetPosition, 1f));

        moving = true;
    }

    void Menu()
    {
        invActive = !invActive;
        Inventory.instance.inventoryPanel.SetActive(invActive);
    }

    void Pause(){
        if (paused){
            Time.timeScale = 1;
        } else {
            stopMove();
            Time.timeScale = 0;
        }

        paused = !paused;
        pausePanel.SetActive(paused);
    }

    public void activateMenu(){
        invActive = true;
        Inventory.instance.inventoryPanel.SetActive(true);
    }

    public void AllowMove(bool allow)
    {
        allowMovement = allow;
    }

    public void AllowInv(bool allow){
        allowInv = allow;
    }

    public void stopMove(){
        movement = new Vector3(0, 0, 0);
    }

    public void stopTalkSound()
    {
        audioSrc.Stop();
    }

    public void startTalkSound()
    {
  
        audioSrc.Play();
        audioSrc.loop = true;

    }

    private IEnumerator FadeIn(){
        for (float f = 0.05f; f <= 1f; f += 0.05f){
            Color c = render.material.color;
            c.a = f;
            render.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }

        transform.Find("collider").gameObject.SetActive(true);
    }

    public void OnCollisionEnter(Collision col)
    {
        //if(col.gameObject.tag == "wall")
        SoundManagerScript.PlaySound("wall_bump");
    }
}



