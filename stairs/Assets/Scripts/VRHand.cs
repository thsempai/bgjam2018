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

    public AudioSource breakSFX;
    public AudioSource placeSFX;

    public string side;

    public SteamVR_TrackedController controller;

    private bool readyToCreate;

	// Use this for initialization
	void Start () {
        isBusy = false;
        stairsBeingCreated = null;
        readyToCreate = false;
        state = HandState.Idle;
        foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem>()) {
            p.Play();
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetAxis("OpenVR" + side + "Trigger") > 0.5f) {
            if (state == HandState.CanStartBuilding && startConnector.isActive) {
                stairsBeingCreated = Instantiate(Resources.Load("StairsPrefab")) as GameObject;
                stairsBeingCreated.GetComponent<StairsCreation>().AttachStart(startConnector);
                placeSFX.Play();
                stairsBeingCreated.GetComponent<StairsCreation>().controllerIndex = (int)controller.controllerIndex;
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
            if (state == HandState.CanFinishBuilding && stairsBeingCreated.GetComponent<StairsCreation>().ValidateStairs() && endConnector.isActive) {
                stairsBeingCreated.GetComponent<StairsCreation>().AttachEnd(endConnector);
                stairsBeingCreated.GetComponent<StairsCreation>().controllerIndex = -1;
                placeSFX.Play();
                state = HandState.Idle;
            } else if (state == HandState.Building) {
                state = HandState.Idle;
                stairsBeingCreated.GetComponent<StairsCreation>().DestroyStairsVisibly(transform.position);
                breakSFX.Play();
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
            other.gameObject.GetComponent<StairsStep>().stairs.DestroyStairsVisibly(transform.position);
            breakSFX.Play();
        } else if (other.tag == "Lemming" && state == HandState.CanBreak) {
            other.gameObject.GetComponent<NPCManagerSingle>().Kill();
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
