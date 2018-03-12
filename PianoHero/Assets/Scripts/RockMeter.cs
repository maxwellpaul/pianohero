﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RockMeter : MonoBehaviour {
	
	GameObject needle;
	GameObject gm;
	Vector3 rotationPoint;

	/// ----------
	/// Init
	/// ----------

	void Start () {
		needle = transform.Find (Const.NeedleObj).gameObject;
		while (needle == null)
			needle = transform.Find (Const.NeedleObj).gameObject;
		
		gm = GameObject.Find (Const.GameManagerObj);
		needle.transform.localScale = new Vector3 (0.02F, 1F, 1F);

		Transform bottomNeedle = transform.Find (Const.BottomNeedleObj);
		while (bottomNeedle == null)
			bottomNeedle = transform.Find (Const.BottomNeedleObj);
		
		rotationPoint = bottomNeedle.position + new Vector3 (0, -0.6F, 0);
		SetToWin ();
	}

	/// ----------
	/// Callable Funcs
	/// ----------

	public void MeterUp() {
		if (Utility.amountOfRock < 35) {
			needle.transform.RotateAround (rotationPoint, Vector3.back, 1);
			needle.transform.localScale = new Vector3 (0.02F, 1F, 1F);
			++Utility.amountOfRock;
		}
	}

	public void MeterDown() {
		if (Utility.amountOfRock == -35) {
			gm.GetComponent<GameManager> ().Lose();
			return;
		}

		needle.transform.RotateAround (rotationPoint, Vector3.back, -1);
		needle.transform.localScale = new Vector3 (0.02F, 1F, 1F);
		--Utility.amountOfRock;
	}

	/// ----------
	/// Helpers
	/// ----------

	private void SetToWin() {
		needle.transform.RotateAround (rotationPoint, Vector3.back, Utility.amountOfRock);
		needle.transform.localScale = new Vector3 (0.02F, 1F, 1F);
	}
}
