using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPages : MonoBehaviour
{
    public GameObject PageOne, PageTwo, PageThree, PageFour, PageFive, PageSix;
    private int activePage = 1;
    public bool threePager;

    public void BackPage()
    {
        switch (activePage)
        {
            case 1:
                if (threePager){
                    SwitchPageThree();
                    activePage = 3;
                } else {
                    SwitchPageSix();
                    activePage = 6;
                }
                break;
            case 2:
                SwitchPageOne();
                activePage = 1;
                break;
            case 3:
                SwitchPageTwo();
                activePage = 2;
                break;
            case 4:
                SwitchPageThree();
                activePage = 3;
                break;
            case 5:
                SwitchPageFour();
                activePage = 4;
                break;
            case 6:
                SwitchPageFive();
                activePage = 5;
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
                if (threePager){
                    SwitchPageOne();
                    activePage = 1;
                } else {
                    SwitchPageFour();
                    activePage = 4;
                }
                break;
            case 4:
                SwitchPageFive();
                activePage = 5;
                break;
            case 5:
                SwitchPageSix();
                activePage = 6;
                break;
            case 6:
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
        if (!threePager){
            PageFour.SetActive(false);
            PageFive.SetActive(false);
            PageSix.SetActive(false);
        }
    }
    private void SwitchPageTwo()
    {
        PageTwo.SetActive(true);
        PageOne.SetActive(false);
        PageThree.SetActive(false);
        if (!threePager){
            PageFour.SetActive(false);
            PageFive.SetActive(false);
            PageSix.SetActive(false);
        }
    }
    private void SwitchPageThree()
    {
        PageThree.SetActive(true);
        PageOne.SetActive(false);
        PageTwo.SetActive(false);
        if (!threePager){
            PageFour.SetActive(false);
            PageFive.SetActive(false);
            PageSix.SetActive(false);
        }
    }
    private void SwitchPageFour(){
        PageOne.SetActive(false);
        PageTwo.SetActive(false);
        PageThree.SetActive(false);
        PageFour.SetActive(true);
        PageFive.SetActive(false);
        PageSix.SetActive(false);
    }
    private void SwitchPageFive(){
        PageOne.SetActive(false);
        PageTwo.SetActive(false);
        PageThree.SetActive(false);
        PageFour.SetActive(false);
        PageFive.SetActive(true);
        PageSix.SetActive(false);
    }
    private void SwitchPageSix(){
        PageOne.SetActive(false);
        PageTwo.SetActive(false);
        PageThree.SetActive(false);
        PageFour.SetActive(false);
        PageFive.SetActive(false);
        PageSix.SetActive(true);
    }
}
