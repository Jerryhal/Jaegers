using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

	GameObject patientPlayerObject; 
	GameObject thiefPlayerObject; 
	GameObject currentPlayerObject;
	ButtonController rightButton;
	ButtonController leftButton;
	Vector3 patientPlayerLocation;
	Text points;
	Player patientPlayer;
	Player currentPlayer;
	Player thiefPlayer;
	int jumpHeight; //Kaikki tälläset pelaajalle omaiset "statsit" sirretään mahdollisesti Player luokkaan myöhemmin
	bool jumpCap;
	int jumpProgress;
	Camera camera;
	Vector3 offset;
	Canvas gameOver;
	Canvas pauseCanvas;
	Text GameOverText;


	// Use this for initialization
	void Start () {
		gameOver = GameObject.Find ("GameOverCanvas").GetComponent<Canvas> ();
		pauseCanvas = GameObject.Find ("PauseCanvas").GetComponent<Canvas> ();
		points = GameObject.Find ("PointsText").GetComponent<Text> ();
		patientPlayerObject = GameObject.Find ("PatientPlayer"); 
		thiefPlayerObject = GameObject.Find ("ThiefPlayer"); 
		patientPlayerLocation = patientPlayerObject.transform.position;
		patientPlayer = GameObject.Find("PatientPlayer").GetComponent<Player>();
		thiefPlayer = GameObject.Find("ThiefPlayer").GetComponent<Player>();
		jumpCap = false;
		pauseCanvas.enabled = false;
		jumpHeight = 30; //Hypyn max korkeus
		int jumpProgress = 0;
		camera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		currentPlayer = patientPlayer;
		currentPlayerObject = patientPlayerObject;
		offset = camera.transform.position - currentPlayerObject.transform.position;
//		rightButton = GameObject.Find ("RightButton").GetComponent<ButtonController>(); 
//		leftButton = GameObject.Find ("LeftButton").GetComponent<ButtonController>(); Mahdollisia virtuaali nappeja varten

		
	}
		
	// Update is called once per frame

	void Update (){
		
		camera.transform.position = currentPlayerObject.transform.position + offset; //Kamera seuraa pelaajaa

		//Input.GetKey (KeyCode.nappi) niin kauan kun nappia painetaan niin suoritetaan komentoa
		//Input.GetKeyDown (KeyCode.nappi) kun nappia painetaan kerran niin suoritetaan komento

		if (gameOver.enabled == false && pauseCanvas.enabled == false) { //Jos pelaaja ei ole game over tai pause screenissä niin voi liikkua

			if (Input.GetKeyUp (KeyCode.W)) { //Jos W nostetaan pohjasta, jumpCap = true -> Pelaaja ei voi leijua spämmäämällä W:tä
				jumpCap = true;

			} else if (Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.A) && jumpCap == false) { //Viisto hyppy vasemmalle
				if (currentPlayer.Facing () == false) {
					currentPlayerObject.transform.Translate (30, 0, 0);
				} else {
					Flip ();
				}
				Jump ();

			} else if (Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.D) && jumpCap == false) { //Viisto hyppy oikealle
				if (currentPlayer.Facing () == true) {
					currentPlayerObject.transform.Translate (30, 0, 0);
				} else {
					Flip ();
				}
				Jump ();

			} else if (Input.GetKey (KeyCode.W) && jumpCap == false) { //kun W painetaan pohjassa ja jumpCap == false pelaaja hyppää 
				Jump ();

			} else if (Input.GetKey (KeyCode.D)) { //liiku oikealle
				if (currentPlayer.Facing () == true) {
					currentPlayerObject.transform.Translate (10, 0, 0);
				} else {
					Flip ();
				}

			} else if (Input.GetKey (KeyCode.A)) { //Liiku vasemmalle
				if (currentPlayer.Facing () == false) {
					currentPlayerObject.transform.Translate (10, 0, 0);
				} else {
					Flip ();
				}

			} else if (Input.GetKeyDown (KeyCode.Tab)) {//Tab vaihtaa hahmoa 
				if (currentPlayer == patientPlayer) {
					currentPlayer = thiefPlayer;
					currentPlayerObject = thiefPlayerObject;
				} else {
					currentPlayer = patientPlayer;
					currentPlayerObject = patientPlayerObject;
				}

			}
		}
		if (Input.GetKeyDown (KeyCode.Escape)  && gameOver.enabled == false) { //Esc Pausee pelin 
				Pause ();
		}

		if (currentPlayer.GetHealth () <= 0) {
			GameOver ();
				}
		points.text = "Points: " + (thiefPlayer.GetPoints() + patientPlayer.GetPoints()); //Pitää näytöllä lukua kerätyistä kolikoista
			}

	void Jump() { //Hyppy
		//Liikutetaan pelaajaa ylöspäin kunnes saavutetaan hypyn lakipiste, jolloin pelaaja lakkaa liikkumasta ylös.
		currentPlayerObject.transform.Translate (0, 20, 0); //Hypyn "räjähtävyys"

		currentPlayer.SetGrounded (false); //Pelaaja ei ole enää maassa 
			jumpProgress++;
		if (jumpProgress == jumpHeight) { //Hypyn lakipiste saavutettu
				jumpCap = true; //Pelaaja ei voi hypätä uudelleen kunnes jumpCap == false
				jumpProgress = 0; 
			}
		}


	public void SetJumpCap(bool b) { 
		this.jumpCap = b;
	}

	public GameObject GetPlayer() {
		return this.currentPlayerObject;
	}

	void GameOver() {
		Time.timeScale = 0; //Pausee pelin 
		gameOver.enabled = true; //Game over kanvas tulee esiin

	}

	void Pause() {
		if (pauseCanvas.enabled == true) {
			Time.timeScale = 1; //Palauttaa peliajan normaaliksi
			pauseCanvas.enabled = false; //Pause kanvas tulee esiin

		} else if (pauseCanvas.enabled == false) {
			Time.timeScale = 0; //Pysäyttää peliajan
			pauseCanvas.enabled = true; //Pause kanvas tulee esiin
		}	
	}

	void Flip() {

		if (currentPlayerObject == patientPlayerObject) { 
			patientPlayerObject.transform.Translate (-200, 0, 0); //Tasaa flipin yhteydessä tapahtuvaa "nykäisyiä". Modeli kohtainen arvo.
		} else if (currentPlayerObject == thiefPlayerObject) {
			thiefPlayerObject.transform.Translate (-1000, 0, 0);  //Tasaa flipin yhteydessä tapahtuvaa "nykäisyiä". Modeli kohtainen arvo.
		}
		currentPlayerObject.transform.Rotate (0, 180, 0); //Modeli peilaantuu
		currentPlayer.SetFacing (!currentPlayer.Facing()); //player.Facing saa vastakkaisen arvon. 
	}
}
