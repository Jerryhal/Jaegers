using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//Jotta spritet vaihtuu oikein pelaajan vaihdon yhteydessä varmista,
//että unityn puolella kaikki vaihtuvat gameobjectit on Levels gameobjectin sisällä
//ja Patient maailman spritet on enabloitu ja thief maailman spritet on disabloitu.

public class LevelManager : MonoBehaviour { 
	
	SpriteRenderer[] sprites;
	static int SceneIndex; //Jos muuttujan on staattinen se ei tuhoudu scenen vaihdon yhteydessä

	// Use this for initialization
	void Start () { //Starttiin ei saa laittaa staattisia muuttujia
	}

	// Update is called once per frame
	void Update () {
		sprites = gameObject.GetComponentsInChildren<SpriteRenderer>();

	} 

	public void PlayerSwitch() { //Vaihtaa spritet käytössä olevan pelaajan mukaan   
		for (int i = 0; i < sprites.Length; i++) { //Käy läpi jokaisen LevelManager scriptin gameobjectille alistetun gameobjectin spriten
				sprites [i].enabled = !sprites [i].enabled;  //Jokainen läpi käyty sprite enabloituu mikäli se oli disabloitu ja jokainen enabloitu disabloituu 
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