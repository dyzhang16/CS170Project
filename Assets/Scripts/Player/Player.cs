using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class Player : MonoBehaviour               //https://stackoverflow.com/questions/46760846/how-to-move-2d-object-with-wasd-in-unity
{
    static AudioSource audioSrc; // audioSource for player talk sound
    public static AudioClip playerTalk;

    public float speed;
    public Rigidbody rb;
    public GameObject player;

    //fade in 
    public SpriteRenderer render;

    public bool paused;
    public bool settings;
    public GameObject settingsPanel;
    //animator for inventory button
    public Animator inventoryButtonAnimator;

    //animator
    //public Animator animator;

    public bool allowMovement = true;
    public bool playTalkSound = true;
    public Vector3 movement = new Vector3(0, 0, 0);

    public bool allowInv = true;

    public bool invActive = false;
    public bool moving = false;
    public bool goingToFade;
    
    //camera shake effect on drift
    public bool drifting = false;
    public Vector3 beginDriftPos;

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

        beginDriftPos = this.transform.position;
    }

    public void Update()
    {
        if(!paused){
            if (allowMovement)
            {
                float h = Input.GetAxis("Horizontal");
                float v = Input.GetAxis("Vertical");

                //keyboard movement
                if (h != 0 || v != 0){
                    movement.x = h;
                    movement.z = v;

                    moving = true;
                    drifting = false;

                    //animantor.SetFloat("Speed", Mathf.Abs(v) + Mathf.Abs(h));

                    if (h > 0){
                        render.flipX = true;
                    } else {
                        render.flipX = false;
                    }
                } else if (Input.GetMouseButton(0)){
                    Mouse();
                } else {
                    moving = false;
                    movement = new Vector3(0, 0, 0);
                    if (!drifting){
                        beginDriftPos = this.transform.position;
                        // Debug.Log("drifting at: " + beginDriftPos);
                        drifting = true;
                    }
                }

                if (!moving){
                    if (Vector3.Distance(this.transform.position, beginDriftPos) > 10f){
                        float dist = Vector3.Distance(this.transform.position, beginDriftPos)/50f;

                        // Debug.Log("drift distance long");
                        this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    }
                }
            }

            if (allowInv){
                if (Input.GetKeyDown(KeyCode.Tab)) {
                    openInventory();
                }
            }

            //select the continue button upon pressing space
            if(Input.GetKeyDown(KeyCode.Space)){
                if (GameObject.Find("DiaSystem Prefab 1/DialogueRunner") != null){
                    // Debug.Log("found dialogue runner");
                    GameObject dia = GameObject.Find("DiaSystem Prefab 1/DialogueRunner");
                    DialogueRunner diaRunner = dia.GetComponent<DialogueRunner>();
                    if (diaRunner.IsDialogueRunning){
                        //finding continue button
                        GameObject contButton = GameObject.Find("DiaSystem Prefab 1/DialogueCanvas/DialogueContainer/ContinueButton");

                        //if external event system
                        GameObject eveSys = GameObject.Find("EventSystem");
                        if (eveSys != null){
                            eveSys.GetComponent<EventSystem>().SetSelectedGameObject(contButton);
                        }

                        //dialogue runnner event system
                        GameObject.Find("DiaSystem Prefab 1/EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(contButton);
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)){
            openSettings();
        }
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    void Mouse()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            if (!EventSystem.current.IsPointerOverGameObject()){
                rb.MovePosition(Vector3.MoveTowards(rb.position, hit.point, speed/2 * Time.fixedDeltaTime));

                moving = true;
                drifting = false;

                if (invActive){
                    openInventory();
                }
            } else {
                // Debug.Log(EventSystem.current.currentSelectedGameObject);
            }
            
        }
        
    }

    public void openInventory()
    {
        if (allowInv){
            invActive = !invActive;
            Inventory.instance.anim.SetBool("Inventory", invActive);
            inventoryButtonAnimator.SetTrigger("Highlighted");
            StartCoroutine(openInvAnim());
        }
    }

    IEnumerator openInvAnim(){
        yield return new WaitForSeconds(0.5f);
        inventoryButtonAnimator.SetBool("InventoryOpen", invActive);

        if (!invActive){
            inventoryButtonAnimator.SetTrigger("Normal");
        }
    }

    public void openSettings(){
        if (paused)
        {
            Time.timeScale = 1;
        }
        else
        {
            stopMove();
            Time.timeScale = 0;
        }
        allowInv = paused;
        allowMovement = paused;
        paused = !paused;
        settings = !settings;
        settingsPanel.SetActive(settings);
    }

    public void activateMenu(){
        invActive = true;
        Inventory.instance.anim.SetBool("Inventory", true);
        inventoryButtonAnimator.SetBool("InventoryOpen", true);
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

    public void closeInv(){
        invActive = false;
        Inventory.instance.anim.SetBool("Inventory", false);
        inventoryButtonAnimator.SetBool("InventoryOpen", false);
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

    // public void OnCollisionEnter(Collision col)
    // {
    //     //if(col.gameObject.tag == "wall")
    //     SoundManagerScript.PlaySound("wall_bump");
    // }

    // Moves player to location (simulated walking)
    //      Parameters: [0] = location name, [1-3] = offset as numbers
    [YarnCommand("MovePlayer")]
    public void MovePlayer(string[] parameters)
    {
        // use better variable names for the parameters
        string locationName = parameters[0];
        string[] offset = new string[parameters.Length - 1];
        Array.Copy(parameters, 1, offset, 0, offset.Length);

        // get the GameObject that the location is referring to
        GameObject location = GameObject.Find(locationName);

        // if the location is null (i.e. the GameObject was not found), log a warning and do no movement
        if (location == null)
        {
            Debug.LogWarningFormat("MovePlayer: Could not find GameObject {0}. Player movement was not done.", locationName);
        }

        // calculate the target position + offset

        // target is first based off the location
        Vector3 targetPosition = location.transform.position;

        // turn the offset (string array) into floats
        
        if (offset.Length > 3)
        {
            Debug.LogWarning("MovePlayer: Too many arguments, only considering the x, y, z arguments");
        }

        // for-loop up to three offset arguments
        for (int i = 0; i < Mathf.Min(offset.Length, 3); i++)
        {
            try
            {
                // convert string to float and apply offset
                targetPosition[i] += float.Parse(offset[i]);
            }
            catch (System.FormatException exc)
            {
                // array for debug logging purposes
                char[] offsetChar = new char[] { 'x', 'y', 'z' };
                Debug.LogWarningFormat("MovePlayer: Failed to apply offset \"{0}\" for {1} position.", offset[i], offsetChar[i]);
            }
        }

        moving = true;

        // Move the player to the target location (with the offset)
        StartCoroutine(MovePlayerCR(targetPosition));
    }

    // Helper Coroutine for use with MovePlayer()
    IEnumerator MovePlayerCR(Vector3 targetPosition)
    {
        // loop movement until the player is at the target position
        while (transform.position != targetPosition)
        {
            // move the player via its transform
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            yield return null;
        }

        moving = false;
    }
}



