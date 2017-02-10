using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

	GameObject playerObject; 
	ButtonController rightButton;
	ButtonController leftButton;
	Vector3 playerLocation;
	Text points;
	Player player;
	int jumpHeight; //Kaikki tälläset pelaajalle omaiset "statsit" sirretään mahdollisesti Player luokkaan myöhemmin
	bool jumpCap;
	int jumpProgress;
//	Camera camera; 

	// Use this for initialization
	void Start () {
		points = GameObject.Find ("PointsText").GetComponent<Text> ();
		playerObject = GameObject.Find ("Player"); 
		playerLocation = playerObject.transform.position; 
		player = GameObject.Find("Player").GetComponent<Player>();
		jumpCap = false;
		jumpHeight = 5; //Hypyn max korkeus
		int jumpProgress = 0;
//		rightButton = GameObject.Find ("RightButton").GetComponent<ButtonController>(); 
//		leftButton = GameObject.Find ("LeftButton").GetComponent<ButtonController>(); Mahdollisia virtuaali nappeja varten

		
	}
		
	// Update is called once per frame
	void Update (){
		
		//Input.GetKey (KeyCode.nappi) niin kauan kun nappia painetaan niin suoritetaan komentoa
		//Input.GetKeyDown (KeyCode.nappi) kun nappia painetaan kerran niin suoritetaan komento
		//To do: viisto hyppy, disablee airstrafe

		if (Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.A) && jumpCap == false) {
			playerObject.transform.Translate (-50, 0, 0);
			Jump ();

		} else if (Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.D) && jumpCap == false) {
			playerObject.transform.Translate (50, 0, 0);
			Jump ();

		} else if (Input.GetKey (KeyCode.W) && jumpCap == false) { //kun W painetaan pohjassa ja jumpCap == false pelaaja hyppää 
			Jump ();
		}

		if (jumpHeight == 5) { //jumpHeight == 5 toistaiseksi, koska se on true enkä jaksa editoida. if:in sisään player.GetGrounded () == true jos halutaan disablee airstrafe

			if (Input.GetKey (KeyCode.D)) {
			playerObject.transform.Translate (5, 0, 0);

			} else if (Input.GetKey (KeyCode.A)) {
			playerObject.transform.Translate (-5, 0, 0);
			}
		}

		points.text = "Points: " + player.GetPoints (); //Pitää näytöllä lukua kerätyistä kolikoista 
	} 

	void Jump() {
		//Liikutetaan pelaajaa ylöspäin kunnes saavutetaan hypyn lakipiste, jolloin pelaaja lakkaa liikkumasta ylös. 
		playerObject.transform.Translate (0, 50, 0); //Hypyn "räjähtävyys"
		player.SetGrounded(false); // 
		jumpProgress++;
		if (jumpProgress == jumpHeight) { //Hypyn lakipiste saavutettu
			jumpCap = true; //Pelaaja ei voi hypätä uudelleen kunnes jumpCap == false
			jumpProgress = 0;
		}
	}

	public void SetJumpCap(bool b) { 
		this.jumpCap = b;
	}
}