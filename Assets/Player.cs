using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private int points; 
//	Camera cam; 

	void OnCollisionEnter2D(Collision2D collision) {
		transform.Translate (-1f, 0, 0); 
	}

 public int GetPoints() {
	return this.points;
	}

	// Use this for initialization
	void Start () {
		Input.GetAxis("Horizontal");

	}

//	bool testi() {
//		Input.GetKeyDown (KeyCode.A); //Palauttaa arvon True kun A on pohjassa
//
//	}
	
	// Update is called once per frame
	void Update () {
		
		
	}
}
