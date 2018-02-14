using System.Collections;
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

	List<string> songTokens = new List<string> ();

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
		highScoresWindow.SetSongs (songTokens);
		highScoresWindow.Show ();
	}

	public void LoadWAVFile() {
		loadWindow = ScriptableObject.CreateInstance<LoadWAV> ();
		loadWindow.Show ();
	}

	public void PlayButton() {
		SceneManager.LoadScene (PHeroConsts.GamePlayScene);
	}

	public void DropDownIndexChanged (int index) {
		PlayerPrefs.SetString(PHeroConsts.songChoiceTokenKey, songTokens [index]);
	}

	public void PopulateList() {
		songTokens.Clear ();
		dropdown.ClearOptions ();

		string currPath = Application.dataPath + "/Songs/";
		foreach (string file in System.IO.Directory.GetFiles(currPath)) {
			string[] filenameArr = file.Split('/');
			string filename = filenameArr [filenameArr.Length - 1];
			if (filename.EndsWith (".txt")) {
				songTokens.Add (Utility.textToToken(filename));
			}
		}

		List<string> displaySongs = new List<string> ();
		foreach (string filename in songTokens) {
			displaySongs.Add (Utility.tokenToDisplay (filename));
		}
		dropdown.AddOptions (displaySongs);
	}
}
