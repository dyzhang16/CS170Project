using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Yarn.Unity;

public class FinishedDocument : MonoBehaviour, IDropHandler
{
    public GameObject DocumentUI, DocumentT1, DocumentT2, DocumentT3, Arrow, WrongDocument;
    public DialogueRunner dia;
    public string dialogueToRun;
    [HideInInspector] public int documentFinished = 0;
    public Text text;
    private GameObject choice;
    private bool Boring = true;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.transform.tag == "Document")
        {
            if (eventData.pointerDrag.GetComponent<DocumentPuzzle>().SignArea && eventData.pointerDrag.GetComponent<DocumentPuzzle>().StampArea)
            {
                if (eventData.pointerDrag.GetComponent<DocumentPuzzle>().signedDocument && eventData.pointerDrag.GetComponent<DocumentPuzzle>().stampedDocument)
                {
                    SoundManagerScript.PlaySound("sliding_document");
                    ++documentFinished;
                    Debug.Log("Correct Document Finished: " + documentFinished);
                }
                else
                {
                    StartCoroutine(WrongDocumentNote());
                    WrongDocument.GetComponent<CanvasGroup>().alpha = 1;
                    Debug.LogWarning("INCORRECT DOCUMENT Y R U SO BAD");
                }
            }
            else if (eventData.pointerDrag.GetComponent<DocumentPuzzle>().StampArea && eventData.pointerDrag.GetComponent<DocumentPuzzle>().StampAreaTwo)
            {
                if (!eventData.pointerDrag.GetComponent<DocumentPuzzle>().signedDocument && eventData.pointerDrag.GetComponent<DocumentPuzzle>().stampedDocument)
                {
                    SoundManagerScript.PlaySound("sliding_document");
                    ++documentFinished;
                    Debug.Log("Correct Document Finished: " + documentFinished);
                }
                else
                {
                    StartCoroutine(WrongDocumentNote());
                    WrongDocument.GetComponent<CanvasGroup>().alpha = 1;
                    Debug.LogWarning("INCORRECT DOCUMENT Y R U SO BAD");
                }
            }
            else if (eventData.pointerDrag.GetComponent<DocumentPuzzle>().SignArea)
            {
                if (eventData.pointerDrag.GetComponent<DocumentPuzzle>().signedDocument && !eventData.pointerDrag.GetComponent<DocumentPuzzle>().stampedDocument)
                {
                    SoundManagerScript.PlaySound("sliding_document");
                    ++documentFinished;
                    Debug.Log("Correct Document Finished: " + documentFinished);
                }
                else
                {
                    StartCoroutine(WrongDocumentNote());
                    WrongDocument.GetComponent<CanvasGroup>().alpha = 1;
                    Debug.LogWarning("INCORRECT DOCUMENT Y R U SO BAD");
                }
            }
            Destroy(eventData.pointerDrag.transform.parent.gameObject);
            Arrow.GetComponent<CanvasGroup>().alpha = 0;
            DisplayFinishedDocuments();
            SpawnDocument();
           
            if (documentFinished == 5)
            {
                if (Boring) 
                {
                    Boring = false;
                    documentFinished = 0;
                    DisplayFinishedDocuments();
                    dia.StartDialogue(dialogueToRun);
                }
            }
        }
    }

    IEnumerator WrongDocumentNote()
    {
        WrongDocument.GetComponent<CanvasGroup>().alpha = 1;
        dia.StartDialogue("WrongDocument");
        yield return new WaitForSeconds(1f);
        WrongDocument.GetComponent<CanvasGroup>().alpha = 0;
    }


    public void DisplayFinishedDocuments()
    {
        string s = documentFinished.ToString();
        text.text = s;
    }
    public void SpawnDocument()
    {
        Vector3 pos = new Vector3(DocumentUI.transform.position.x, DocumentUI.transform.position.y, DocumentUI.transform.position.z);
        int x = Random.Range(1, 4);
        
        switch (x)
        {
            case 1:
                choice = DocumentT1;
                break;
            case 2:
                choice = DocumentT2;
                break;
            case 3:
                choice = DocumentT3;
                break;
        }
        GameObject SpawnedDocument = Instantiate(choice, pos, DocumentUI.transform.localRotation, DocumentUI.transform);
        SpawnedDocument.transform.SetAsFirstSibling();
        Debug.Log("Spawning: " + choice);
    }
}
