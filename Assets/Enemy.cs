using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC {
	int time;
	int time2;


	// Use this for initialization
	void Start () {
		controls = GameObject.Find ("GameControl").GetComponent<GameControl>();

		if (behaviourModel.Equals ("AxeMan")) {
			ThrowAxe ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		Player currentPlayer = controls.GetPlayer (); //Haetaan referenssi tämän hetkiselle pelattavalle hahmolle,
		playerFacing = currentPlayer.Facing (); 	//jotta voidaan asettaa NPC nytkähtämään, iskettäessä, oikeaan suuntaan myöhemmin.
		time++;
		if (health <= 0) {
			DeactivateSelf ();

		} else if (behaviourModel.Equals ("SpearAndShield")) {
			PaceAround ();

		} else if (behaviourModel.Equals ("AxeMan")) {
			time++;
			if (time == 1000) {
				ThrowAxe ();
				time = 0;
			}
		} else if (behaviourModel.Equals ("Eagle")) {
			FlyAround ();
		}
	}

	void PaceAround() {
		gameObject.transform.Translate (2, 0, 0); //kävely nopeus 
		time++;
		if (time == 300) { //kuinka kauan npc liikkuu suuntaan ennen kääntymistä
			Flip ();
			time = 0;
		}
	}

	void FlyAround() {
		time++;
		time2++;
		if (time2 == 900) {
			Flip ();
			time = 0;
			time2 = 0;
		} else if (time < 300) {
			gameObject.transform.Translate (3, -1, 0);
		} else if (time > 300) {
			gameObject.transform.Translate (3, 1, 0);
			if (time == 600) {
				time = 0;
			}
		}
	}

//	void ThrowAxes() {
//		Weapon weapon = GameObject.Find ("Axe").GetComponent<Weapon> ();
//		time++;
//		if (time == 1000) {
//			Weapon newWeapon = Instantiate (weapon.gameObject, weapon.GetWeaponPosition (), weapon.GetWeaponRotation (), weapon.gameObject.transform.parent).GetComponent<Weapon> ();
//			newWeapon.ThrowWeapon ();
//			time = 0;
//		}
//	}

	void ThrowAxe() {
		Weapon weapon = GameObject.Find ("Axe").GetComponent<Weapon> ();
		Weapon newWeapon = Instantiate (weapon.gameObject, weapon.GetWeaponPosition (), weapon.GetWeaponRotation (), weapon.gameObject.transform.parent).GetComponent<Weapon> ();
		newWeapon.ThrowWeapon ();
	}
}