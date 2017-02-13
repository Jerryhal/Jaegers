using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	int damage;

	// Use this for initialization
	void Start () {
		damage = 50; //miekan damage
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collision) { //Kun pelaaja osuu collideriin tapahtuu seuraava: 
		//collision muuttuja on collideri mihin pelaaja koskee 
		Debug.Log (collision); 

		if (collision.gameObject.tag == "NPC") { 
			NPC npc = collision.gameObject.GetComponent<NPC>(); //käytetään referenssinä NPCtä johon miekka osui
			npc.TakeDmg (damage); //NPC johon miekka osui ottaa damagea sen verran mitä miekka tekee
		}
	}
}