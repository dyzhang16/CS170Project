using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class RunDialogue : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    public Player player;
    public string dialogueToRun;
    public bool startInstantly;

    public GameObject dialogueCursor;
    public bool showDialogueCursor = true;

    private BoxCollider box;

    public bool runDialogue;
    // Start is called before the first frame update
    void Start()
    {
        runDialogue = false;
        box = GetComponent<BoxCollider>();
        if (box == null){
            box = this.transform.Find("collider").GetComponent<BoxCollider>();
        }

        // //change dialoguecursor to interact if item
        // if (this.GetComponent<ItemAssignment>() != null){
        //     dialogueCursor = GameObject.Find("InteractCursor");
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if (runDialogue)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!dialogueRunner.IsDialogueRunning && player.allowMovement)
                {
                    if (box.isTrigger){
                        runDialogue = false;
                    }

                    dialogueRunner.StartDialogue(dialogueToRun);
                    // Debug.Log("running" + dialogueToRun);
                }
            }
        }
        
    }

    

    void OnTriggerStay(Collider collider)                          //https://www.youtube.com/watch?v=Bc9lmHjqLZc
    {
        if (startInstantly && collider.gameObject.CompareTag("Player"))
        {
            dialogueRunner.StartDialogue(dialogueToRun);
            startInstantly = false;
        }
        else
        {
            if (collider.gameObject.CompareTag("Player") && !runDialogue)
            {
                runDialogue = true;
                showDialogueCursor = true;
            }

            if (dialogueCursor != null && collider.gameObject.tag == "Player"){
                //set the dialogue cursor active and position
                if (showDialogueCursor)
                    dialogueCursor.SetActive(true);
                else
                    dialogueCursor.SetActive(false);

                dialogueCursor.transform.position = this.GetComponent<SpriteRenderer>().bounds.center;
                // dialogueCursor.transform.position = this.transform.position + 
                // new Vector3(0, this.GetComponent<SpriteRenderer>().bounds.size.y, this.GetComponent<SpriteRenderer>().bounds.size.y);

                //set the scale of the dialogueCursor so that it doesn't cover the obj
                float size = this.GetComponent<SpriteRenderer>().bounds.size.x * this.GetComponent<SpriteRenderer>().bounds.size.z;
                if (size < 100){
                    dialogueCursor.transform.localScale = new Vector3(1f, 1f, 1f);
                } else if (size < 500){
                    dialogueCursor.transform.localScale = new Vector3(2f, 2f, 1f);
                } else {
                    dialogueCursor.transform.localScale = new Vector3(4f, 4f, 1f);
                }
            }
        }

    }

    //clicking the npc
    void OnMouseDown()
    {
        if (!dialogueRunner.IsDialogueRunning && runDialogue)
        {
            dialogueRunner.StartDialogue(dialogueToRun);
        }
    }

    // //hover
    // void OnMouseEnter(){
    //     Cursor.SetCursor(GameManager.instance.cursorHoverTexture, Vector2.zero, CursorMode.Auto);
    // }

    // void OnMouseExit(){
    //     Cursor.SetCursor(GameManager.instance.cursorTexture, Vector2.zero, CursorMode.Auto);
    // }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player") || collider.gameObject.CompareTag("Document"))
        {
            runDialogue = false;
            showDialogueCursor = false;
            if (dialogueCursor != null){
                dialogueCursor.SetActive(false);
            }
        }

    }

    void OnCollisionStay(Collision collision){
        if (startInstantly && GetComponent<Collider>().gameObject.CompareTag("Player"))
        {
            dialogueRunner.StartDialogue(dialogueToRun);
            startInstantly = false;
        }
        else 
        {
            if (collision.gameObject.tag == "Player" && !runDialogue)
            {
                runDialogue = true;
                showDialogueCursor = true;
            } 

            if (dialogueCursor != null && collision.gameObject.tag == "Player"){
                //set the dialogue cursor active and position
                if (showDialogueCursor)
                    dialogueCursor.SetActive(true);
                else
                    dialogueCursor.SetActive(false);

                dialogueCursor.transform.position = this.GetComponent<SpriteRenderer>().bounds.center;
                // dialogueCursor.transform.position = this.transform.position + 
                // new Vector3(0, this.GetComponent<SpriteRenderer>().bounds.size.y, this.GetComponent<SpriteRenderer>().bounds.size.y);

                //set the scale of the dialogueCursor so that it doesn't cover the obj
                float size = this.GetComponent<SpriteRenderer>().bounds.size.x * this.GetComponent<SpriteRenderer>().bounds.size.z;
                if (size < 100){
                    dialogueCursor.transform.localScale = new Vector3(1f, 1f, 1f);
                } else if (size < 500){
                    dialogueCursor.transform.localScale = new Vector3(2f, 2f, 1f);
                } else {
                    dialogueCursor.transform.localScale = new Vector3(4f, 4f, 1f);
                }
            }
        }
    }

    void OnCollisionExit(Collision collision){
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Document")
        {
            runDialogue = false;
            showDialogueCursor = false;
            if (dialogueCursor != null){
                dialogueCursor.SetActive(false);
            }
        }
    }
}
