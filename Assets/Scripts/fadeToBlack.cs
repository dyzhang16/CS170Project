using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeToBlack : MonoBehaviour
{

    public GameObject blackSquare;
    public float fadeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeToBlack() {
        Color col = blackSquare.GetComponent<Image>().color;
        float fadeAmount;

        while(blackSquare.GetComponent<Image>().color.a < 1) {
            fadeAmount = col.a + (fadeSpeed * Time.deltaTime);

            col = new Color(col.r, col.g, col.b, fadeAmount);

            blackSquare.GetComponent<Image>().color = col;
        }
    }
}
