//C# Example
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class LoadMP3 : MonoBehaviour {
	
	string MP3Path;
	string songName;

	public void SetMP3Path(string path) {
		if (CheckValid())
			MP3Path = path;
		else
			print ("Error: invalid path");
	}

	public void SetSongName(string name) {
		songName = name;
	}

	public void LoadButton() {
		LoadFile ();
	}

	bool CheckValid() {
		return true; // TODO
	}

	public void MainMenuButton() {
		SceneManager.LoadScene (Const.MainMenuScene);
	}

	// Only called from OpenLoadWindow, load the file and call the backend TODO
	private void LoadFile() {

		print ("Loading file");

		string sourcePath = MP3Path.Remove (MP3Path.LastIndexOf('/'));
		string destToken = Utility.DisplayToToken (songName);
		string destFileName = destToken + ".mp3";

		CopyMP3File (sourcePath, MP3Path, destFileName);

		Program pipeline = new Program (Application.streamingAssetsPath + "/pipeline.sh", "");
		pipeline.LaunchCommandLineApp ();
	}

	private void CopyMP3File(string sourcePath, string MP3Path, string destFileName) {
		if (System.IO.Directory.Exists (sourcePath)) {
			string targetPath = PlayerPrefs.GetString (Const.resourcePathKey) + "MP3Files/";
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