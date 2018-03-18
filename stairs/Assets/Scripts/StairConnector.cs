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

    private bool particles = false;

	// Use this for initialization
	void Start () {
        readyToCreateStairs = false;
        creatingStairs = false;
        particles = false;
	}
	
	// Update is called once per frame
	void Update () {
        bool oldParticles = particles;
        particles = isActive && connectedStairs == null;
        if (particles != oldParticles) {
            foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem>()) {
                if (particles) {
                    p.Play();
                } else {
                    p.Stop();
                }
            }
        }
    }

    public GameObject GetOwnWaypoint() {
        return gameObject.GetComponentInChildren<Waypoint>().gameObject;
    }
}
