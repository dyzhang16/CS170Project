using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class friendFinishedDocuments : MonoBehaviour
{
    public Text text;
    [HideInInspector] public int documentFinished = 0;
    private float period = 0.0f;
    void Update()
    {
        float time = Random.Range(3, 10);
        if (period > time)
        {
            documentFinished += 1;
            Debug.LogWarning("Fredric Finished Document #" + documentFinished);
            period = 0;
        }
        period += Time.deltaTime;
        DisplayFredricFinishedDocuments();
    }
    public void DisplayFredricFinishedDocuments()
    {
        string s = documentFinished.ToString();
        text.text = s;
    }

}
