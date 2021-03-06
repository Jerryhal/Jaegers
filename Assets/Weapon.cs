﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public int damage; //miekan damage
	int time;
	bool weaponHit = false;
	bool thrown = false;
	Vector3 weaponPosition;
	Quaternion weaponRotation;

	// Use this for initialization
	void Start () {
		weaponPosition = gameObject.transform.position;
		weaponRotation = gameObject.transform.rotation;
	}

	// Update is called once per frame
	void Update () {

		if (thrown == true) {
			RotatingWeaponThrow ();
		}
	}

	void OnCollisionEnter2D(Collision2D collision) { //Kun pelaaja osuu collideriin tapahtuu seuraava: 
		//collision muuttuja on collideri mihin pelaaja koskee  


		if (gameObject.transform.parent.tag == "Player") { //Jos pelaajan ase osuu x niin tapahtuu y 
			if (collision.gameObject.tag == "Enemy") { //x
				Debug.Log(collision.gameObject.name);
				weaponHit = true; //tapahtuu y:
				Enemy enemy = collision.gameObject.GetComponent<Enemy> ();
				enemy.TakeDamage (damage);	
			}

		} else if (collision.gameObject.tag == "NPC") { 
			weaponHit = true;
			NPC npc = collision.gameObject.GetComponent<NPC> (); //käytetään referenssinä NPCtä johon miekka osui
			npc.TakeDamage (damage); //NPC johon miekka osui ottaa damagea sen verran mitä miekka tekee 

		} else if (gameObject.transform.parent.tag == "Enemy") { //Jos vihollisen ase osuu x niin tapahtuu y 
			if (collision.gameObject.tag == "Player") { //x
				weaponHit = true; //tapahtuu y:
				Player player = collision.gameObject.GetComponent<Player> ();
				player.TakeDamage (damage);	
				}
			}
		}

	public void RotatingWeaponThrow() {
		SpriteRenderer weaponSprite = gameObject.GetComponentInChildren<SpriteRenderer> ();
		Collider2D weaponCollider = gameObject.GetComponent<Collider2D> ();
		weaponCollider.enabled = true;
		weaponSprite.enabled = true;
		weaponSprite.transform.Rotate (0, 0, -2);
		gameObject.transform.Translate (2, 0, 0);
		if (weaponHit == true) {
			GameObject.Destroy (gameObject);
		}
	}

	public void DisableSelf() {
		SpriteRenderer weaponSprite = gameObject.GetComponentInChildren<SpriteRenderer> ();
		Collider2D weaponCollider = gameObject.GetComponent<Collider2D> ();
		weaponCollider.enabled = false;
		weaponSprite.enabled = false;
	}

	public int GetTime() {
		return time;
	}

	public Vector3 GetWeaponPosition() {
		return weaponPosition;
	}

	public Quaternion GetWeaponRotation() {
		return weaponRotation;
	}

	public bool GetWeaponHit() {
		return weaponHit;
	}

	public void ThrowWeapon() {
		thrown = true;	
	}
}