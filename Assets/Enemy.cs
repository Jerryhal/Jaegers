using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC {
	int steps;
	Weapon weapon;
	int time;
	bool asd;


	// Use this for initialization
	void Start () {
		controls = GameObject.Find ("GameControl").GetComponent<GameControl>();
		move = true;
		weapon = GameObject.Find ("Axe").GetComponent<Weapon> ();
		GameObject newWeapon = Instantiate (weapon.gameObject, weapon.GetWeaponPosition (), weapon.GetWeaponRotation (), weapon.gameObject.transform.parent);

		Weapon weaponw = newWeapon.GetComponent<Weapon> ();
		weaponw.ThrowWeapon ();
		weapon.ThrowWeapon();
	
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

//	void ThrowAxes() {
//		weapon.ThrowWeapon ();
//		if (time == 500) {
//			Weapon newWeapon = Instantiate (weapon.gameObject, weapon.GetWeaponPosition (), weapon.GetWeaponRotation (), weapon.gameObject.transform.parent).GetComponent<Weapon> ();
//			newWeapon.ThrowWeapon ();
//			time = 0;
//		}
//	}
//	void NewWeaponThrow() {
//		Weapon newWeapon = Instantiate (weapon.gameObject, weapon.GetWeaponPosition (), weapon.GetWeaponRotation (), weapon.gameObject.transform.parent).GetComponent<Weapon> ();
//		newWeapon.ThrowWeapon ();	
//	}
}