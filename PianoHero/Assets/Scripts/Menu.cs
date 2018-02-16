using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using System;


public class Menu : MonoBehaviour {

	// Windows
	LoadMP3 loadWindow;
	HighScores highScoresWindow;

	GameObject gm;
	public Dropdown dropdown;

	// Current songTokens
	List<string> songTokens = new List<string> ();

	// Use this for initialization
	void Start () {
		PopulateList ();
		DropDownIndexChanged (0);
	}

	// Play the game
	public void PlayButton() {
		SceneManager.LoadScene (Const.GamePlayScene);
	}

	// Called when the user selects something from the dropdown menu
	public void DropDownIndexChanged (int index) {
		PlayerPrefs.SetString(Const.songChoiceTokenKey, songTokens [index]);
	}

	// Called to populate the dropdown and songTokens
	private void PopulateList() {

		// Clear previous data
		songTokens.Clear ();
		dropdown.ClearOptions ();

		// Get the text songs in the given directory
		string currPath = Application.dataPath + Const.LocalNotePath;
		foreach (string file in System.IO.Directory.GetFiles(currPath)) {
			string[] filenameArr = file.Split('/');
			string filename = filenameArr [filenameArr.Length - 1];
			if (filename.EndsWith (".txt")) {
				songTokens.Add (Utility.textToToken(filename));
			}
		}

		// Populate the dropdown with user readable names for the songs
		List<string> displaySongs = new List<string> ();
		foreach (string filename in songTokens)
			displaySongs.Add (Utility.tokenToDisplay (filename));
		dropdown.AddOptions (displaySongs);
	}

	// Button to open high scores dialog
	public void OpenHighScoresWindowButton() {
		highScoresWindow = ScriptableObject.CreateInstance<HighScores> ();
		highScoresWindow.SetSongs (songTokens);
		highScoresWindow.Show ();
	}

	// Button to open the high scores dialog
	public void OpenLoadWindowButton() {
		loadWindow = ScriptableObject.CreateInstance<LoadMP3> ();
		loadWindow.Show();
	}

	// Only called from OpenLoadWindow, load the file and call the backend TODO
	public void LoadFile() {
		loadWindow.Close ();

		string MP3Path = loadWindow.getMP3Path ();
		string songName = loadWindow.getSong ();
		string sourcePath = MP3Path.Remove (MP3Path.LastIndexOf('/'));
		string sourceFileName = MP3Path.Substring (MP3Path.LastIndexOf ('/'));
		string destToken = Utility.DisplayToToken (songName);
		string destFileName = destToken + ".mp3";

		CopyMP3File (sourcePath, MP3Path, destFileName);
	
		Program python = new Program (Const.pythonExe, destToken);
		Program matlab = new Program (Const.matlabExe, destToken);

		PopulateList ();
	}

	private void CopyMP3File(string sourcePath, string MP3Path, string destFileName) {
		if (System.IO.Directory.Exists (sourcePath)) {
			string targetPath = Application.dataPath + Const.LocalMP3Path;
			string destFilePath = System.IO.Path.Combine (targetPath, destFileName);
			foreach (string s in System.IO.Directory.GetFiles(sourcePath)) {
				if (s.Equals (MP3Path)) {
					System.IO.File.Copy (MP3Path, destFilePath, true);
					print ("Copy of " + MP3Path + " success!");
					return;
				}
			}
			print ("Copy of " + MP3Path + " Failed");
		} else {
			print ("Source path does not exist: " + MP3Path);
			return;
		}
	}
}
