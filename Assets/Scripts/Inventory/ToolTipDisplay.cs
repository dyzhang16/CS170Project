using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipDisplay : MonoBehaviour //https://www.youtube.com/watch?v=pg4-7aSf_Co
{
    [SerializeField] private GameObject popupCanvasObject;
    [SerializeField] private RectTransform popupObject;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float padding;

    private Canvas popupCanvas;
    private void Awake()
    {
        popupCanvas = popupCanvasObject.GetComponent<Canvas>();
    }

    // Update is called once per frame
    private void Update()
    {
        FollowCursor();
        /*if (!popupCanvasObject.activeSelf) { return; } //Does not work unless Canvas is active

        Vector3 newPos = Input.mousePosition;
        popupObject.transform.position = newPos;*/
    }
    private void FollowCursor()
    {
        if (!popupCanvasObject.activeSelf) { return; } //Does not work unless Canvas is active

        Vector3 newPos = Input.mousePosition + offset;
        newPos.z = 0f;
        float rightEdgeToScreenEdgeDistance = Screen.width - (newPos.x + popupObject.rect.width * popupCanvas.scaleFactor / 2) - padding;

        if (rightEdgeToScreenEdgeDistance < 0)
        {
            newPos.x += rightEdgeToScreenEdgeDistance;
        }
        float leftEdgeToScreenEdgeDistance = 0 - (newPos.x - popupObject.rect.width * popupCanvas.scaleFactor / 2) - padding;

        if (leftEdgeToScreenEdgeDistance > 0)
        {
            newPos.x += leftEdgeToScreenEdgeDistance;
        }
        float topEdgeToScreenEdgeDistance = Screen.height - (newPos.y + popupObject.rect.height * popupCanvas.scaleFactor) - padding;

        if (topEdgeToScreenEdgeDistance < 0)
        {
            newPos.y += topEdgeToScreenEdgeDistance;
        }
/*        float bottomEdgeToScreenEdgeDistance = 0 - (newPos.y - popupObject.rect.height * popupCanvas.scaleFactor) - padding;

        if (bottomEdgeToScreenEdgeDistance < 0)
        {
            newPos.y += bottomEdgeToScreenEdgeDistance;
        }
        popupObject.transform.position = newPos;*/
    }
    public void DisplayInfo(Item item)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(item.DisplayInfo());

        infoText.text = builder.ToString();
        popupCanvasObject.SetActive(true);
        LayoutRebuilder.ForceRebuildLayoutImmediate(popupObject);
    }
    public void HideInfo()
    {
        popupCanvasObject.SetActive(false);
    }
}
