using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoBoy : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DemoChangeBoy()
    {

        ChangeFoot.instance.Changecloth("shen2-17");
        ChangeFoot.instance.ChangeFeet("jiao2-10");

    }
}
