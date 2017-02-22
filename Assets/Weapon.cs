using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public int damage; //miekan damage

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collision) { //Kun pelaaja osuu collideriin tapahtuu seuraava: 
		//collision muuttuja on collideri mihin pelaaja koskee  


		if (gameObject.tag == "Player") { //Jos pelaajan ase osuu x niin tapahtuu y 
			if (collision.gameObject.tag == "Enemy") {
				Enemy enemy = collision.gameObject.GetComponent<Enemy> ();
				enemy.TakeDamage (damage);	
				Debug.Log ("osuit enemyyn");

			} else if (collision.gameObject.tag == "NPC") { 
				NPC npc = collision.gameObject.GetComponent<NPC> (); //käytetään referenssinä NPCtä johon miekka osui
				npc.TakeDamage (damage); //NPC johon miekka osui ottaa damagea sen verran mitä miekka tekee
			} 

		} else if (gameObject.tag == "Enemy") { //Jos vihollisen ase osuu x niin tapahtuu y 
			if (collision.gameObject.tag == "Player") {
				Player player = collision.gameObject.GetComponentInChildren<Player> ();
				player.TakeDamage (damage);	
			}
		}
	}

	void SetDamage(int i) {
		damage = i;
	}
}