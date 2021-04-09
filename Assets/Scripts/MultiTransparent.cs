using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTransparent : MonoBehaviour
{
	public bool doTransparency = true;

    public float fadeAmount = 0.05f;

    // Items added to dictionary in OnTriggerStay, items removed in OnTriggerExit
    // private Dictionary<SpriteRenderer, Coroutine> spriteCoroutinePairs;

    void Awake()
    {
        // spriteCoroutinePairs = new Dictionary<SpriteRenderer, Coroutine>();
    }

    void Start()
    {
        if (fadeAmount < 0f || fadeAmount > 1f)
        {
            Debug.LogWarningFormat("MultiTransparent Warning! Fade Amount for {} is outside of the allowed bounds: {}", name, fadeAmount);
        }
    }

    // Based off of Transparent.cs, with new stuff
    void OnTriggerStay(Collider collider)
    {

        if (doTransparency)
        {
            if (collider.gameObject.tag == "Player")
            {
                if (GetComponent<SpriteRenderer>() != null)
                {
                    float dist = Vector3.Distance(this.transform.position, collider.gameObject.transform.position) / (GetComponent<SpriteRenderer>().bounds.size.x);

                    // Debug.Log(dist);

                    GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, dist);
                }
                else if (transform.parent.GetComponent<SpriteRenderer>() != null)
                {
                    float dist = Vector3.Distance(this.transform.parent.position, collider.gameObject.transform.position) / (transform.parent.GetComponent<SpriteRenderer>().bounds.size.x);

                    // Debug.Log(dist);

                    transform.parent.GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, dist);
                }

                // Make children of this game object also be transparent
                for (int i = 0; i < transform.childCount; i++)
                {
                    SpriteRenderer child = transform.GetChild(i).GetComponent<SpriteRenderer>();
                    if (child)
                    {
                        float dist = Vector3.Distance(child.transform.position, collider.gameObject.transform.position) /
                            (child.bounds.size.x);

                        child.color = new Color(child.color.r, child.color.g, child.color.b, dist);
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {

        if (collider.gameObject.tag == "Player")
        {
            if (doTransparency)
            {
                if (GetComponent<SpriteRenderer>() != null)
                {
                    GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 1f);
                }
                else if (transform.parent.GetComponent<SpriteRenderer>() != null)
                {
                    transform.parent.GetComponent<SpriteRenderer>().color = new Color(GetComponent<SpriteRenderer>().color.r, GetComponent<SpriteRenderer>().color.g, GetComponent<SpriteRenderer>().color.b, 1f);
                }

                // Make children of this game object also not be transparent
                for (int i = 0; i < transform.childCount; i++)
                {
                    SpriteRenderer child = transform.GetChild(i).GetComponent<SpriteRenderer>();
                    if (child)
                    {
                        child.color = new Color(child.color.r, child.color.g, child.color.b, 1f);
                    }
                }

            }
        }
    }

    // Transparency Coroutine
    IEnumerator MakeTransparentCR(SpriteRenderer sprite)
    {
        // Lower sprite's alpha by some amount until 30%
        while (sprite.color.a > 0.3f)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - fadeAmount);
            yield return null;
        }
    }
}
