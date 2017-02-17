using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	int damage; //miekan damage

	// Use this for initialization
	void Start () {
		damage = 10; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collision) { //Kun pelaaja osuu collideriin tapahtuu seuraava: 
		//collision muuttuja on collideri mihin pelaaja koskee 
		Debug.Log (collision); 




		if (gameObject.tag == "Player") { //Jos pelaajan ase osuu x niin tapahtuu y 
			if (collision.gameObject.tag == "Enemy") {
//				NPC npc = collision.gameObject.GetComponent<NPC> ();
//				npc.TakeDamage (damage);	
				Debug.Log ("osuit enemyyn");

			} else if (collision.gameObject.tag == "NPC") { 
				NPC npc = collision.gameObject.GetComponent<NPC> (); //käytetään referenssinä NPCtä johon miekka osui
				npc.TakeDamage (damage); //NPC johon miekka osui ottaa damagea sen verran mitä miekka tekee
			} 

		} else if (gameObject.tag == "Enemy") { //Jos vihollisen ase osuu x niin tapahtuu y 
			if (collision.gameObject.tag == "Player") {
//				Player player = collision.gameObject.GetComponent<Player> ();
//				player.TakeDamage (damage);	
				Debug.Log ("sinuun osuttiin");
			}
		}
	}

	void SetDamage(int i) {
		damage = i;
	}
}