using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    public void OnTriggerEnter(Collider other) {
        if (other.tag == "Lemming") {
            NPCManager npcManager = other.transform.GetComponent<NPCManager>();
            if (npcManager != null) {
                print("dipel");
                npcManager.Dispel();
            }
        }

    }
}
