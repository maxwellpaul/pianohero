//C# Example
using UnityEditor;
using UnityEngine;
using System.IO;
using System;

public class LoadWAV : EditorWindow
{
	string WAVPath;
	string songName;
	// Add menu item named "My Window" to the Window menu
	public static void ShowWindow() {
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(LoadWAV));
	}

	void OnGUI() {
		GUILayout.Label ("Enter the full path to the WAV file", EditorStyles.boldLabel);
		WAVPath = EditorGUILayout.TextField ("Full Path", WAVPath);
		songName = EditorGUILayout.TextField ("Song Name", songName);
		if (GUILayout.Button ("Load")) {
			PlayerPrefs.SetString (PHeroConsts.loadPath, WAVPath);
			PlayerPrefs.SetString (PHeroConsts.songName, songName);
			this.Close ();
		}
	}
}