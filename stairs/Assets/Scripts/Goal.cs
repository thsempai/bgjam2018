using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {

    public int LemmingsGoal = 1;
    public string LevelToLoad;

    public void OnTriggerEnter(Collider other) {
        if (other.tag == "Lemming") {
            NPCManager npcManager = other.transform.GetComponent<NPCManager>();
            if (npcManager != null) {
                print("dipel");
                npcManager.Dispel();
                

            }
                LemmingsGoal--;
                print(LemmingsGoal);
                if (LemmingsGoal <= 0)
                 {
                    print("SceneChanger");
                    ChangeScene();
                 }



        }

    }
    void ChangeScene()
    {
        SceneManager.LoadScene(LevelToLoad);
    }


}
