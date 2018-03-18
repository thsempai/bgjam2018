using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unlockstair : MonoBehaviour {

    public Animator anim;
    public LevelManager manager;

    public GameObject oldTarget;
    public GameObject newTarget;

    public StairConnector toActivate;

    bool on = false;
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
        if (!on) {
            if (other.tag == "Lemming") {
                ActiveStairs();
                }
            }
       
    }

    void ActiveStairs()
    {
        on = true;
        //Debug.Log("More Stairs");
        //anim.enabled = true;
        //oldTarget = newTarget;
        //newTarget = manager.currentTarget;
        //manager.currentTarget = oldTarget;
        toActivate.isActive = true;
        }
    }
