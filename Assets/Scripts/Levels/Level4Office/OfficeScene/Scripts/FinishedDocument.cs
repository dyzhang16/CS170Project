﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class FinishedDocument : MonoBehaviour, IDropHandler
{
    public GameObject DocumentUI, DocumentT1, DocumentT2, DocumentT3;
    public DialogueRunner DialogueRunner;
    public string dialogueToRun;
    [HideInInspector] public int documentFinished = 0;
    private GameObject choice;
    private bool Boring = true;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.transform.tag == "Document")
        {
            if (eventData.pointerDrag.GetComponent<DocumentPuzzle>().SignArea && eventData.pointerDrag.GetComponent<DocumentPuzzle>().StampArea)
            {
                if (eventData.pointerDrag.GetComponent<DocumentPuzzle>().signedSpace && eventData.pointerDrag.GetComponent<DocumentPuzzle>().stampedSpace)
                {
                    ++documentFinished;
                    Debug.Log("Correct Document Finished: " + documentFinished);
                }
            }
            else if (eventData.pointerDrag.GetComponent<DocumentPuzzle>().SignArea)
            {
                if (eventData.pointerDrag.GetComponent<DocumentPuzzle>().signedSpace)
                {
                    ++documentFinished;
                    Debug.Log("Correct Document Finished: " + documentFinished);
                }
            }
            else if (eventData.pointerDrag.GetComponent<DocumentPuzzle>().StampArea)
            {
                if (eventData.pointerDrag.GetComponent<DocumentPuzzle>().stampedSpace)
                {
                    ++documentFinished;
                    Debug.Log("Correct Document Finished: " + documentFinished);
                }
            }
            else 
            {
                Debug.Log("Incorrect Document");
            }
            Destroy(eventData.pointerDrag.transform.gameObject);
            SpawnDocument();
           
            if (documentFinished == 5)
            {
                if (Boring) 
                {
                    Boring = false;
                    documentFinished = 0;
                    DialogueRunner.StartDialogue(dialogueToRun);
                }
            }
        }
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
        Debug.Log("Spawning: " + choice);
    }
}