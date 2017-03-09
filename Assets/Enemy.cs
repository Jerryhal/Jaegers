using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{
	int time;
	int time2;
	int time3;
	int time4;
	public bool paceAround;
	Vector2 position;
	Ray2D ray;
	Ray2D groundRay;
	RaycastHit2D hit;
	RaycastHit2D groundHit;
	bool agro;
	Collider2D claws;


	// Use this for initialization
	void Start ()
	{
		position = gameObject.transform.position;
		controls = GameObject.Find ("GameControl").GetComponent<GameControl> ();



		if (behaviourModel.Equals ("AxeMan")) { //axeman heittää spawnatessaan ensimmäisen kirveensä
			ThrowAxe ();

		} else if (behaviourModel.Equals ("Eagle")) {
			claws = GameObject.Find ("Claws").GetComponent<Collider2D> ();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		Player currentPlayer = controls.GetPlayer (); //Haetaan referenssi tämän hetkiselle pelattavalle hahmolle,
		playerFacing = currentPlayer.Facing (); 	//jotta voidaan asettaa NPC nytkähtämään, iskettäessä, oikeaan suuntaan myöhemmin.
		if (health <= 0) {
			Die();

		} else if (controls.globalFreeze == false) {
			
			if (behaviourModel.Equals ("SpearAndShield")) { //Kirjoita behavior modeli gameobjectille unityn puolella. Löytyy enemy script komponentista
				if (paceAround == true) {
					time++;
					PaceAround ();
				} else {
					gameObject.transform.Translate (speed, 0, 0);
				}

			} else if (behaviourModel.Equals ("AxeMan")) {
				time++;
				if (time == 1500 - speed * 100) { //kuinka usein axeman heittää kirveen 
					ThrowAxe ();
					time = 0;
				}

			} else if (behaviourModel.Equals ("Eagle")) {
				position = gameObject.transform.position;
				ray = new Ray2D (position + new Vector2 (130, -130), transform.TransformVector (1, -1, 0));
				hit = Physics2D.Raycast (position + new Vector2 (130, -130), transform.TransformVector (1, -1, 0));
				groundRay = new Ray2D (position + new Vector2 (0, -130), Vector2.down);
				groundHit = Physics2D.Raycast (position + new Vector2 (0, -130), Vector2.down);
			

				if (agro == true && groundHit.distance > 1) {
					gameObject.transform.Translate (10, -10, 0);
					time3++;

					if (time3 > 30) {
						agro = false;	
						claws.enabled = false;
					}

				} else if (hit.collider.tag == "Player" && hit.distance < 500) {
					time = 0;
					time3 = 0;
					agro = true;
					claws.enabled = true;

				} else if (groundHit.collider.tag == "GroundAndPlatform" && groundHit.distance < 300) {
					gameObject.transform.Translate (0, speed, 0);

				} else {		
					FlyAround ();
				}
			}
		}
	}

	void PaceAround ()
	{
		gameObject.transform.Translate (speed, 0, 0); //kävely nopeus 
		if (time == 300) { //kuinka kauan npc liikkuu suuntaan ennen kääntymistä
			Flip ();
			time = 0;
		}
	}

	void FlyAround ()
	{ 
		time++;
		time2++;
		if (time2 == 600) { //kun time2 on 600 niin kotka kääntyy
			Flip ();
			time = 0;
			time2 = 0;
		} else if (time < 150) { // kun time < 150 niin kotka lentää eteen ja ylös
			gameObject.transform.Translate (speed, 1, 0); 
		} else if (time > 150) { //kun time > 150 niin kotka lentää eteen ja alas
			gameObject.transform.Translate (speed, -1, 0);
			if (time == 300) { //aika resettaa jolloin kotka tekee kaksi aikaisemmin mainittua liikettä uudelleen
				time = 0;
			}
		}
	}

	void ThrowAxe ()
	{
		Weapon weapon = GameObject.Find ("Axe").GetComponent<Weapon> ();
		Weapon newWeapon = Instantiate (weapon.gameObject, weapon.GetWeaponPosition (), weapon.GetWeaponRotation (), weapon.gameObject.transform.parent).GetComponent<Weapon> (); //luodaan uusi kirves
		newWeapon.ThrowWeapon (); //heitetään uusi kirves

	}
}