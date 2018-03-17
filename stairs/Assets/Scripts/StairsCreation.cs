using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsCreation : MonoBehaviour {

    public Vector3 startPos;
    public Vector3 endPos;

    public bool debugMode;

    public StairConnector startConnector;
    public StairConnector endConnector;

    public GameObject[] stepsPrefabs;
    public Vector3 stepsDimensions;

    private List<GameObject> steps;
    private bool dirty;
    private Vector3 oldStartPos;
    private Vector3 oldEndPos;

    private int numberOfSteps;
    private float totalLength;
    private float length;
    private float height;
    private float yScale;
    private float zScale;
    private Vector3 trajectory;
    private Vector3 orientation;

	// Use this for initialization
	void Start () {
        steps = new List<GameObject>();
        dirty = true;
	}

    public void AttachStart(StairConnector connector) {
        startConnector = connector;
        startPos = connector.transform.position;
    }

    public void AttachEnd(StairConnector connector) {
        endConnector = connector;
        endPos = connector.transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (debugMode) {
            Color lineColor = Color.red;
            if (ValidateStairs()) {
                lineColor = Color.green;
            }
            Debug.DrawLine(startPos, endPos, lineColor);
        }

        if (oldStartPos != startPos || oldEndPos != endPos) {
            dirty = true;
        }

        if (dirty) {
            DestroyStairs();

            trajectory = endPos - startPos;
            orientation = new Vector3(trajectory.x, 0, trajectory.z);
            totalLength = trajectory.magnitude;
            height = Mathf.Abs(trajectory.y);
            length = orientation.magnitude;

            numberOfSteps = (int)Mathf.Ceil(height / stepsDimensions.y);
            if (height - numberOfSteps * stepsDimensions.y > stepsDimensions.y / 2.0f) {
                numberOfSteps++;
            }

            yScale = height / (numberOfSteps * stepsDimensions.y);
            zScale = length / (numberOfSteps * stepsDimensions.z);

            float yOffset = 0;
            if (trajectory.y < 0) {
                yOffset = -stepsDimensions.y * yScale;
            }

            if (ValidateStairs()) {
                for (int i = 0; i < numberOfSteps; i++) {
                    GameObject newStep = Instantiate(stepsPrefabs[0], startPos + ((float)i / numberOfSteps) * trajectory + new Vector3(0, yOffset, 0), Quaternion.LookRotation(orientation)) as GameObject;
                    newStep.transform.localScale = new Vector3(newStep.transform.localScale.x, newStep.transform.localScale.y * yScale, newStep.transform.localScale.z * zScale);
                    steps.Add(newStep);
                    newStep.GetComponent<StairsStep>().stairs = this;
                }
            }

            oldEndPos = endPos;
            oldStartPos = startPos;
            dirty = false;
        }

    }

    public bool ValidateStairs() {
        return (zScale > 0.25 && totalLength < 50);
    }
                
    public void DestroyStairs() {
        foreach (GameObject step in steps) {
            Destroy(step);
        }
        steps.Clear();
    }

    public void DestroyStairsVisibly() {
        foreach (GameObject step in steps) {
            print("Destroying step");
            step.GetComponent<Rigidbody>().isKinematic = false;
            Destroy(step, 3f);
        }
        if (gameObject) {
            Destroy(gameObject);
        }
        
    }
}
