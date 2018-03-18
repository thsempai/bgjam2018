using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTitle : MonoBehaviour {

   public float count = 7;
    public string LevelToLoad;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        count -= Time.deltaTime ;
        if(count <= 0)
        {
            SceneManager.LoadScene(LevelToLoad);
        }
	}
}
