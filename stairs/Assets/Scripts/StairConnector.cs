using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairConnector : MonoBehaviour {

    public bool isActive;
    public GameObject connectedStairs;

    private bool readyToCreateStairs;
    private bool creatingStairs;
    private StairsCreation stairsScript;
    private VRHand hand;

	// Use this for initialization
	void Start () {
        readyToCreateStairs = false;
        creatingStairs = false;
	}
	
	// Update is called once per frame
	void Update () {

	}


}
