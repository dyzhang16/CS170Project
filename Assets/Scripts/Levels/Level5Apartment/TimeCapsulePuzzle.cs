using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class TimeCapsulePuzzle : MonoBehaviour
{
    public int num1Solved = 6;
    public int num2Solved = 5;
    public int num3Solved = 9;
    public int num4Solved = 3;

    public int num1 = 1, num2 = 1, num3 = 1, num4 = 1;

    public Text num1Text;
    public Text num2Text;
    public Text num3Text;
    public Text num4Text;

    public DialogueRunner dia;
    public HidePuzzle exit;

    public Animator num1Animator;
    public Animator num2Animator;
    public Animator num3Animator;
    public Animator num4Animator;

    public void AddNum(string numText){
        switch(numText){
            case "num1":
                num1++;
                num1 = num1%10;

                if (num1 == 0)
                    num1++;

                num1Text.text = "" + num1;
                break;
            case "num2":
                num2++;
                num2 = num2%10;

                if (num2 == 0)
                    num2++;

                num2Text.text = "" + num2;
                break;
            case "num3":
                num3++;
                num3 = num3%10;

                if (num3 == 0)
                    num3++;

                num3Text.text = "" + num3;
                break;
            case "num4":
                num4++;
                num4 = num4%10;

                if (num4 == 0)
                    num4++;

                num4Text.text = "" + num4;
                break;
        }
    }

    public void SubNum(string numText){
        switch(numText){
            case "num1":
                num1--;
                num1 = num1%10;

                if (num1 == 0)
                    num1 = 9;

                num1Text.text = "" + num1;
                break;
            case "num2":
                num2--;
                num2 = num2%10;

                if (num2 == 0)
                    num2 = 9;

                num2Text.text = "" + num2;
                break;
            case "num3":
                num3--;
                num3 = num3%10;

                if (num3 == 0)
                    num3 = 9;

                num3Text.text = "" + num3;
                break;
            case "num4":
                num4--;
                num4 = num4%10;

                if (num4 == 0)
                    num4 = 9;

                num4Text.text = "" + num4;
                break;
        }
    }

    public void checkTimeCapsule(){
        if (num1 == num1Solved && num2 == num2Solved && num3 == num3Solved && num4 == num4Solved){
            exit.Hide();
            dia.StartDialogue("done_with_puzzle");
            GameManager.instance.timeCapsule = 1;
        }
    }

    public void animateNum(int num){
        switch(num){
            case 1:
                num1Animator.SetTrigger("ChangeNum");
                break;
            case 2:
                num2Animator.SetTrigger("ChangeNum");
                break;
            case 3:
                num3Animator.SetTrigger("ChangeNum");
                break;
            case 4:
                num4Animator.SetTrigger("ChangeNum");
                break;
        }
    }

    public void animateReverseNum(int num){
        switch(num){
            case 1:
                num1Animator.SetTrigger("ChangeNumReverse");
                break;
            case 2:
                num2Animator.SetTrigger("ChangeNumReverse");
                break;
            case 3:
                num3Animator.SetTrigger("ChangeNumReverse");
                break;
            case 4:
                num4Animator.SetTrigger("ChangeNumReverse");
                break;
        }
    }
}
