using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private ScrollRectScript scrollRectScript;

    [SerializeField]
    private bool isDownButton;

    // Use this for initialization
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isDownButton)
        {
            scrollRectScript.ButtonDownIsPressed();
        }
        else
        {
            scrollRectScript.ButtonUpIsPressed();
        }
    }
}
