using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMeter : MonoBehaviour {

	int rm;
	GameObject needle;

	// Use this for initialization
	void Start () {
		needle = transform.Find ("Needle").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		rm = PlayerPrefs.GetInt ("RockMeter");

		// This wont work like we want it to
		needle.transform.localPosition = new Vector3 ((rm - 25) / 25, 0, 0);
	}
}
