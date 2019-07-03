using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoGirl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DemoChangeGirl(){
        
        ChangeFoot.instance.Changecloth("shen3-13");
        ChangeFoot.instance.ChangeFeet("jiao3-9");

    }
}
