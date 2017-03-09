using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour { //Tänne voidaan myös siirtää GameOver ja Pause, GameControlista
//nappeja voi olla esim. showScore, showInventory, restartFromCheckpoint
	Button restartLevel;
	Button restartLevel1;
	LevelManager levels;

	// Use this for initialization
	void Start () {

		levels = GameObject.Find ("Levels").GetComponent<LevelManager> ();

		restartLevel = GameObject.Find ("RestartLevelButton").GetComponent<Button> ();
		restartLevel1 = GameObject.Find ("RestartLevelButton1").GetComponent<Button> ();

		restartLevel.onClick.AddListener (() => ReloadLevel ());
		restartLevel1.onClick.AddListener (() => ReloadLevel ());
	}
	
	// Update is called once per frame
	void Update () {
		
//		restartLevel.onClick.AddListener (() => levels.reloadLevel());
	}

	void ReloadLevel () {
		levels.ReloadScene ();
		Time.timeScale = 1;
	}
}
