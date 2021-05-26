using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Cinemachine;

public class PanReceipt : MonoBehaviour
{
    [YarnCommand("PanReceipt")]
    public void ReceiptPan()
    {
        StartCoroutine(PanReceiptDelay());
    }
    public CinemachineVirtualCamera receiptCam;
    // Start is called before the first frame update
    IEnumerator PanReceiptDelay()
    {        
        receiptCam.enabled = true;
        yield return new WaitForSeconds(2);
        receiptCam.enabled = false;
    }
}
