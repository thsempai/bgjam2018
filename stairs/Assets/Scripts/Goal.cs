using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {

    public int LemmingsGoal = 1;
    public string LevelToLoad;

    public void OnTriggerEnter(Collider other) {
        if (other.tag == "Lemming") {
            NPCManagerSingle npcManager = other.transform.GetComponent<NPCManagerSingle>();
            if (npcManager != null) {
                print("dipel");
                npcManager.Dispel();
                

            }
                LemmingsGoal--;
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
