﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//An activator is the circle at the bottom that notes pass through
public class Activator : MonoBehaviour {

	SpriteRenderer sr;
	Color old;

	//Whatever key the activator is linked to
	public KeyCode key;

	//To determine if a note is inside the activator
	bool active = false;

	//The game object inside the activator
	GameObject note;

	// Use this for initialization
	void Awake () {
		sr = GetComponent<SpriteRenderer> ();
		PlayerPrefs.SetInt ("Score", 0);	
	}

	void Start() {
		old = sr.color;
	}

	// Update is called once per frame
	void Update () {
		//If the intended key is pressed and there is a note inside the activator
		if (Input.GetKeyDown (key)) {
			StartCoroutine (Pressed ());
		}

		if (Input.GetKeyDown (key) && active) {
			Destroy (note);
			AddScore ();
			active = false;
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		active = true;
		//If there is a note inside the activator, grab its game object
		if (col.gameObject.tag == "Note") {
			note = col.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		active = false;
	}

	void AddScore() {
		PlayerPrefs.SetInt ("Score", PlayerPrefs.GetInt ("Score") + 100);
	}

	IEnumerator Pressed() {
		sr.color = new Color (0, 0, 0);
		yield return new WaitForSeconds (0.05f);
		sr.color = old;
	}
}