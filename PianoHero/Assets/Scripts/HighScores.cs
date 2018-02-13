//C# Example
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class HighScores : EditorWindow
{
	static List<string> songsWithHighScores;

	public static void ShowWindow() {
		EditorWindow.GetWindow(typeof(HighScores));
	}

	public void SetSongs(List<string> songsToDisplay) {
		songsWithHighScores = songsToDisplay;
	}

	void OnGUI() {
		GUILayout.Label ("User High Scores", EditorStyles.boldLabel);
		foreach (string songName in songsWithHighScores) {

		}
	}
}