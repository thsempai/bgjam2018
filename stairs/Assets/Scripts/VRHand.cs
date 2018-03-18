using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRHand : MonoBehaviour {

    public enum HandState { Idle, CanStartBuilding, Building, CanFinishBuilding, CanBreak };

    public bool isBusy;
    public HandState state;
    public GameObject stairsBeingCreated;
    public StairConnector startConnector;
    public StairConnector endConnector;

    public string side;

    private bool readyToCreate;

	// Use this for initialization
	void Start () {
        isBusy = false;
        stairsBeingCreated = null;
        readyToCreate = false;
        state = HandState.Idle;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetAxis("OpenVR" + side + "Trigger") > 0.5f) {
            if (state == HandState.CanStartBuilding) {
                stairsBeingCreated = Instantiate(Resources.Load("StairsPrefab")) as GameObject;
                stairsBeingCreated.GetComponent<StairsCreation>().AttachStart(startConnector);
                state = HandState.Building;
            }
            else if (state != HandState.Building && state != HandState.CanFinishBuilding) {
                state = HandState.CanBreak;
            }
        }

        if (state == HandState.Building || state == HandState.CanFinishBuilding) {
            stairsBeingCreated.GetComponent<StairsCreation>().endPos = transform.position;
        }

        if (Input.GetAxis("OpenVR" + side + "Trigger") < 0.1f) {
            if (state == HandState.CanFinishBuilding && stairsBeingCreated.GetComponent<StairsCreation>().ValidateStairs()) {
                stairsBeingCreated.GetComponent<StairsCreation>().AttachEnd(endConnector);
                state = HandState.Idle;
            } else if (state == HandState.Building) {
                state = HandState.Idle;
                stairsBeingCreated.GetComponent<StairsCreation>().DestroyStairsVisibly();
            } else if (state == HandState.CanBreak) {
                state = HandState.Idle;
            }
        }

    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "StairsConnector") {
            if (state == HandState.Idle) {
                state = HandState.CanStartBuilding;
                startConnector = other.gameObject.GetComponent<StairConnector>();
            } else if (state == HandState.Building && other.gameObject.GetComponent<StairConnector>() != startConnector) {
                state = HandState.CanFinishBuilding;
                endConnector = other.gameObject.GetComponent<StairConnector>();
            }
        } else if (other.tag == "StairsStep" && state == HandState.CanBreak) {
            other.gameObject.GetComponent<StairsStep>().stairs.DestroyStairsVisibly();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "StairsConnector") {
            if (state == HandState.CanStartBuilding) {
                state = HandState.Idle;
                startConnector = null;
            }
            else if (state == HandState.CanFinishBuilding) {
                state = HandState.Building;
                endConnector = null;
            }
        }
    }

}
