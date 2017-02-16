using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour { 

	static int SceneIndex; //Jos muuttujan on staattinen se ei tuhoudu scenen vaihdon yhteydessä

	// Use this for initialization
	void Start () { //Starttiin ei saa laittaa staattisia muuttujia
	}

	// Update is called once per frame
	void Update () {

	} 

	public void ChangeToLevel(int i) {
		SceneManager.LoadScene (i);
	}

	public void LoadNextLevel() {
		SceneIndex++;
		SceneManager.LoadScene (SceneIndex);
	}
}