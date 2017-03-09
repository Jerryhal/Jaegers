using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItems : MonoBehaviour {
	Quaternion weaponRotation;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Player") { 
			Player player = collision.gameObject.GetComponent<Player> (); 

			if (gameObject.tag == "HealthDrop") {
				player.TakeHealth (20);
				GameObject.Destroy (gameObject);
				Debug.Log ("hp added " + player.GetHealth ());

			} else if (gameObject.tag == "Coins") {
				player.AddPoints (1);
				GameObject.Destroy (gameObject);
			}
		}
	}
}