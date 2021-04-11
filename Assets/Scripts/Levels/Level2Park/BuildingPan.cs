using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Cinemachine;


public class BuildingPan : MonoBehaviour
{
    public CinemachineVirtualCamera buildingCam;
    public CinemachineVirtualCamera mainCam;

    [YarnCommand("Pan")]
    public void Pan(){
        buildingCam.transform.position = this.transform.position + new Vector3(0, 65, -35);
        buildingCam.enabled = true;
        mainCam.enabled = false;
    }

    [YarnCommand("UnPan")]
    public void UnPan(){
        buildingCam.enabled = false;
        mainCam.enabled = true;
    }

    [YarnCommand("SuperPan")]
    public void SuperPan(){
        buildingCam.transform.position = this.transform.position + new Vector3(0, 150, -80);
        buildingCam.enabled = true;
        mainCam.enabled = false;
    }
}
