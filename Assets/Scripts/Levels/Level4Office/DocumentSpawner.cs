using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Yarn.Unity;

public class DocumentSpawner : MonoBehaviour
{
    public GameObject DocumentUI, Document;

    // Start is called before the first frame update
    [YarnCommand("ResetDocument")]

    public void Reset()
    {

    }
    private void Awake()
    {
        Vector3 pos = new Vector3(DocumentUI.transform.position.x, DocumentUI.transform.position.y-20, DocumentUI.transform.position.z);
        GameObject SpawnedDocument = Instantiate(Document, pos, DocumentUI.transform.localRotation, transform);
    }
}
