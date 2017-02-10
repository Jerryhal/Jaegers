using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	int points;
	GameControl controls;
	GameObject coin; //Siirretään mahdollisesti GameItems (tai vast.) luokkaan
	bool grounded;

	void OnCollisionEnter2D(Collision2D collision) { //Kun pelaaja osuu collideriin tapahtuu seuraava: 
		//collision muuttuja on collideri mihin pelaaja koskee 
		Debug.Log (collision.gameObject.name);

		if (collision.gameObject.name == "Coin") { //collision.gameObject.name == "spriteen liitetyn _Kuvan_ nimi"
			collision.collider.gameObject.SetActive (false); //Kolikko katoaa pelimaailmasta
			GameObject.Destroy (collision.collider.gameObject); //Kolikko katoaa unityn muistista
			points++;

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

	// Use this for initialization
	void Start () {
		controls = GameObject.Find("GameControl").GetComponent<GameControl>(); //Haetaan referenssi GameControl scriptiin 
		this.grounded = true;  

	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
}
