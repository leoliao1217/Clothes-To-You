using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{

    public static PopUp instance;
    public string name;

    // Use this for initialization
    void Start()
    {

        instance = this;
        name = this.gameObject.name;
        Shaded();
        if (name == "CatalogPlace")
        {
            Popup();
        } else if (name == "UnionRect"){
            Popup();
        }

    }

    public CanvasGroup CanvasChange;

    public void Awake()
    {
        CanvasChange = GetComponent<CanvasGroup>();
    }

    public void Popup()
    {
        CanvasChange.alpha = 1f;
        CanvasChange.interactable = true;
        CanvasChange.blocksRaycasts = true;

    }

    public void Shaded()
    {
        CanvasChange.alpha = 0f;
        CanvasChange.interactable = false;
        CanvasChange.blocksRaycasts = false;
    }

}

