﻿//C# Example
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Collections;

public class LoadMP3 : MonoBehaviour {
	
	string mp3Path;
	string songName;
    public GameObject waitMenu;

    private void Awake()
    {
        waitMenu.SetActive(false);
    }

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
        waitMenu.SetActive(true);
        StartCoroutine(waitForLoad());
		//LoadFile ();
	}

    private IEnumerator waitForLoad() {
        yield return new WaitForSeconds(1);
        LoadFile();
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

    public void ChooseFile() {
		string path = EditorUtility.OpenFilePanel("Please Choose an MP3 File", "", "mp3");
		if (path.Length != 0)
		{
            GameObject text = GameObject.Find("FilePathText");
            text.GetComponent<UnityEngine.UI.Text>().text = path;
            text.GetComponent<UnityEngine.UI.Text>().color = Color.white;
			print(path);
			SetMP3Path(path);
		}
    }

	private void LoadFile() {
        print("Loading file...");

		CopyMP3File (Utility.DisplayToToken (songName) + ".mp3");

		print ("Starting pipeline script");
		Program pipeline = new Program (Application.streamingAssetsPath + "/run_feature_extract.sh", "/Applications/MATLAB/MATLAB_Runtime/v93");
		pipeline.LaunchCommandLineApp ();
		print ("Completed pipeline script");
        MoveMP3File(Utility.DisplayToToken(songName) + ".mp3");
		while (!DoneLoading());
        waitMenu.SetActive(false);
		return;
	}

    private void MoveMP3File(string destFileName) {
        string sourcePath = PlayerPrefs.GetString(Const.resourcePathKey) + "SongQueue/";
		string targetPath = PlayerPrefs.GetString(Const.resourcePathKey) + "MP3Files/";
		string destFilePath = System.IO.Path.Combine(targetPath, destFileName);
        string currMP3Path = System.IO.Path.Combine(sourcePath, destFileName);
        System.IO.File.Move(currMP3Path, destFilePath);
		print("Move of " + mp3Path + " success!");
		return;
    }


    private void CopyMP3File(string destFileName) {
		string sourcePath = mp3Path.Remove (mp3Path.LastIndexOf('/'));

		if (System.IO.Directory.Exists (sourcePath)) {
			string targetPath = PlayerPrefs.GetString (Const.resourcePathKey) + "SongQueue/";
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