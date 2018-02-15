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
		PlayerPrefs.SetString (PHeroConsts.loadPath, string.Empty);
		PlayerPrefs.SetString (PHeroConsts.songName, string.Empty);

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
		print ("hello!");
	}

	void LoadFile() {
		string WAVPath = PlayerPrefs.GetString (PHeroConsts.loadPath);
		string songName = PlayerPrefs.GetString (PHeroConsts.songName);
		string targetPath = Application.dataPath + "/Songs";

		string[] temp = WAVPath.Split ('/');
		string origFileName = temp [temp.Length - 1];
		string sourcePath = WAVPath.Remove(WAVPath.LastIndexOf ('/'));
		string fileName = songName.Replace (' ', '_') + ".txt";

		print ("WAVPath " + WAVPath);

		if (System.IO.Directory.Exists(sourcePath)) {
			string[] files = System.IO.Directory.GetFiles(sourcePath);
			// TODO verify that file is there

			string destFile = System.IO.Path.Combine(targetPath, fileName);
			System.IO.File.Copy(WAVPath, destFile, true);
		} else {
			print("Source path does not exist!");
		}
	}

	public void PlayButton() {
		SceneManager.LoadScene (PHeroConsts.GamePlayScene);
	}

	public void DropDownIndexChanged (int index) {
		PlayerPrefs.SetString(PHeroConsts.songChoiceTokenKey, songTokens [index]);
	}

	private void PopulateList() {
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
