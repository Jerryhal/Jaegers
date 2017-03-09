using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

	public int health; //Jos muuttuja on public niin silloin sitä voidaan säätää unityn puolella
	public int speed;
	public string behaviourModel; 
	public bool facing; //kummalle puolelle npc katsoo. oikealle = true
	protected bool freeze; //protected muuttujaa voidaan käyttää periytyvissä luokissa mutta ei voida säätää unityn puolella
	protected bool grounded; //onko npc maassa 
	protected bool playerFacing;  //kummalle puolelle pelaaja katsoo
	protected bool move; 
	protected GameControl controls;
	protected Vector3 location;
	protected Rigidbody2D rb;
	GameObject gameItems;
	LevelManager levels;


	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D> ();
		gameItems = GameObject.Find ("GameItems");
		controls = GameObject.Find ("GameControl").GetComponent<GameControl>();
		levels = GameObject.Find ("Levels").GetComponent<LevelManager> ();

		if (facing == false) {
			Flip ();
			facing = true;
		} 
	}
	
	// Update is called once per frame
	void Update () {
		location = gameObject.transform.position;
		Player currentPlayer = controls.GetPlayer (); //Haetaan referenssi tämän hetkiselle pelattavalle hahmolle,
		playerFacing = currentPlayer.Facing (); 	//jotta voidaan asettaa NPC nytkähtämään, iskettäessä, oikeaan suuntaan myöhemmin.

		if (health <= 0) { 		//Kun hp tippuu nollaan niin 
			Die (); //npc katoaa pelimaailmasta 

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

	public void Die() {
		GameObject.Destroy (gameObject); //NPC katoaa unityn muistista
		int i = Random.Range(1, 100);
		if (i <= 20) {
			DropItem("HealthDrop");

		} else if (i >= 65) {
			DropItem ("Coin");

		}
	}

	public void DropItem(string itemName) {
		GameObject item = GameObject.Find (itemName);
		GameObject newItem = Instantiate(item, gameObject.transform.position, new Quaternion(), controls.transform);
		Debug.Log ("Item dropped " + newItem);
		Debug.Log (newItem.transform.position);
	}
}