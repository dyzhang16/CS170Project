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
    public string NPCName = "";
    public string NPCInteract = "";

    public GameObject dialogueCursor;

    private BoxCollider box;

    public bool runDialogue;
    // Start is called before the first frame update
    void Start()
    {
        runDialogue = false;
        box = GetComponent<BoxCollider>();
        if (box == null){
            box = transform.Find("collider").GetComponent<BoxCollider>();
        }
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
                if (dialogueCursor != null){
                    //set the dialogue cursor active and position
                    dialogueCursor.SetActive(true);

                    dialogueCursor.transform.position = this.GetComponent<SpriteRenderer>().bounds.center;
                    // dialogueCursor.transform.position = this.transform.position + 
                    // new Vector3(0, this.GetComponent<SpriteRenderer>().bounds.size.y, this.GetComponent<SpriteRenderer>().bounds.size.y);

                    //set the scale of the dialogueCursor so that it doesn't cover the obj
                    float size = this.GetComponent<SpriteRenderer>().bounds.size.x * this.GetComponent<SpriteRenderer>().bounds.size.z;
                    if (size < 150){
                        dialogueCursor.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
                    } else {
                        dialogueCursor.transform.localScale = new Vector3(1f, 1f, 1f);
                    }
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
        if (collider.gameObject.CompareTag("Player"))
        {
            runDialogue = false;
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

                if (dialogueCursor != null){
                    //set the dialogue cursor active and position
                    dialogueCursor.SetActive(true);

                    dialogueCursor.transform.position = this.GetComponent<SpriteRenderer>().bounds.center;
                    // dialogueCursor.transform.position = this.transform.position + 
                    // new Vector3(0, this.GetComponent<SpriteRenderer>().bounds.size.y, this.GetComponent<SpriteRenderer>().bounds.size.y);

                    //set the scale of the dialogueCursor so that it doesn't cover the obj
                    float size = this.GetComponent<SpriteRenderer>().bounds.size.x * this.GetComponent<SpriteRenderer>().bounds.size.z;
                    if (size < 150){
                        dialogueCursor.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
                    } else {
                        dialogueCursor.transform.localScale = new Vector3(1f, 1f, 1f);
                    }
                }
            } 
        }
    }

    void OnCollisionExit(Collision collision){
        if (collision.gameObject.tag == "Player")
        {
            runDialogue = false;
            if (dialogueCursor != null){
                dialogueCursor.SetActive(false);
            }
        }
    }
}
