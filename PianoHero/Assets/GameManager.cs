using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public int multiplier = 1;
	public int streak = 0;
	public int mult_length = 10;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("Score", 0);
		UpdateGUI();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddStreak() {
		++streak;
		multiplier = 1 + streak / mult_length;
		UpdateGUI();
	}

	public void ResetStreak() {
		streak = 0;
		multiplier = 1;
		UpdateGUI();
	}

	void UpdateGUI() {
		PlayerPrefs.SetInt ("Streak", streak);
		PlayerPrefs.SetInt ("Mult", multiplier);
	}

	void OnTriggerEnter2D(Collider2D col) {
			
	}

	public int GetScore() {
		return 100 * multiplier;
	}
}
