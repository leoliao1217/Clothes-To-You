using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonjiao : MonoBehaviour
{

    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }
    
    public void OnClick()
    {
        string name = this.gameObject.name;
        
        if(name == "original")
        {
            ChangeFoot.instance.ChangeFeet("001");
            ChangeFoot.instance.Changecloth("001");
        }
        else
        {
            ChangeFoot.instance.ChangeFeet(name);
        }
    }

    
    // Update is called once per frame
    void Update()
    {

    }
}
