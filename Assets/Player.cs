using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	int points;
	GameControl controls;
	GameObject coin; //Siirretään mahdollisesti GameItems (tai vast.) luokkaan
	bool grounded;
	bool freeze;
	bool facing;
	int health; 


	// Use this for initialization
	void Start () {
		freeze = false;
		controls = GameObject.Find("GameControl").GetComponent<GameControl>(); //Haetaan referenssi GameControl scriptiin 
		grounded = true;  
		facing = true;
		health = 100; 

	}

	// Update is called once per frame
	void Update () {
		

	}

	void OnCollisionEnter2D(Collision2D collision) { //Kun pelaaja osuu collideriin tapahtuu seuraava: 
		//collision muuttuja on collideri mihin pelaaja koskee 
		Debug.Log(collision); 

		if (collision.gameObject.name == "Coin") { //collision.gameObject.name == "spriteen liitetyn _Kuvan_ nimi"
			collision.collider.gameObject.SetActive (false); //Kolikko katoaa pelimaailmasta
			GameObject.Destroy (collision.collider.gameObject); //Kolikko katoaa unityn muistista
			points++;

		} else if (collision.gameObject.name == "Spikes") {
			health -= 35; //piikkien damage
			if (health > 0) { //pelaaja ponnahtaa iskusta vain jos isku ei ole tappava
				gameObject.transform.Translate (0, 300, 0); //pelaaja ponnahtaa ylös 
			}
			Debug.Log("health: "+ health);
			controls.SetJumpCap (false); //piikkien koskeminen lasektaan maassa käymiseksi,
			grounded = true;			// joten pelaaja voi hyppää uudelleen välttääkseen putoamasta piikkien päälle uudestaan. 

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

	public void SetAlive(bool b) {
	}
}
