using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class barista : MonoBehaviour
{
    public Sprite awake, asleep;
    public bool woke = false;
    public GameObject bartender, point, player;
    private Player p;

    public float timer = 10;


    // Start is called before the first frame update
    void Start()
    {
        p = player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0) {
            if (timer <= 2) {
                point.SetActive(true);
            }

            timer -= Time.deltaTime;
        } else {
            switchState();
            point.SetActive(false);
        }

        if (woke) {
            if (p.moving) {
                resetScene();
            }
        }
    }

    void switchState() {
        if (woke) {
            bartender.GetComponent<SpriteRenderer>().sprite = asleep;
            timer = 10;
        } else {
            bartender.GetComponent<SpriteRenderer>().sprite = awake;
            timer = 5;
        }

        woke = !woke;
    }

    void resetScene() {
        SceneManager.LoadScene("CoffeeScene");
    }
}
