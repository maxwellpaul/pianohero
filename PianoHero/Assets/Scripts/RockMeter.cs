using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RockMeter : MonoBehaviour {
	
	int amtOfRock = 0;
	GameObject needle;
	GameObject gm;
	Vector3 rotationPoint;

	// Use this for initialization
	void Start () {
		amtOfRock = PlayerPrefs.GetInt (Const.amountOfRockKey);

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

	private void SetToWin() {
		amtOfRock = PlayerPrefs.GetInt (Const.amountOfRockKey);
		needle.transform.RotateAround (rotationPoint, Vector3.back, PlayerPrefs.GetInt(Const.amountOfRockKey));
		needle.transform.localScale = new Vector3 (0.02F, 1F, 1F);
	}

	public void MeterUp() {
		if (amtOfRock < 35) {
			needle.transform.RotateAround (rotationPoint, Vector3.back, 1);
			needle.transform.localScale = new Vector3 (0.02F, 1F, 1F);
			++amtOfRock;
			PlayerPrefs.SetInt (Const.amountOfRockKey, amtOfRock);
		}
	}

	public void MeterDown() {
		print ("amt " + amtOfRock);
		if (amtOfRock == -35) {
			gm.GetComponent<GameManager> ().Lose();
			return;
		}

		needle.transform.RotateAround (rotationPoint, Vector3.back, -1);
		needle.transform.localScale = new Vector3 (0.02F, 1F, 1F);
		--amtOfRock;
		PlayerPrefs.SetInt (Const.amountOfRockKey, amtOfRock);
	}
}
