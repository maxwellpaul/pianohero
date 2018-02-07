﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMeter : MonoBehaviour {

	int amtOfRock = 0;
	GameObject needle;
	GameObject gm;
	Vector3 rotationPoint;

	// Use this for initialization
	void Start () {
		needle = transform.Find ("Needle").gameObject;
		gm = GameObject.Find ("GameManager");

		needle.transform.localScale = new Vector3 (0.02F, 1F, 1F);
		rotationPoint = transform.Find ("BottomNeedle").position + new Vector3 (0, -0.6F, 0);
	}

	public void MeterUp() {
		if (amtOfRock < 35) {
			needle.transform.RotateAround (rotationPoint, Vector3.back, 1);
			needle.transform.localScale = new Vector3 (0.02F, 1F, 1F);
			++amtOfRock;
		}
	}

	public void MeterDown() {
		if (amtOfRock == -35) {
			gm.GetComponent<GameManager> ().Lose();
			return;
		}

		needle.transform.RotateAround (rotationPoint, Vector3.back, -1);
		needle.transform.localScale = new Vector3 (0.02F, 1F, 1F);
		--amtOfRock;
	}

	// Update is called once per frame
	void Update () {

	}
}
