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
	LoadWAV loadWindow;
	HighScores highScoresWindow;

	List<string> textFiles = new List<string> ();
	List<string> displaySongs = new List<string> ();

	// Use this for initialization
	void Start () {
		PopulateList ();
		DropDownIndexChanged (0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ViewHighScores() {
		highScoresWindow = ScriptableObject.CreateInstance<HighScores> ();
		highScoresWindow.SetSongs (textFiles);
		highScoresWindow.Show ();
	}

	public void LoadWAVFile() {
		loadWindow = ScriptableObject.CreateInstance<LoadWAV> ();
		loadWindow.Show ();
	}

	public void PlayButton() {
		SceneManager.LoadScene (1);
	}

	public void DropDownIndexChanged (int index) {
		PlayerPrefs.SetString("SongChoice", textFiles [index]);
		PlayerPrefs.SetString ("SongFormattedTitle", displaySongs [index]);
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

		foreach (string filename in textFiles) {
			displaySongs.Add (parseFilename (filename));
		}
		dropdown.AddOptions (displaySongs);
	}

	string parseFilename(string filename) {
		// TODO add asserts
		filename = filename.Split ('.')[0];
		filename = filename.Replace ('_', ' ');
		return filename;
	}
}
