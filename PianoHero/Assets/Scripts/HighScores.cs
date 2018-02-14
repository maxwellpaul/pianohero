//C# Example
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class HighScores : EditorWindow
{
	List<string> songTokens = new List<string>();

	public static void ShowWindow() {
		EditorWindow.GetWindow(typeof(HighScores));
	}

	public void SetSongs(List<string> songs) {
		songTokens = songs;
	}

	void OnGUI() {
		GUILayout.Label ("User High Scores", EditorStyles.boldLabel);
		drawTable ();
	}

	private void drawsingleline (int pos, string songToken) {
		pos += 2;
		GUI.Label(new Rect(0, pos*32, 128,32), Utility.tokenToDisplay(songToken));
		GUI.Label(new Rect(128, pos*32, 128,32), PlayerPrefs.GetInt (Utility.makeHighScoreKey (songToken, PHeroConsts.highScoreKey)).ToString());
		GUI.Label(new Rect(256, pos*32, 64,32), PlayerPrefs.GetInt (Utility.makeHighScoreKey (songToken, PHeroConsts.highStreakKey)).ToString());
		GUI.Label(new Rect(320, pos*32, 64,32), PlayerPrefs.GetInt (Utility.makeHighScoreKey (songToken, PHeroConsts.highMultKey)).ToString());
	}

	private void drawTable () {
		GUI.Label(new Rect(0, 32, 128,32), "Song Name");
		GUI.Label(new Rect(128, 32, 128,32), "High Score");
		GUI.Label(new Rect(256, 32, 64,32), "High Streak");
		GUI.Label(new Rect(320, 32, 64,32), "High Mult");

		int i = 0;
		foreach (string song in songTokens) {
			drawsingleline(i, song);
			i++;
		}
	}
}