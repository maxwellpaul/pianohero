using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using System;
using System.Diagnostics;


public class Menu : MonoBehaviour {

	GameObject gm;

	/// ----------
	/// Init
	/// ----------

	void Start () {
		Utility.LocalNotePath = PlayerPrefs.GetString (Const.resourcePathKey) + "NoteFiles/";
		PopulateList ();
	}

	/// ----------
	/// Buttons
	/// ----------

	// Quit the game
	public void QuitGameButton() {
		Application.Quit ();
	}

	// Alternate Play Screen
	public void PlayAltGameButton() {
		List<string> options = PopulateList ();
		if (options.Count == 0)
			return;

		SceneManager.LoadScene(Const.SongSelectScene);
	}

	// Button to open high scores dialog
	public void OpenHighScoresWindowButton() {
		SceneManager.LoadScene (Const.HighScoresScene);
	}

	// Button to open the high scores dialog
	public void OpenLoadWindowButton() {
		SceneManager.LoadScene (Const.LoadSongScene);
	}

	public void OpenSettingsButton() {
		SceneManager.LoadScene (Const.SettingsScene);
	}

	public void OpenHelpButton() {
		SceneManager.LoadScene (Const.HelpScene);
	}

	/// ----------
	/// Helpers
	/// ----------

	private List<string> PopulateList() {
		Utility.songTokens.Clear ();

		// Get the text songs in the given directory
		if (!System.IO.Directory.Exists (Utility.LocalNotePath)) {
			print ("Error: path dne - " + Utility.LocalNotePath);
			return new List<string>();
		}

		foreach (string file in System.IO.Directory.GetFiles(Utility.LocalNotePath)) {
			string[] filenameArr = file.Split('/');
			string filename = filenameArr [filenameArr.Length - 1];
			if (filename.EndsWith (".txt")) {
				filenameArr = filename.Split ('-');
				filename = filenameArr [0];
				if (!Utility.songTokens.Contains (filename))
					Utility.songTokens.Add (Utility.textToToken(filename));
			}
		}

		// Populate the dropdown with user readable names for the songs
		List<string> displaySongs = new List<string> ();
		foreach (string filename in Utility.songTokens)
			displaySongs.Add (Utility.tokenToDisplay (filename));
		return displaySongs;
	}
}
