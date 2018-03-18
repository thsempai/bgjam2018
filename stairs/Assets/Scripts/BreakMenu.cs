using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakMenu : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "VRHand")
        {
            Debug.Log("ButtonIsBroke");
            ChangeScene();
            Destroy(other.gameObject);
        }
    }
    void ChangeScene()
    {

    }
}
