using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour { 
	
	SpriteRenderer[] sprites;
	static int SceneIndex; //Jos muuttujan on staattinen se ei tuhoudu scenen vaihdon yhteydessä

	// Use this for initialization
	void Start () { //Starttiin ei saa laittaa staattisia muuttujia

		sprites = gameObject.GetComponentsInChildren<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Tab)) {
			for (int i = 0; i < sprites.Length; i++)
				sprites [i].enabled = !sprites [i].enabled; 
		}
	} 

	public void ChangeToLevel(int i) {
		SceneManager.LoadScene (i);
	}

	public void LoadNextLevel() {
		SceneIndex++;
		SceneManager.LoadScene (SceneIndex);
	}

	public void ReloadScene() {
		SceneManager.LoadScene (SceneIndex);
	}
}