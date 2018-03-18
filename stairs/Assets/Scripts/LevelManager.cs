using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public GameObject currentTarget;
    public GameObject finalTarget;
    SpawnPoint spawnPoint;

	// Use this for initialization
	void Start () {
        GameObject goSpawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        if(goSpawnPoint == null) {
            Debug.LogError("Spawn point not found!");
            }
        else {
            spawnPoint = goSpawnPoint.GetComponent<SpawnPoint>();
            if(spawnPoint == null) {
                Debug.LogError("Spawn point component not found!");
                }
            spawnPoint.manager = this;
            }
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("PressurePlate")) {
            go.GetComponent<unlockstair>().manager = this;
            }
	}
	
	// Update is called once per frame
	void Update () {
        spawnPoint.target = currentTarget;
    }
}
