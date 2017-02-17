using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

	int health;
	int steps;
	bool facing;
	bool grounded;
	bool playerFacing; 
	bool move;
	GameControl controls;

	// Use this for initialization
	void Start () {
		health = 500;
		controls = GameObject.Find ("GameControl").GetComponent<GameControl>();
		steps = 0;
		move = true;
	}
	
	// Update is called once per frame
	void Update () {
		Player currentPlayer = controls.GetPlayer (); //Haetaan referenssi tämän hetkiselle pelattavalle hahmolle,
		playerFacing = currentPlayer.Facing (); 	//jotta voidaan asettaa NPC nytkähtämään, iskettäessä, oikeaan suuntaan myöhemmin.

		if (health <= 0) {
			gameObject.SetActive (false); //NPC katoaa pelimaailmasta
			GameObject.Destroy (gameObject); //NPC katoaa unityn muistista

		} else if (move == true) {
			PaceAround ();
			}
	}

	public void TakeDamage(int dmg) {
		health -= dmg; //healthistä lähtee damagen verran pois
		Debug.Log ("NPC Health: " + health);
		if (playerFacing == true) { //NPC nytkähtää riippuen mistä päin sitä isketään (mihin suuntaan pelaaja katsoo) 
			gameObject.transform.Translate (20, 20, 0); 
		} else {
			gameObject.transform.Translate (-20, 20, 0);
		}
	}	
	void PaceAround() {
		gameObject.transform.Translate (3, 0, 0); //kävely nopeus 
		steps++;
		if (steps == 200) { //kuinka kauan npc liikkuu suuntaan ennen kääntymistä
			Flip ();
			steps = 0;
		}
	}

	void Flip() {
		gameObject.transform.Rotate (0, 180, 0); //NPC kääntyy 180 astetta Y-akselilla eli peilaantuu.    
		facing = !facing; //Facing saa vastakkaisen arvon. 
	}
}