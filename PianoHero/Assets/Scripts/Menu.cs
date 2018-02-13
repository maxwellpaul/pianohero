﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	GameObject gm;
	public Dropdown dropdown;
	Regex r_text = new Regex (@"(\.txt)", RegexOptions.IgnoreCase);
	Regex r_meta = new Regex (@"(\.meta)", RegexOptions.IgnoreCase);

	List<string> textFiles = new List<string> ();

	// Use this for initialization
	void Start () {
		PopulateList ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoadWAVFile() {
		
	}

	public void PlayButton() {
		print ("Play game!!");
		SceneManager.LoadScene (1);	
	}

	public void DropDownIndexChanged (int index) {
		gm.GetComponent<GameManager> ().SetSong (textFiles [index]);
	}

	public void PopulateList() {
		textFiles.Clear ();
		dropdown.ClearOptions ();

		string currPath = Application.dataPath + "/Songs/";
		foreach (string file in System.IO.Directory.GetFiles(currPath)) {
			string[] filenameArr = file.Split('/');
			string filename = filenameArr [filenameArr.Length - 1];
			if (filename.EndsWith (".txt")) {
				textFiles.Add (filename);
			}
		}

		List<string> songs = new List<string> () {};
		foreach (string filename in textFiles) {
			songs.Add (splitFilename (filename));
		}
		dropdown.AddOptions (songs);
	}

	string splitFilename(string filename) {
		return filename;
	}
}
