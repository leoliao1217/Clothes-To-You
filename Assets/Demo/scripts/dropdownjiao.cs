using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dropdownjiao : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }
    public void OnClick()
    {
        string name = Convert.ToString(transform.GetComponent<Dropdown>().value);
        
            ChangeFoot.instance.ChangeFeet("00"+name);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
