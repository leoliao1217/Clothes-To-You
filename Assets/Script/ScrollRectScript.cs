using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectScript : MonoBehaviour
{

    private ScrollRect scrollRect;
    private bool mouseDown, buttonDown, buttonUp;
    // Use this for initialization
    void Start()
    {

        scrollRect = GetComponent<ScrollRect>();
    }

    // Update is called once per frame
    void Update()
    {

        if (mouseDown)
        {
            if (buttonDown)
            {
                Scrolldown();
            }
            else if (buttonUp)
            {
                Scrollup();
            }
        }
    }

    public void ButtonDownIsPressed()
    {

        mouseDown = true;
        buttonDown = true;
        Debug.Log("yes");
    }

    public void ButtonUpIsPressed()
    {

        mouseDown = true;
        buttonUp = true;
    }

    public void Scrolldown()
    {

        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
            buttonDown = false;
        }
        else
        {
            scrollRect.verticalNormalizedPosition -= 0.01f;
        }
    }

    public void Scrollup()
    {

        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
            buttonUp = false;
        }
        else
        {
            scrollRect.verticalNormalizedPosition += 0.01f;
        }
    }
}
