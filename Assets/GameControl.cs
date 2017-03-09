using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

	public bool globalFreeze = false;
	GameObject patientPlayerObject; 
	GameObject thiefPlayerObject; 
	GameObject currentPlayerObject;
	GameObject door;
	GameObject switcher;
	LevelManager levels;
	Collider2D sword;
	ButtonController rightButton;
	ButtonController leftButton;
	Text points;
	Text GameOverText;
	Player patientPlayer;
	Player currentPlayer;
	Player thiefPlayer;
	Camera thiefCamera;
	Camera patientCamera; 
	Camera currentCamera;
	Canvas gameOver;
	Canvas pauseCanvas;
	Canvas thiefUI;
	Canvas patientUI;
	Rigidbody2D playerRigidBody;
	Animator animator;

	// Use this for initialization
	void Start () { 

		switcher = GameObject.Find ("Switch");
		door = GameObject.Find ("Door");
		gameOver = GameObject.Find ("GameOverCanvas").GetComponent<Canvas> ();
		pauseCanvas = GameObject.Find ("PauseCanvas").GetComponent<Canvas> ();
		patientUI = GameObject.Find ("PatientUI").GetComponent<Canvas> ();
		thiefUI = GameObject.Find ("ThiefUI").GetComponent<Canvas> ();
		points = GameObject.Find ("PointsText").GetComponent<Text> ();
		patientPlayerObject = GameObject.Find ("Patient"); 
		thiefPlayerObject = GameObject.Find ("Thief"); 
		patientPlayer = GameObject.Find("Patient").GetComponent<Player>();
		thiefPlayer = GameObject.Find("Thief").GetComponent<Player>();
		thiefCamera = GameObject.Find("ThiefCamera").GetComponent<Camera>();
		patientCamera = GameObject.Find("PatientCamera").GetComponent<Camera>();
		sword = GameObject.Find ("Sword").GetComponent<Collider2D>();
		levels = GameObject.Find ("Levels").GetComponent<LevelManager> ();
		currentPlayer = patientPlayer;
		currentPlayerObject = patientPlayerObject;
		currentCamera = patientCamera;
		pauseCanvas.enabled = false;
		playerRigidBody = currentPlayer.GetComponent<Rigidbody2D> ();
		animator = patientPlayerObject.GetComponent<Animator> ();

//		rightButton = GameObject.Find ("RightButton").GetComponent<ButtonController>(); 
//		leftButton = GameObject.Find ("LeftButton").GetComponent<ButtonController>(); Mahdollisia virtuaali nappeja varten


	}

	// Update is called once per frame

	void Update (){



		if (currentPlayer.GetFreeze() == false) { //Jos pelaaja ei ole game over tai pause screenissä niin voi liikkua

			if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
				animator.SetBool("moving", false);

			if (Input.GetKeyDown (KeyCode.W)) { //Hyppää, mikäli pelaaja on maassa
				if (currentPlayer.GetGrounded () == true && currentPlayer.JumpCap () == false) { 
					currentPlayer.Jump ();
				}

//			} else if (Input.GetKeyUp (KeyCode.A) || Input.GetKeyUp (KeyCode.A)) {
//				playerRigidBody.velocity = new Vector3(0, 0, 0);

			} else if (Input.GetKey (KeyCode.A)) { //Liiku vasemmalle
				if (currentPlayer.Facing () == false) {
					animator.SetBool("moving", true);
//					currentPlayer.transform.Translate (10, 0, 0); //Hypyn "räjähtävyys"
					playerRigidBody.AddForce(new Vector3(-200, 0, 0) * currentPlayer.Speed(), ForceMode2D.Impulse);
						
//					playerRigidBody.MovePosition (playerRigidBody.transform.position + new Vector3(-1000, 0, 0) * Time.deltaTime);

				} else {
					Flip ();
				}

			} else if (Input.GetKey (KeyCode.D)) { //liiku oikealle
				if (currentPlayer.Facing () == true) {
					animator.SetBool("moving", true);
//					currentPlayer.transform.Translate (10, 0, 0); 
					playerRigidBody.AddForce(new Vector3(200, 0, 0) * currentPlayer.Speed(), ForceMode2D.Impulse);
//					playerRigidBody.MovePosition (playerRigidBody.transform.position + new Vector3(1000, 0, 0) * Time.deltaTime);
//					playerRigidBody.AddRelativeForce (Vector2.right * 1000, ForceMode2D.Impulse);

				} else {
					Flip ();
				}
			}

			if (Input.GetKeyDown (KeyCode.Mouse0)) { //kun M1 painetaan niin miekka collideri aktivoituu 
				if (currentPlayer == patientPlayer) {
					Debug.Log ("Sword " + sword.enabled);
					sword.enabled = true;
				} else if (currentPlayer == thiefPlayer) {
					currentPlayer.transform.Translate (1000, 0, 0); 

				} 

				} else if (Input.GetKeyDown (KeyCode.E) && currentPlayer.GetSwitch () == true) {
				PressSwitch ();
				OpenDoor ();

			} else if (Input.GetKeyUp (KeyCode.Mouse0)) { //kun M1 nostetaan pohjasta miekka collideri deaktivoituu
				if (currentPlayer == patientPlayer) {
					Debug.Log ("Sword " + sword.enabled);
					sword.enabled = false;
				}

				//			} else if (Input.GetKeyUp (KeyCode.O)) { //Testi nappula
			}
		}
		if (Input.GetKeyDown (KeyCode.Escape) && gameOver.enabled == false) { //Esc Pausee pelin 
			Pause ();

		} else if (Input.GetKeyDown (KeyCode.Tab)) {//Tab vaihtaa hahmoa 
			SwitchPlayers ();
		}

		if (currentPlayer.GetHealth () <= 0) {
			GameOver ();

		} else if (thiefPlayer.GetGoal() == true && patientPlayer.GetGoal() == true) {
			levels.LoadNextLevel ();
		}
		points.text = "Points: " + (thiefPlayer.GetPoints() + patientPlayer.GetPoints()); //Pitää näytöllä lukua kerätyistä kolikoista
	}

	void GameOver() {
		Time.timeScale = 0; //pysäyttää ajan 
		globalFreeze = true;
		gameOver.enabled = true; //Game over kanvas tulee esiin
//		thiefPlayer.SetFreeze(true);
//		patientPlayer.SetFreeze (true);

	}

	void Pause() {
		if (pauseCanvas.enabled == true) {
			Time.timeScale = 1; //Palauttaa peliajan normaaliksi
			globalFreeze = false;
			pauseCanvas.enabled = false; //Pause kanvas menee pois 
			thiefPlayer.SetFreeze(false);
			patientPlayer.SetFreeze (false);

		} else if (pauseCanvas.enabled == false) {
			Time.timeScale = 0; //Pysäyttää peliajan
			globalFreeze = true;
			pauseCanvas.enabled = true; //Pause kanvas tulee esiin
			thiefPlayer.SetFreeze(true);
			patientPlayer.SetFreeze (true);
		}	
	}

	void Flip() {
		currentPlayerObject.transform.Rotate (0, 180, 0); //PlayerObject kääntyy 180 astetta Y-akselilla eli peilaantuu.
		currentCamera.transform.Rotate(0, -180, 0); //koska PlayeObject ja kaikki sille alistetut komponentit peilaantuu myös, pitää kamera asetta normaaliksi kääntämäälä se toiset 180 astetta.
		currentCamera.transform.Translate (0, 0, -20); //Kun kamera pelitetään toista kertaa, pelimaailma jää sen "taakse". siksi sitä pitää tuoda takaisinpäin Z-akselilla.    
		currentPlayer.SetFacing (!currentPlayer.Facing()); //player.Facing saa vastakkaisen arvon. 
	}

	public void SwitchPlayers() {
		
			levels.PlayerSwitch ();

		if (currentPlayer == patientPlayer) { //Jos nykyinen (current) pelihahmo on jo patient niin kaikki current referenssit vaihtuu thieffin referensseihin
			thiefCamera.enabled = true; //Thieffin kamera käynnistyy
			patientCamera.enabled = false; //patientin kamera sammuu
			thiefUI.enabled = true;
			patientUI.enabled = false;
			currentPlayer = thiefPlayer;
			currentPlayerObject = thiefPlayerObject;
			currentCamera = thiefCamera;
			playerRigidBody = currentPlayer.GetComponent<Rigidbody2D> ();

		} else if (currentPlayer == thiefPlayer){ //Jos nykyinen (current) pelihahmo on jo thief niin kaikki current referenssit vaihtuu pateintin referensseihin
			patientCamera.enabled = true; //patientin kamera käynnistyy
			thiefCamera.enabled = false; //theiffin kamera sammuu
			patientUI.enabled = true;
			thiefUI.enabled = false;
			currentPlayer = patientPlayer;
			currentPlayerObject = patientPlayerObject;
			currentCamera = patientCamera;
			playerRigidBody = currentPlayer.GetComponent<Rigidbody2D> ();
		}
	}

	public Player GetPlayer() {
		return this.currentPlayer;
	}

	public bool GetGlobalFreeze () {
	return globalFreeze;
	}

	void OpenDoor() {
		Collider2D doorCollider = door.GetComponent<Collider2D> ();
		doorCollider.enabled = !doorCollider.enabled; 
		SpriteRenderer[] sprites = door.GetComponentsInChildren<SpriteRenderer> ();
		for (int i = 0; i < sprites.Length; i++) { //Käy läpi jokaisen gameobjectille alistetun gameobjectin spriten
			sprites [i].enabled = !sprites [i].enabled; 
		}
	}

	public void PressSwitch() {
		SpriteRenderer[] sprites = switcher.GetComponentsInChildren<SpriteRenderer> ();
		for (int i = 0; i < sprites.Length; i++) { //Käy läpi jokaisen gameobjectille alistetun gameobjectin spriten
			sprites [i].enabled = !sprites [i].enabled; 
		}
	}
}
