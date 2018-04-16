//C# Example
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using System.Collections;
using Crosstales.FB;

public class LoadMP3 : MonoBehaviour
{

	string mp3Path = "";
	string songName = "";
	public GameObject waitMenu;
	public GameObject errorText;

	private void Awake()
	{
		waitMenu.SetActive(false);
		errorText.SetActive(false);
	}

	/// ----------
	/// Buttons
	/// ----------
	public void SetMP3Path(string path)
	{
		if (CheckValidPath(path))
		{
			mp3Path = System.IO.Path.Combine(path, "");
		}
	}

	public void SetSongName(string name)
	{
		songName = name;
	}

	public void LoadButton()
	{
		if (songName.Length == 0)
		{
			errorText.GetComponent<UnityEngine.UI.Text>().text = "Error: Must enter a song name";
			errorText.SetActive(true);
		}
		else if (songName.Contains("-") || songName.Contains("_") || songName.Contains("/"))
		{
			errorText.GetComponent<UnityEngine.UI.Text>().text = "Error: Song name cannot contain '-', '_', or '/'";
			errorText.SetActive(true);
		}
		//else if (mp3Path.Contains("%20"))
		//{
		//  errorText.GetComponent<UnityEngine.UI.Text>().text = "Error: MP3 file path cannot contain \"%20\"";
		//  errorText.SetActive(true);
		//}
		else if (mp3Path.Length == 0)
		{
			errorText.GetComponent<UnityEngine.UI.Text>().text = "Error: Must choose an mp3 file";
			errorText.SetActive(true);
		}
		else
		{
			errorText.SetActive(false);
			waitMenu.SetActive(true);
			StartCoroutine(waitForLoad());
			//LoadFile ();
		}
	}

	private IEnumerator waitForLoad()
	{
		yield return new WaitForSeconds(1);
		LoadFile();
	}

	public void MainMenuButton()
	{
		SceneManager.LoadScene(Const.MainMenuScene);
	}

	/// ----------
	/// Validation Funcs
	/// ----------

	private bool CheckValidPath(string path)
	{
		return System.IO.File.Exists(path) && path.EndsWith(".mp3");
	}

	private bool CheckValidSongName(string name)
	{

		return !(name.Contains("-") || name.Contains("_") || name.Contains("/"));
	}

	/// ----------
	/// Helpers
	/// ----------

	private bool DoneLoading()
	{
		return true;

		string token = Utility.DisplayToToken(songName);
		string resourcePath = PlayerPrefs.GetString(Const.resourcePathKey);

		return System.IO.File.Exists(resourcePath + "WAVFiles/" + token + ".wav") &&
			System.IO.File.Exists(resourcePath + "NoteFiles/" + token + "-Easy.txt") &&
			System.IO.File.Exists(resourcePath + "NoteFiles/" + token + "-Medium.txt") &&
			System.IO.File.Exists(resourcePath + "NoteFiles/" + token + "-Hard.txt") &&
			System.IO.File.Exists(resourcePath + "NoteFiles/" + token + "-Expert.txt");
	}

	public void ChooseFile()
	{
		string path = FileBrowser.OpenSingleFile("Please Choose an MP3 File", "", "mp3");

		if (path.Length != 0)
		{
			path = path.Replace("%20", " ");
			GameObject text = GameObject.Find("FilePathText");
			text.GetComponent<UnityEngine.UI.Text>().text = path;
			text.GetComponent<UnityEngine.UI.Text>().color = Color.white;
			print(path);
			SetMP3Path(path);
		}
	}

	private void LoadFile()
	{
		print("Loading file...");
		CopyMP3File(Utility.DisplayToToken(songName) + ".mp3");
		print("Starting pipeline script");
		Program pipeline = new Program(PlayerPrefs.GetString(Const.resourcePathKey) + "run_feature_extract.sh", "/Applications/MATLAB/MATLAB_Runtime/v93");
		pipeline.LaunchCommandLineApp();
		print("Completed pipeline script");
		MoveMP3File(Utility.DisplayToToken(songName) + ".mp3");
		while (!DoneLoading()) ;
		waitMenu.SetActive(false);
		return;
	}

	private void MoveMP3File(string destFileName)
	{
		string sourcePath = PlayerPrefs.GetString(Const.resourcePathKey) + "SongQueue/";
		string targetPath = PlayerPrefs.GetString(Const.resourcePathKey) + "MP3Files/";
		string destFilePath = System.IO.Path.Combine(targetPath, destFileName);
		string currMP3Path = System.IO.Path.Combine(sourcePath, destFileName);
		System.IO.File.Move(currMP3Path, destFilePath);
		print("Move of " + mp3Path + " success!");
		return;
	}


	private void CopyMP3File(string destFileName)
	{
		string sourcePath = mp3Path.Remove(mp3Path.LastIndexOf('/'));

		if (System.IO.Directory.Exists(sourcePath))
		{
			string targetPath = PlayerPrefs.GetString(Const.resourcePathKey) + "SongQueue/";
			string destFilePath = System.IO.Path.Combine(targetPath, destFileName);
			foreach (string s in System.IO.Directory.GetFiles(sourcePath))
			{
				if (s.Equals(mp3Path))
				{
					System.IO.File.Copy(mp3Path, destFilePath, true);
					print("Copy of " + mp3Path + " success!");
					return;
				}
			}
			print("Copy of " + mp3Path + " Failed");
		}
		else
		{
			print("Source path does not exist: " + mp3Path);
			return;
		}
	}
}