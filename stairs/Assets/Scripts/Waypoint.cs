using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

    public StairConnector connector;
    public GameObject nextWaypoint;
    private LevelManager manager;

    public GameObject GetNextWaypoint() {
        if (nextWaypoint != null) {
            return nextWaypoint;
        }
        else if (connector.connectedStairs != null) {
            if (connector == connector.connectedStairs.GetComponent<StairsCreation>().startConnector) {
                return connector.connectedStairs.GetComponent<StairsCreation>().endConnector.GetOwnWaypoint();
            } else {
                return connector.connectedStairs.GetComponent<StairsCreation>().startConnector.GetOwnWaypoint();
            }
        }

        return gameObject;
    }

    public void OnTriggerStay(Collider other) {
        if (other.tag == "Lemming" && manager.currentTarget == gameObject) {
            manager.currentTarget = GetNextWaypoint().gameObject;
        }
    }

    // Use this for initialization
    void Start () {
        manager = GameObject.Find("Managers").GetComponent<LevelManager>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
