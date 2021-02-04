using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingstuff : MonoBehaviour
{
    public GameObject coffeeCup;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(transform);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,-80*Time.deltaTime);

        Vector3 id = transform.position;
        id.y -=1;
        transform.position = id;

        RectTransform c = coffeeCup.transform as RectTransform;

        if(id.y <= c.position.y + c.rect.height*1.75) {
            Destroy(gameObject);
            SoundManagerScript.PlaySound("plop");
        }
    }
}
