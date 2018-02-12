using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Menu : MonoBehaviour {

	public Dropdown dropdown;
	List<string> WAVFiles = new List<string> () { "Paul", "Maxwell" };
	public string selectedWAV;

	// Use this for initialization
	void Start () {
		PopulateList ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DropDownIndexChanged (int index) {
		selectedWAV = WAVFiles [index];
		print ("here");
		print (selectedWAV);
	}

	public void PopulateList() {
		// read the values from the folder
		string currPath = Application.dataPath + "/Songs/";
		foreach (string file in System.IO.Directory.GetFiles(currPath)) {
			print (file);
		}

		List<string> songs = new List<string> () { "Paul", "Maxwell" };
		dropdown.AddOptions (songs);
	}
}
