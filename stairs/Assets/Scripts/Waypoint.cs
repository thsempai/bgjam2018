using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {

    public StairConnector connector;
    public GameObject nextWaypoint;
    private LevelManager manager;
    public int stay = 0;

    private List<NPCManagerSingle> clients;

    public GameObject GetNextWaypoint() {
        if (nextWaypoint != null) {
            return nextWaypoint;
        }
        else {
            try {
                if (connector.connectedStairs != null) {
                    if (connector == connector.connectedStairs.GetComponent<StairsCreation>().startConnector) {
                        return connector.connectedStairs.GetComponent<StairsCreation>().endConnector.GetOwnWaypoint();
                    }
                    else {
                        return connector.connectedStairs.GetComponent<StairsCreation>().startConnector.GetOwnWaypoint();
                    }
                }
            }
            catch (System.NullReferenceException) { Debug.LogWarning("no connector"); }
        }
        return gameObject;
    }
    public void Stay() {
        print("test du matin, chagrin");
        if (stay > 0) {
            manager.currentTarget = GetNextWaypoint().gameObject;
            if (manager.currentTarget != gameObject) {
                stay = 0;
            }
        }
    }
    public void OnTriggerEnter(Collider other) {
        if (other.tag == "Lemming" && other.GetComponent<NPCManagerSingle>().target == gameObject) {
            clients.Add(other.gameObject.GetComponent<NPCManagerSingle>());
        }
    }


    // Use this for initialization
    void Start() {
        clients = new List<NPCManagerSingle>();
    }

    // Update is called once per frame
    void Update() {
        //Stay();
        GameObject newWaypoint = GetNextWaypoint();
        if (newWaypoint != gameObject) {
            foreach (NPCManagerSingle c in clients) {
                c.target = newWaypoint;
            }
            clients.Clear();
        }

    }
}
