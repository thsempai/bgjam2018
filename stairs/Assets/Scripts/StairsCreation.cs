using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsCreation : MonoBehaviour {

    public Vector3 startPos;
    public Vector3 endPos;

    public bool debugMode;

    public GameObject[] stepsPrefabs;
    public Vector3 stepsDimensions;

    private List<GameObject> steps;
    private bool dirty;
    private Vector3 oldStartPos;
    private Vector3 oldEndPos;

	// Use this for initialization
	void Start () {
        steps = new List<GameObject>();
        dirty = true;
	}

    // Update is called once per frame
    void Update() {
        if (debugMode) {
            Debug.DrawLine(startPos, endPos, Color.green);
        }

        if (oldStartPos != startPos || oldEndPos != endPos) {
            dirty = true;
        }

        if (dirty) {
            foreach (GameObject step in steps) {
                Destroy(step);
            }

            float length = Vector3.Magnitude(endPos - startPos);
            float stairHeight = (endPos - startPos).y;
            int numberOfSteps = (int)Mathf.Ceil(stairHeight / stepsDimensions.y);
            if (stairHeight - numberOfSteps * stepsDimensions.y > stepsDimensions.y / 2.0f) {
                numberOfSteps++;
            }
            float yScale = stairHeight / (numberOfSteps * stepsDimensions.y);
            print(numberOfSteps);

            float stairLength = new Vector2((endPos - startPos).x, (endPos - startPos).z).magnitude;

            float zScale = stairLength / (numberOfSteps * stepsDimensions.z);

            Vector3 orientation = new Vector3((endPos - startPos).x, 0, (endPos - startPos).z).normalized;

            for (int i = 0; i < numberOfSteps; i++) {
                GameObject newStep = Instantiate(stepsPrefabs[0], startPos + ((float)i / numberOfSteps) * (endPos - startPos), Quaternion.LookRotation(orientation)) as GameObject;
                newStep.transform.localScale = new Vector3(newStep.transform.localScale.x, newStep.transform.localScale.y * yScale, newStep.transform.localScale.z * zScale);
                steps.Add(newStep);
            }

            oldEndPos = endPos;
            oldStartPos = startPos;
            dirty = false;
        }

    }
                    
}
