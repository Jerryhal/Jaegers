using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

	public int health;
	public string behaviourModel;
	protected bool freeze;
	protected bool facing;
	protected bool grounded;
	protected bool playerFacing; 
	protected bool move;
	protected GameControl controls;


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

	public void Flip() {
		gameObject.transform.Rotate (0, 180, 0); //NPC kääntyy 180 astetta Y-akselilla eli peilaantuu.    
		facing = !facing; //Facing saa vastakkaisen arvon. 
	}

	public void DeactivateSelf() {
		gameObject.SetActive (false); //NPC katoaa pelimaailmasta
		GameObject.Destroy (gameObject); //NPC katoaa unityn muistista
	}
}