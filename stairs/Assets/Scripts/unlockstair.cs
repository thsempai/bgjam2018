using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unlockstair : MonoBehaviour {

    public Animator anim;

	// Use this for initialization
	void Start ()
    {
        anim = this.GetComponent<Animator>();
        anim.enabled = false;
        

	}
	
	// Update is called once per frame
	void Update ()
    { 
        
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Lemming")
        {
            ActiveStairs();
            
        }
    }
    void ActiveStairs()
    {
        Debug.Log("More Stairs");
        anim.enabled = true;
       
    }
}
