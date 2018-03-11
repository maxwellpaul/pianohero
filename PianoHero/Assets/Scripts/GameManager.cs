using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	int multiplier = 1;
	int streak = 0;
	int mult_length = 4;
    public GameObject note;
	public GameObject rockMeter;
    public float noteSpeed;
	bool ready = false;

	string noteFile;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt (Const.scoreKey, 0);
		PlayerPrefs.SetInt (Const.maxMultKey, 1);
		PlayerPrefs.SetInt (Const.maxStreakKey, 0);
		Utility.amountOfRock = 0;

		noteFile = Utility.songChoiceToken + "-" + Utility.difficultyLevel + ".txt";

		UpdateGUI();
        ReadString();
	}

	// TODO remove or move?
	void OnLevelWasLoaded(int level) {
		Utility.amountOfRock = 0;
		rockMeter = GameObject.Find (Const.RockMeterObj);
	}

	// Update is called once per frame
	void Update () {
		if (ready && GameObject.FindGameObjectsWithTag (Const.NoteObj).Length == 0)
			Win ();
	}

	public void HitNote() {
		AddScore ();
		AddStreak ();
		RockMeterUp ();
		UpdateGUI ();
	}

	public void MissedNote() {
		ResetStreak ();
		RockMeterDown ();
		UpdateGUI ();
	}

	private void AddScore() {
		PlayerPrefs.SetInt (Const.scoreKey, PlayerPrefs.GetInt (Const.scoreKey) + 100 * multiplier);
	}

	private void AddStreak() {
		streak += 1;
		multiplier = 1 + streak / mult_length;

		PlayerPrefs.SetInt (
			Const.maxStreakKey, 
			Mathf.Max (streak, PlayerPrefs.GetInt (Const.maxStreakKey))
		);

		PlayerPrefs.SetInt (
			Const.maxMultKey,
			Mathf.Max (multiplier, PlayerPrefs.GetInt (Const.maxMultKey))
		);
	}

	private void ResetStreak() {
		streak = 0;
		multiplier = 1;
	}

	private void RockMeterUp() {
		rockMeter.GetComponent<RockMeter> ().MeterUp ();
	}

	private void RockMeterDown() {
		rockMeter.GetComponent<RockMeter> ().MeterDown ();
	}

	void UpdateGUI() {
		PlayerPrefs.SetInt (Const.streakKey, streak);
		PlayerPrefs.SetInt (Const.multKey, multiplier);
	}

	void OnTriggerEnter2D(Collider2D col) {
		Destroy (col.gameObject);
		MissedNote ();
	}

	public void QuitButton() {
		MainMenu ();
	}

	void ReadString() {
        float noteOneX = -1.5f;
        float noteTwoX = -.5f;
		float noteThreeX = .5f;
		float noteFourX = 1.5f;
		string path = Utility.LocalNotePath + noteFile;

		//Read the text from directly from the test.txt file
		StreamReader reader = new StreamReader(path);
        noteSpeed = 1 / float.Parse(reader.ReadLine());
        float startY = -3 + noteSpeed * 2.2f;
        string noteString;
		float yCoord = 0;
		while(!reader.EndOfStream) {
            noteString = reader.ReadLine();
            string[] subStrings = noteString.Split(':');
            yCoord = startY + (noteSpeed * float.Parse(subStrings[1]));
            switch(subStrings[0]) {
                case "1":
                    Instantiate(note, new Vector3(noteOneX, yCoord, 0), Quaternion.identity);
                    break;
                case "2":
					Instantiate(note, new Vector3(noteTwoX, yCoord, 0), Quaternion.identity);
                    break;
                case "3":
					Instantiate(note, new Vector3(noteThreeX, yCoord, 0), Quaternion.identity);
                    break;
                case "4":
					Instantiate(note, new Vector3(noteFourX, yCoord, 0), Quaternion.identity);
                    break;
                default:
                    print("Error: Invalid Note Type");
                    break;
            }
        }

		ready = true;
		reader.Close();
	}

	public void Win() {
		string songHighScoreKey = Utility.makeHighScoreKey (Utility.songChoiceToken, Const.highScoreKey, Utility.difficultyLevel);
		int score = Mathf.Max (PlayerPrefs.GetInt (Const.scoreKey), PlayerPrefs.GetInt(songHighScoreKey));
		PlayerPrefs.SetInt (songHighScoreKey, score);

		string songHighStreakKey = Utility.makeHighScoreKey (Utility.songChoiceToken, Const.highStreakKey, Utility.difficultyLevel);
		int streak = Mathf.Max (PlayerPrefs.GetInt (Const.maxStreakKey), PlayerPrefs.GetInt (songHighStreakKey));
		PlayerPrefs.SetInt (songHighStreakKey, streak);

		string songHighMultKey = Utility.makeHighScoreKey (Utility.songChoiceToken, Const.highMultKey, Utility.difficultyLevel);
		int mult = Mathf.Max (PlayerPrefs.GetInt (Const.maxMultKey), PlayerPrefs.GetInt (songHighMultKey));
		PlayerPrefs.SetInt (songHighMultKey, mult);

		// Set the scores for the win screen
		PlayerPrefs.SetInt (Const.highScoreKey, score);
		PlayerPrefs.SetInt (Const.highStreakKey, streak);
		PlayerPrefs.SetInt (Const.highMultKey, mult);

		SceneManager.LoadScene (Const.WinScreenScene);	
	}

	public void Lose() {
		Win();
	}

	public void MainMenu() {
		SceneManager.LoadScene (Const.MainMenuScene);	
	}

	public void PlayAgain() {
		SceneManager.LoadScene (Const.GamePlayScene);	
	}
}