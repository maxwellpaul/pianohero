//C# Example
using UnityEditor;
using UnityEngine;
using System.IO;
using System;

public class LoadMP3 : EditorWindow {
	
	string MP3Path;
	string songName;
	GameObject menu;

	public string getSong() {
		return songName;
	}

	public string getMP3Path() {
		return MP3Path;
	}

	void OnGUI() {
		menu = GameObject.Find (Const.MenuManagerObj);
		GUILayout.Label ("Enter the full path to the WAV file", EditorStyles.boldLabel);
		MP3Path = EditorGUILayout.TextField ("Full Path", MP3Path);
		songName = EditorGUILayout.TextField ("Song Name", songName);
		if (GUILayout.Button ("Load"))
			menu.GetComponent<Menu> ().LoadFile ();
	}
}