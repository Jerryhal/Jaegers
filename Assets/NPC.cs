using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

	int health;
	bool facing;
	bool grounded;
	bool playerFacing; 
	GameControl controls;


	// Use this for initialization
	void Start () {
		health = 500;
		controls = GameObject.Find ("GameControl").GetComponent<GameControl>();
	}
	
	// Update is called once per frame
	void Update () {
		Player currentPlayer = controls.GetPlayer(); //Haetaan referenssi tämän hetkiselle pelattavalle hahmolle,
		playerFacing = currentPlayer.Facing (); 	//jotta voidaan asettaa NPC nytkähtämään, iskettäessä, oikeaan suuntaan myöhemmin.

		if (health <= 0) {
			gameObject.SetActive (false); //NPC katoaa pelimaailmasta
			GameObject.Destroy(gameObject); //NPC katoaa unityn muistista
		}
	}

	public void TakeDmg(int dmg) {
		health -= dmg; //healthistä lähtee damagen verran pois

		Debug.Log ("NPC Health: " + health);
		if (playerFacing == true) { //NPC nytkähtää riippuen mistä päin sitä isketään (mihin suuntaan pelaaja katsoo) 
			gameObject.transform.Translate (20, 20, 0); 
		} else {
			gameObject.transform.Translate (-20, 20, 0);
		}
	}	
}