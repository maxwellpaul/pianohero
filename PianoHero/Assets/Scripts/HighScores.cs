//C# Example
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighScores : MonoBehaviour {
	public Dropdown drop;

	void Start() {
		drop.ClearOptions ();

		// Populate the dropdown with user readable names for the songs
		List<string> displaySongs = new List<string> ();
		foreach (string filename in Utility.songTokens)
			displaySongs.Add (Utility.tokenToDisplay (filename));
		drop.AddOptions (displaySongs);

		SetHighScores (0);
	}

	public void BackToMenuButton() {
		SceneManager.LoadScene (Const.MainMenuScene);
	}

	public void DropDownIndexChanged (int index) {
		print ("Change index " + index);
		SetHighScores (index);
	}

	private void SetHighScores(int index) {
		PlayerPrefs.SetInt ("Score_Scene", PlayerPrefs.GetInt(Utility.makeHighScoreKey(Utility.songTokens[index], Const.highScoreKey)));
		PlayerPrefs.SetInt ("Streak_Scene", PlayerPrefs.GetInt(Utility.makeHighScoreKey(Utility.songTokens[index], Const.highStreakKey)));
		PlayerPrefs.SetInt ("Mult_Scene", PlayerPrefs.GetInt(Utility.makeHighScoreKey(Utility.songTokens[index], Const.highMultKey)));
	}
}