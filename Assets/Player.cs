using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

	static int points;
	GameControl controls;
	GameObject coin; //Siirretään mahdollisesti GameItems (tai vast.) luokkaan
	bool doubleJump;
	bool freeze; //onko pelaaja jäädytetty (kuollut, peli pausettu, gameover jnejne)
	bool facing; //kummalle puolelle pelaaja katsoo, true = oikealle
	int health; //pelaajan hp
	bool goal; //onko pelaaja käynyt maalissa
	bool nearSwitch;
	bool grounded;
	public int jumpHeight; //Jos muuttuja on public niin silloin sitä voidaan säätää unityn puolella
	public int speed;
	Slider healthSlider;
	Image damageImage;
	float flashSpeed = 5f;
	Color flashColour;
	Vector3 location;
	Rigidbody2D rb;
	bool jumpCap;
	int jumps;

	// Use this for initialization
	void Start ()
	{
		rb = gameObject.GetComponent<Rigidbody2D> ();
		nearSwitch = false;
		freeze = false;
		controls = GameObject.Find ("GameControl").GetComponent<GameControl> (); //Haetaan referenssi GameControl scriptiin   
		facing = true;
		health = 100; 
		goal = false;
		grounded = true;
		Time.timeScale = 1;
		damageImage = GameObject.Find ("DamageImage").GetComponent<Image> ();
		healthSlider = gameObject.GetComponentInChildren<Slider> (); 
		flashColour = new Color (1f, 0f, 0f, 0.1f);
	}

	// Update is called once per frame
	void Update ()
	{
		Debug.Log (nearSwitch);
		location = gameObject.transform.position;
		damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
	}

	void OnCollisionEnter2D (Collision2D collision)
	{ //Kun pelaaja osuu collideriin tapahtuu seuraava: 
		//collision muuttuja on collideri mihin pelaaja koskee 
		Debug.Log(collision);

		 if (collision.gameObject.tag == "Hazard") { 
			TakeDamage (35);
			Grounded ();// joten pelaaja voi hyppää uudelleen välttääkseen putoamasta piikkien päälle uudestaan. 

		} else if (collision.gameObject.name == "Switch") {
			nearSwitch = true;

		} else if (collision.gameObject.name == "Goal") { //Jos pelaaja koskettaa maalia
			goal = true;
			freeze = true;
			controls.SwitchPlayers ();

		} else if (collision.gameObject.tag == "Player") {

		} else { 
			Grounded ();
			nearSwitch = false;


			}
		}


	public void Jump ()
	{ 	//Hyppy
		jumps++;
//		gameObject.transform.Translate (0, 20, 0); //Hypyn "räjähtävyys"
		rb.AddForce(new Vector3(0, 3500, 0) * jumpHeight, ForceMode2D.Impulse); //Hypyn "räjähtävyys"
		if (gameObject.name == "Thief" && jumps == 2) {
			jumpCap = true;
			grounded = false;
		} else if (gameObject.name == "Patient") {
			jumpCap = true;
			grounded = false;
		}
	}

	public void TakeDamage (int i)
	{
		health -= i;
		healthSlider.value = health;
		damageImage.color = flashColour;
		Debug.Log ("health: " + health);
		if (health > 0) { //pelaaja ponnahtaa iskusta vain jos isku ei ole tappava
//			gameObject.transform.Translate (0, 300, 0); //pelaaja ponnahtaa ylös 
			rb.AddForce(new Vector3(0, 7000, 0), ForceMode2D.Impulse);
		}
	}

	void Grounded() {
		grounded = true;
		jumpCap = false;
		jumps = 0;
	}
	public void AddPoints (int i)
	{
		points += i;
	}

	public void TakeHealth (int hp)
	{
		health += hp;
		healthSlider.value = health;
	}

	public bool JumpCap() {
		return jumpCap;
	}

	public int GetPoints ()
	{ //Palauttaa pisteet GameControl:ille
		return points;
	}

	public int GetHealth ()
	{
		return health;
	}

	public bool Facing ()
	{
		return facing;
	}

	public void SetFacing (bool b)
	{
		facing = b;
	}

	public bool GetGoal ()
	{
		return goal;
	}

	public bool GetFreeze ()
	{ 
		return freeze;
	}

	public void SetFreeze (bool b)
	{ 
		freeze = b;
	}

	public float Speed ()
	{
		return speed;
	}

	public bool GetGrounded ()
	{
		return grounded;
	}

	public bool GetSwitch()
	{
		return nearSwitch;
	}
}
