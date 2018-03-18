using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LocalNavMeshBuilder))]
public class SpawnPointSingle : MonoBehaviour {

    public float frequency = 2f;
    public float NPCSpeed = 5f;
    public GameObject NPCType;
    public GameObject target;
    public LevelManager manager;

    private float timer;
    private bool first = true;

    GameObject lastest;
    LocalNavMeshBuilder builder;

	// Use this for initialization
	void Start () {
		if(NPCType == null) {
            Debug.LogError("NPC Type is mandatory");
            }
        builder = GetComponent<LocalNavMeshBuilder>();
        timer = frequency;
	}
	
	// Update is called once per frame
	void Update () {
        if (timer > 0) {
            timer -= Time.deltaTime;
        }
        else {
            
            if (first) {
                builder.m_Tracked = CloneOne();
            } else {
                CloneOne();
            }
            timer = frequency;
        }

    }

    private Transform CloneOne() {
        GameObject clone = Instantiate(NPCType);
        clone.SetActive(true);
        clone.transform.position = transform.position;
        NPCManagerSingle cloneManager = clone.GetComponent<NPCManagerSingle>();
        //cloneManager.manager = manager;

        cloneManager.target = target;
        cloneManager.speed = NPCSpeed;

        return clone.transform;
    }
}
