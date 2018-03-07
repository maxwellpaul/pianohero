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
	public Dropdown songDropdown;
	public Dropdown difficultyDropdown;

	// Use this for initialization
	void Start () {
		Const.ResourcePath = PlayerPrefs.GetString ("ResourcePath");
		Const.LocalMP3Path = Const.ResourcePath + "MP3Files/";
		Const.LocalNotePath = Const.ResourcePath + "NoteFiles/";
		Const.LocalWAVPath = Const.ResourcePath + "WAVFiles/";

		PlayerPrefs.SetInt ("Score_Scene", 0);
		PlayerPrefs.SetInt ("Streak_Scene", 0);
		PlayerPrefs.SetInt ("Mult_Scene", 0);

		PopulateList ();
		SongDropDownIndexChanged (0);
		DifficultyDropDownIndexChanged (0);
	}

	/// ----------
	/// Buttons
	/// ----------

	// Play the game
	public void PlayButton() {
		SceneManager.LoadScene (Const.GamePlayScene);
	}

	// Quit the game
	public void QuitGameButton() {
		Application.Quit ();
	}

	// Button to open high scores dialog
	public void OpenHighScoresWindowButton() {
		SceneManager.LoadScene (3);
	}

	// Button to open the high scores dialog
	public void OpenLoadWindowButton() {
		SceneManager.LoadScene (4);
	}

	public void OpenSettingsButton() {
		SceneManager.LoadScene (Const.SettingsScene);
	}

	/// ----------
	/// Helper Functions
	/// ----------

	// Called when the user selects something from the dropdown menu
	public void SongDropDownIndexChanged (int index) {
		PlayerPrefs.SetString(Const.songChoiceTokenKey, Utility.songTokens [index]);
	}

	public void DifficultyDropDownIndexChanged (int index) {
		switch (index) {
		case 0:
			PlayerPrefs.SetString (Const.difficultyLevel, "Easy");
			break;
		case 1:
			PlayerPrefs.SetString (Const.difficultyLevel, "Medium");
			break;
		case 2:
			PlayerPrefs.SetString (Const.difficultyLevel, "Hard");
			break;
		case 3:
			PlayerPrefs.SetString (Const.difficultyLevel, "Expert");
			break;
		default:
			break;
		}
	}

	// Called to populate the dropdown and songTokens
	private void PopulateList() {

		// Clear previous data
		Utility.songTokens.Clear ();
		songDropdown.ClearOptions ();

		// Get the text songs in the given directory
		string currPath = Const.LocalNotePath;
		if (!System.IO.Directory.Exists (currPath))
			print ("Error: path dne - " + currPath);

		foreach (string file in System.IO.Directory.GetFiles(currPath)) {
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
		songDropdown.AddOptions (displaySongs);
	}
}
