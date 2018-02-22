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
    float noteSpeed;
	bool ready = false;

	string songInfo;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt (Const.scoreKey, 0);
		PlayerPrefs.SetInt (Const.maxMultKey, 1);
		PlayerPrefs.SetInt (Const.maxStreakKey, 0);
		PlayerPrefs.SetInt (Const.amountOfRockKey, 0);

		rockMeter = GameObject.Find (Const.RockMeterObj);

		songInfo = PlayerPrefs.GetString (Const.songChoiceTokenKey) + ".txt";

		UpdateGUI();
        noteSpeed = note.GetComponent<Note>().speed;
        ReadString();
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

	void ReadString()
    {
        float noteOneX = -1.5f;
        float noteTwoX = -.5f;
		float noteThreeX = .5f;
		float noteFourX = 1.5f;
        //float startY = -3;
		float startY = 6;
		string path = Application.dataPath + Const.LocalNotePath + songInfo;

		//Read the text from directly from the test.txt file
		StreamReader reader = new StreamReader(path);
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
		string songChoiceToken = PlayerPrefs.GetString (Const.songChoiceTokenKey);
		print ("song choice token" + songChoiceToken);

		string songHighScoreKey = Utility.makeHighScoreKey (songChoiceToken, Const.highScoreKey);
		int score = Mathf.Max (PlayerPrefs.GetInt (Const.scoreKey), PlayerPrefs.GetInt(songHighScoreKey));
		PlayerPrefs.SetInt (songHighScoreKey, score);

		string songHighStreakKey = Utility.makeHighScoreKey (songChoiceToken, Const.highStreakKey);
		int streak = Mathf.Max (PlayerPrefs.GetInt (Const.maxStreakKey), PlayerPrefs.GetInt (songHighStreakKey));
		PlayerPrefs.SetInt (songHighStreakKey, streak);

		string songHighMultKey = Utility.makeHighScoreKey (songChoiceToken, Const.highMultKey);
		int mult = Mathf.Max (PlayerPrefs.GetInt (Const.maxMultKey), PlayerPrefs.GetInt (songHighMultKey));
		PlayerPrefs.SetInt (songHighMultKey, mult);

		// Set the scores for the win screen
		PlayerPrefs.SetInt (Const.highScoreKey, score);
		PlayerPrefs.SetInt (Const.highStreakKey, streak);
		PlayerPrefs.SetInt (Const.highMultKey, mult);

		SceneManager.LoadScene (Const.WinScreenScene);	
	}

	public void Lose() {
		// Load the lose screen, TODO add a lose screen
		Win();
	}

	public void MainMenu() {
		SceneManager.LoadScene (Const.MainMenuScene);	
	}

	public void PlayAgain() {
		PlayerPrefs.SetInt (Const.amountOfRockKey, 0);
		SceneManager.LoadScene (Const.GamePlayScene);	
	}
}