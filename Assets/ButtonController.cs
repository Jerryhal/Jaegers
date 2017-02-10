using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Tätä luokkaa käytetään toistaseksi vain virtuaali nappuloiden toimintaan

public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	private bool pressed; 

	public void OnPointerDown(PointerEventData e) {
		pressed = true; 
	}

	public void OnPointerUp(PointerEventData e) {
		pressed = false;
	}

	public bool GetPressed() {
		return pressed;
	}
}
		