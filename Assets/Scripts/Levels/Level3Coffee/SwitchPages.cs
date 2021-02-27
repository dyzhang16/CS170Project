using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPages : MonoBehaviour
{
    public GameObject PageOne, PageTwo, PageThree;
    private int activePage = 1;

    public void BackPage()
    {
        switch (activePage)
        {
            case 1:
                SwitchPageThree();
                activePage = 3;
                break;
            case 2:
                SwitchPageOne();
                activePage = 1;
                break;
            case 3:
                SwitchPageTwo();
                activePage = 2;
                break;

        }
    }
    public void FrontPage()
    {
        switch (activePage)
        {
            case 1:
                SwitchPageTwo();
                activePage = 2;
                break;
            case 2:
                SwitchPageThree();
                activePage = 3;
                break;
            case 3:
                SwitchPageOne();
                activePage = 1;
                break;

        }
    }
    private void SwitchPageOne()
    {
        PageOne.SetActive(true);
        PageTwo.SetActive(false);
        PageThree.SetActive(false);
    }
    private void SwitchPageTwo()
    {
        PageTwo.SetActive(true);
        PageOne.SetActive(false);
        PageThree.SetActive(false);
    }
    private void SwitchPageThree()
    {
        PageThree.SetActive(true);
        PageOne.SetActive(false);
        PageTwo.SetActive(false);
    }
}
