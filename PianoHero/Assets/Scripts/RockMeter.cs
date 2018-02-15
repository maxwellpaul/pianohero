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
		needle = transform.Find ("Needle").gameObject;
		gm = GameObject.Find ("GameManager");
		needle.transform.localScale = new Vector3 (0.02F, 1F, 1F);
		rotationPoint = transform.Find ("BottomNeedle").position + new Vector3 (0, -0.6F, 0);
		SetToWin ();
	}

	public void SetToWin() {
		needle.transform.RotateAround (rotationPoint, Vector3.back, PlayerPrefs.GetInt("amtOfRock"));
		needle.transform.localScale = new Vector3 (0.02F, 1F, 1F);
	}

	public void MeterUp() {
		if (amtOfRock < 35) {
			needle.transform.RotateAround (rotationPoint, Vector3.back, 1);
			needle.transform.localScale = new Vector3 (0.02F, 1F, 1F);
			++amtOfRock;
			PlayerPrefs.SetInt ("amtOfRock", amtOfRock);
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
		PlayerPrefs.SetInt ("amtOfRock", amtOfRock);
	}
}
