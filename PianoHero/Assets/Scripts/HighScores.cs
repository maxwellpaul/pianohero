//C# Example
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScores : MonoBehaviour {
	
	public Dropdown songDrop;
	public Dropdown diffDrop;

	int songIndex = 0;
	int diffIndex = 0;

	/// ----------
	/// Init
	/// ----------

	void Start() {
		songDrop.ClearOptions ();

		List<string> displaySongs = new List<string> ();
		foreach (string filename in Utility.songTokens)
			displaySongs.Add (Utility.tokenToDisplay (filename));
		songDrop.AddOptions (displaySongs);

		PlayerPrefs.SetInt ("Score_Scene", 0);
		PlayerPrefs.SetInt ("Streak_Scene", 0);
		PlayerPrefs.SetInt ("Mult_Scene", 0);

		RefreshHighScores ();
	}

	/// ----------
	/// Buttons
	/// ----------

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

	/// ----------
	/// Helpers
	/// ----------

	private void RefreshHighScores() {
		string diffLevel = Const.difficultyLevelsArray [diffIndex];
		string songToken = Utility.songTokens [songIndex];

		PlayerPrefs.SetInt ("Score_Scene", PlayerPrefs.GetInt(Utility.makeHighScoreKey(songToken, Const.highScoreKey, diffLevel)));
		PlayerPrefs.SetInt ("Streak_Scene", PlayerPrefs.GetInt(Utility.makeHighScoreKey(songToken, Const.highStreakKey, diffLevel)));
		PlayerPrefs.SetInt ("Mult_Scene", PlayerPrefs.GetInt(Utility.makeHighScoreKey(songToken, Const.highMultKey, diffLevel)));
	}
}