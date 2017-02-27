using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	int points;
	GameControl controls;
	GameObject coin; //Siirretään mahdollisesti GameItems (tai vast.) luokkaan
	bool grounded; //koskettaako pelaajan jalat maata, true = koskettaa
	bool freeze; //onko pelaaja jäädytetty (kuollut, peli pausettu, gameover jnejne)
	bool facing; //kummalle puolelle pelaaja katsoo, true = oikealle
	int health; //pelaajan hp 
	bool goal; //onko pelaaja käynyt maalissa


	// Use this for initialization
	void Start () {
		freeze = false;
		controls = GameObject.Find("GameControl").GetComponent<GameControl>(); //Haetaan referenssi GameControl scriptiin 
		grounded = true;  
		facing = true;
		health = 100; 
		goal = false;

	}

	// Update is called once per frame
	void Update () {
		

	}

	void OnCollisionEnter2D(Collision2D collision) { //Kun pelaaja osuu collideriin tapahtuu seuraava: 
		//collision muuttuja on collideri mihin pelaaja koskee 

		if (collision.gameObject.tag == "Coins") { 
			collision.collider.gameObject.SetActive (false); //Kolikko katoaa pelimaailmasta
			GameObject.Destroy (collision.collider.gameObject); //Kolikko katoaa unityn muistista
			points++;

		} else if (collision.gameObject.tag == "Hazard") { //collision.gameObject.name == "spriten nimi"
			health -= 35; //piikkien damage
			if (health > 0) { //pelaaja ponnahtaa iskusta vain jos isku ei ole tappava
				gameObject.transform.parent.Translate (0, 300, 0); //pelaaja ponnahtaa ylös 
			}
			Debug.Log("health: "+ health);
			controls.SetJumpCap (false); //piikkien koskeminen lasektaan maassa käymiseksi,
			grounded = true;			// joten pelaaja voi hyppää uudelleen välttääkseen putoamasta piikkien päälle uudestaan. 

		} else if (collision.gameObject.name == "Goal") { //Jos pelaaja koskettaa maalia
			goal = true;
			freeze = true;


		} else { //Suosittelen antamaan kaikille tasanne tai maa collideri kuville samat nimet 
			controls.SetJumpCap (false); //Kun pelaaja osuu Collideriin, mikä ei ole kolikko, jumpCap = false -> pelaaja voi hypätä uudelleen
			grounded = true; //pelaaja on maassa joten grounded = true

		}
	}

 	public int GetPoints() { //Palauttaa pisteet GameControl:ille
		return this.points;
	}

	public bool GetGrounded() {
		return this.grounded;
	}

	public void SetGrounded(bool b) {
		this.grounded = b;
	}

	public int GetHealth() {
		return health;
	}

	public bool Facing() {
		return facing;
	}

	public void SetFacing(bool b) {
		facing = b;
	}

	public bool GetGoal() {
		return goal;
	}

	void SetAlive(bool b) {
	}

	public bool GetFreeze() { 
		return freeze;
	}
	public void SetFreeze(bool b) { 
		freeze = b;
	}

	public void TakeDamage(int i) {
		health -= i;
		Debug.Log ("health: " + health);
		if (health > 0) { //pelaaja ponnahtaa iskusta vain jos isku ei ole tappava
			gameObject.transform.Translate (0, 300, 0); //pelaaja ponnahtaa ylös 
		}
	}
}
