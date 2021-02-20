using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class OfficeFriend : MonoBehaviour
{
    public GameObject friend;
    [YarnCommand("Move")]
    public void Move()
    {
        friend.transform.position = new Vector3(0,0,0);
    }
}
