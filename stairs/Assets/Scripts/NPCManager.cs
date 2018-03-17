using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCManager : MonoBehaviour {
    // Use this for initialization

    public GameObject target;
    NavMeshAgent agent;

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
        get { return agent.speed; }
        set { agent.speed = value; }
        }

    public float StoppingDistance{
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
            if(targetManager != null) {
                if (targetManager.isStopped) {
                    isStopped = true;
                    }
                }
            }
        }

    public void Follow(GameObject newTarget) {
        target = newTarget;
        }

    }
