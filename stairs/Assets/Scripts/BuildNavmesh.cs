using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshSurface))]
public class BuildNavmesh : MonoBehaviour {
    public NavMeshSurface surface;
    // Use this for initialization
    void Start () {
        surface = GetComponent<NavMeshSurface>();
        }
	
	// Update is called once per frame
	void Update () {

        }
}
