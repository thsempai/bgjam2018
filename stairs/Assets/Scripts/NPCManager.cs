using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCManager : MonoBehaviour {
    // Use this for initialization

    public GameObject target;
    NavMeshAgent agent;
    public LevelManager manager;

    public bool isAtGoal {
        get { return Vector3.Distance(transform.position, target.transform.position) <= agent.stoppingDistance + agent.radius; }
    }

    public bool isStopped {
        get {
            if (target == null || agent.isStopped) {
                return true;
            }
            return Vector3.Distance(transform.position, target.transform.position) <= agent.stoppingDistance + agent.radius;
        }
        set { agent.isStopped = value; }
    }

    public float speed {
        get { return GetComponent<NavMeshAgent>().speed; }
        set { GetComponent<NavMeshAgent>().speed = value; }
        }

    public float StoppingDistance {
        get { return agent.stoppingDistance; }
        set {
            GetComponent<NavMeshAgent>().stoppingDistance = value;
            }
        }

    void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {
        if (target != null) {
            agent.SetDestination(target.transform.position);
            NPCManager targetManager = target.GetComponent<NPCManager>();
            if (targetManager != null) {
                isStopped = targetManager.isStopped;
            } else {
                Follow(manager.currentTarget);
                //if(agent.pathStatus != NavMeshPathStatus.PathComplete) {
                //    isStopped = true;
                //    }
                //else {
                //    isStopped = false;
                //    }
            }

        }
        else {
            target = manager.currentTarget;
            isStopped = false;
            //agent.stoppingDistance = 0;
            }

    }

    public void Follow(GameObject newTarget) {
        target = newTarget;
        }

    public void Kill() {
        GetComponent<Rigidbody>().useGravity = true;
        Destroy(gameObject, 3f);
        }

    public void Dispel(){
        Destroy(gameObject, 1f);
    }

}
