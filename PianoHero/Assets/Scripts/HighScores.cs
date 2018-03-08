//C# Example
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScores : MonoBehaviour {
	public Dropdown songDrop;
	public Dropdown diffDrop;

	int songIndex;
	int diffIndex;

	void Start() {
		songDrop.ClearOptions ();

		// Populate the dropdown with user readable names for the songs
		List<string> displaySongs = new List<string> ();
		foreach (string filename in Utility.songTokens)
			displaySongs.Add (Utility.tokenToDisplay (filename));
		songDrop.AddOptions (displaySongs);

		songIndex = 0;
		diffIndex = 0;
		RefreshHighScores ();
	}

	public void BackToMenuButton() {
		SceneManager.LoadScene (Const.MainMenuScene);
	}

	public void SongDropDownIndexChanged (int index) {
		songIndex = index;
		RefreshHighScores ();
	}

	public void DiffDropDownIndexChanged (int index) {
		diffIndex = index;
		RefreshHighScores ();
	}

	private void RefreshHighScores() {
		string diffLevel = Const.difficultyLevelsArray [diffIndex];
		string songToken = Utility.songTokens [songIndex];

		PlayerPrefs.SetInt ("Score_Scene", PlayerPrefs.GetInt(Utility.makeHighScoreKey(songToken, Const.highScoreKey, diffLevel)));
		PlayerPrefs.SetInt ("Streak_Scene", PlayerPrefs.GetInt(Utility.makeHighScoreKey(songToken, Const.highStreakKey, diffLevel)));
		PlayerPrefs.SetInt ("Mult_Scene", PlayerPrefs.GetInt(Utility.makeHighScoreKey(songToken, Const.highMultKey, diffLevel)));
	}
}