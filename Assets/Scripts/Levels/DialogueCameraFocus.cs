using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Cinemachine;

public class DialogueCameraFocus : MonoBehaviour
{
    public CinemachineVirtualCamera dialogueCam;
    public CinemachineVirtualCamera mainCam;

    [YarnCommand("PanToObj")]
    public void PanToObj(string[] parameters){

        //copied from emmanuel's code from MovePlayer

        // use better variable names for the parameters
        string locationName = parameters[0];
        string[] offset = new string[parameters.Length - 1];
        Array.Copy(parameters, 1, offset, 0, offset.Length);

        // get the GameObject that the location is referring to
        GameObject location = GameObject.Find(locationName);

        // if the location is null (i.e. the GameObject was not found), log a warning and do no movement
        if (location == null)
        {
            Debug.LogWarningFormat("MovePlayer: Could not find GameObject {0}. Player movement was not done.", locationName);
        }

        // calculate the target position + offset

        // target is first based off the location
        Vector3 targetPosition = location.transform.position;

        // turn the offset (string array) into floats
        
        if (offset.Length > 3)
        {
            Debug.LogWarning("MovePlayer: Too many arguments, only considering the x, y, z arguments");
        }

        // for-loop up to three offset arguments
        for (int i = 0; i < Mathf.Min(offset.Length, 3); i++)
        {
            try
            {
                // convert string to float and apply offset
                targetPosition[i] += float.Parse(offset[i]);
            }
            catch (System.FormatException exc)
            {
                // array for debug logging purposes
                char[] offsetChar = new char[] { 'x', 'y', 'z' };
                Debug.LogWarningFormat("MovePlayer: Failed to apply offset \"{0}\" for {1} position.", offset[i], offsetChar[i]);
            }
        }

        //set position
        dialogueCam.transform.position = targetPosition;
        //enable camera
        dialogueCam.enabled = true;
        //disable main camera
        mainCam.enabled = false;
    }

    [YarnCommand("ChangeRotation")]
    public void ChangeRotation(string[] parameters){

        string[] offset = new string[parameters.Length];
        Array.Copy(parameters, 0, offset, 0, offset.Length);

        Vector3 targetRotation = dialogueCam.transform.rotation.eulerAngles;

        if (offset.Length > 3)
        {
            Debug.LogWarning("MovePlayer: Too many arguments, only considering the x, y, z arguments");
        }

        // for-loop up to three offset arguments
        for (int i = 0; i < Mathf.Min(offset.Length, 3); i++)
        {
            try
            {
                // convert string to float and apply offset
                targetRotation[i] += float.Parse(offset[i]);
            }
            catch (System.FormatException exc)
            {
                // array for debug logging purposes
                char[] offsetChar = new char[] { 'x', 'y', 'z' };
                Debug.LogWarningFormat("MovePlayer: Failed to apply offset \"{0}\" for {1} position.", offset[i], offsetChar[i]);
            }
        }

        dialogueCam.transform.rotation = Quaternion.Euler(targetRotation);
    }

    [YarnCommand("UnRotate")]
    public void UnRotate(){
        dialogueCam.transform.rotation = Quaternion.Euler(new Vector3(45, 0, 0));
    }

    [YarnCommand("UnPanObj")]
    public void UnPanObj(){
        //disable cam
        dialogueCam.enabled = false;
        //main cam
        mainCam.enabled = true;
    }

    [YarnCommand("FollowObject")]
    public void FollowObj(string[] parameters){

        // use better variable names for the parameters
        string locationName = parameters[0];

        // get the GameObject that the location is referring to
        GameObject location = GameObject.Find(locationName);

        // if the location is null (i.e. the GameObject was not found), log a warning and do no movement
        if (location == null)
        {
            Debug.LogWarningFormat("MovePlayer: Could not find GameObject {0}. Player movement was not done.", locationName);
        }

        dialogueCam.Follow = location.transform;

        dialogueCam.enabled = true; 

        mainCam.enabled = false;

        // Debug.Log("penis");
    }

    [YarnCommand("UnFollowObject")]
    public void UnFollowObj(){

        dialogueCam.Follow = null;

        dialogueCam.enabled = false; 

        mainCam.enabled = true;
    }
}
