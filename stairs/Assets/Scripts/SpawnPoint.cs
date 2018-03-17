using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LocalNavMeshBuilder))]
public class SpawnPoint : MonoBehaviour {

    public int number = 7;
    public float distanceBetweenNPC = 1f;
    public float NPCSpeed = 5f;
    public GameObject NPCType;
    public GameObject target;

    GameObject lastest;
    LocalNavMeshBuilder builder;

	// Use this for initialization
	void Start () {
		if(NPCType == null) {
            Debug.LogError("NPC Type is mandatory");
            }
        builder = GetComponent<LocalNavMeshBuilder>();
	}
	
	// Update is called once per frame
	void Update () {
        if (number > 0) {

            if (lastest == null) {
                CloneOne();
                builder.m_Tracked = lastest.transform;
                }
            else {
                if (Vector3.Distance(transform.position, lastest.transform.position) >= distanceBetweenNPC) {
                    CloneOne();
                    }
                }
            }
        }

    private void CloneOne() {
        GameObject clone = Instantiate(NPCType);
        clone.SetActive(true);
        clone.transform.position = transform.position;
        NPCManager cloneManager = clone.GetComponent<NPCManager>();
        if (lastest == null) {
            cloneManager.Follow(target);
            }
        else {
            cloneManager.Follow(lastest);
            cloneManager.StoppingDistance = distanceBetweenNPC;
            }

        cloneManager.speed = NPCSpeed;
        lastest = clone;
        number--;
        }
}
