using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

	GameObject player; 
	ButtonController rightButton;
	ButtonController leftButton;
//	Text points;
	int i;
	int height;
	int jump;
//	Camera camera; 

	// Use this for initialization
	void Start () {
//		points = GameObject.Find ("PointsText").GetComponent<Text> ();
		player = GameObject.Find ("Player");
//		rightButton = GameObject.Find ("RightButton").GetComponent<ButtonController>(); 
//		leftButton = GameObject.Find ("LeftButton").GetComponent<ButtonController>();
		Input.GetKey (KeyCode.A);
		i = 1;
		height = 100;
		jump = 10;

		

		
	}
		


	// Update is called once per frame
	void Update () {
			 if (Input.GetKey (KeyCode.A)) {
			player.transform.Translate (-1, 0, 0);

			} else if (Input.GetKey(KeyCode.D)) {
			player.transform.Translate (1, 0, 0);

			}
		}
	}
//if (Input.GetKey(KeyCode.A))
//{
//	transform.position -= new Vector3(Nopeus * Time.deltaTime, 0f, 0f);
//	if(lookingRight && Input.GetKey(KeyCode.A))
//}


