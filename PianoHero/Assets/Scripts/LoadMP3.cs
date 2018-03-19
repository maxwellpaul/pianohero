//C# Example
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class LoadMP3 : MonoBehaviour {
	
	string mp3Path;
	string songName;

	/// ----------
	/// Buttons
	/// ----------

	public void SetMP3Path(string path) {
		if (CheckValidPath(path))
			mp3Path = path;
		else
			print ("Error: invalid path " + path);
	}

	public void SetSongName(string name) {
		if (CheckValidSongName (name))
			songName = name;
		else
			print ("Error: invalid song name");
	}

	public void LoadButton() {
		LoadFile ();
	}

	public void MainMenuButton() {
		SceneManager.LoadScene (Const.MainMenuScene);
	}

	/// ----------
	/// Validation Funcs
	/// ----------

	private bool CheckValidPath(string path) {
		return System.IO.File.Exists (path) && path.EndsWith(".mp3");
	}

	private bool CheckValidSongName(string name) {
		return !(name.Contains ("-") || name.Contains ("_") || name.Contains ("/"));
	}

	/// ----------
	/// Helpers
	/// ----------

	private bool DoneLoading () {
		return true;

		string token = Utility.DisplayToToken (songName);
		string resourcePath = PlayerPrefs.GetString (Const.resourcePathKey);

		return System.IO.File.Exists (resourcePath + "WAVFiles/" + token + ".wav") && 
			System.IO.File.Exists(resourcePath + "NoteFiles/" + token + "-Easy.txt") &&
			System.IO.File.Exists(resourcePath + "NoteFiles/" + token + "-Medium.txt") &&
			System.IO.File.Exists(resourcePath + "NoteFiles/" + token + "-Hard.txt") &&
			System.IO.File.Exists(resourcePath + "NoteFiles/" + token + "-Expert.txt");
	}

	private void LoadFile() {
		print ("Loading file...");

		CopyMP3File (Utility.DisplayToToken (songName) + ".mp3");

		Program pipeline = new Program (Application.streamingAssetsPath + "/pipeline.sh", PlayerPrefs.GetString (Const.resourcePathKey));
		pipeline.LaunchCommandLineApp ();

		while (!DoneLoading());

		return;
	}

	private void CopyMP3File(string destFileName) {
		string sourcePath = mp3Path.Remove (mp3Path.LastIndexOf('/'));

		if (System.IO.Directory.Exists (sourcePath)) {
			string targetPath = PlayerPrefs.GetString (Const.resourcePathKey) + "MP3Files/";
			string destFilePath = System.IO.Path.Combine (targetPath, destFileName);
			foreach (string s in System.IO.Directory.GetFiles(sourcePath)) {
				if (s.Equals (mp3Path)) {
					System.IO.File.Copy (mp3Path, destFilePath, true);
					print ("Copy of " + mp3Path + " success!");
					return;
				}
			}
			print ("Copy of " + mp3Path + " Failed");
		} else {
			print ("Source path does not exist: " + mp3Path);
			return;
		}
	}
}