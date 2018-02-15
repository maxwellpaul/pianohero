using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;


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
		PlayerPrefs.SetString (PHeroConsts.loadPath, string.Empty);
		PlayerPrefs.SetString (PHeroConsts.songName, string.Empty);

		PopulateList ();
		DropDownIndexChanged (0);
	}

	// Play the game
	public void PlayButton() {
		SceneManager.LoadScene (PHeroConsts.GamePlayScene);
	}

	// Called when the user selects something from the dropdown menu
	public void DropDownIndexChanged (int index) {
		PlayerPrefs.SetString(PHeroConsts.songChoiceTokenKey, songTokens [index]);
	}

	// Called to populate the dropdown and songTokens
	private void PopulateList() {

		// Clear previous data
		songTokens.Clear ();
		dropdown.ClearOptions ();

		// Get the text songs in the given directory
		string currPath = Application.dataPath + "/Songs/";
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
		string songName = loadWindow.getSong();
		string targetPath = Application.dataPath + "/Songs";

		string[] temp = MP3Path.Split ('/');
		string origFileName = temp [temp.Length - 1];
		string sourcePath = MP3Path.Remove(MP3Path.LastIndexOf ('/'));
		string fileName = songName.Replace (' ', '_') + ".txt";

		print ("WAVPath " + MP3Path);

		if (System.IO.Directory.Exists(sourcePath)) {
			string[] files = System.IO.Directory.GetFiles(sourcePath);
			// TODO verify that file is there

			string destFile = System.IO.Path.Combine(targetPath, fileName);
			System.IO.File.Copy(MP3Path, destFile, true);
		} else {
			print("Source path does not exist!");
		}
	}
}
