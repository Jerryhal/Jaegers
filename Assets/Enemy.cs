using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC {
	int steps;

	// Use this for initialization
	void Start () {
		controls = GameObject.Find ("GameControl").GetComponent<GameControl>();
		move = true;

	}
	
	// Update is called once per frame
	void Update () {
		Player currentPlayer = controls.GetPlayer (); //Haetaan referenssi tämän hetkiselle pelattavalle hahmolle,
		playerFacing = currentPlayer.Facing (); 	//jotta voidaan asettaa NPC nytkähtämään, iskettäessä, oikeaan suuntaan myöhemmin.

		if (health <= 0) {
			DeactivateSelf ();

		} else if (move == true) {
			PaceAround ();
		}
	}

	void PaceAround() {
		gameObject.transform.Translate (2, 0, 0); //kävely nopeus 
		steps++;
		if (steps == 300) { //kuinka kauan npc liikkuu suuntaan ennen kääntymistä
			Flip ();
			steps = 0;
		}
	}
}
