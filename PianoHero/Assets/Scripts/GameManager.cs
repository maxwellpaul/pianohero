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

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("Score", 0);
		PlayerPrefs.SetInt ("MaxMult", 1);
		PlayerPrefs.SetInt ("MaxStreak", 1);

		rockMeter = GameObject.Find ("RockMeter");

		UpdateGUI();
        noteSpeed = note.GetComponent<Note>().speed;
        ReadString();
	}
	
	// Update is called once per frame
	void Update () {
		if (ready && GameObject.FindGameObjectsWithTag ("Note").Length == 0)
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
		PlayerPrefs.SetInt ("Score", PlayerPrefs.GetInt ("Score") + 100 * multiplier);
	}

	private void AddStreak() {
		streak += 1;
		multiplier = 1 + streak / mult_length;

		PlayerPrefs.SetInt (
			"MaxStreak", 
			Mathf.Max (streak, PlayerPrefs.GetInt ("MaxStreak"))
		);

		PlayerPrefs.SetInt (
			"MaxMult",
			Mathf.Max (multiplier, PlayerPrefs.GetInt ("MaxMult"))
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
		PlayerPrefs.SetInt ("Streak", streak);
		PlayerPrefs.SetInt ("Mult", multiplier);
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
		float startY = 7;
        string path = "Assets/Songs/MyFile.txt";

		//Read the text from directly from the test.txt file
		StreamReader reader = new StreamReader(path);
        string noteString;
		float yCoord = 0;
		while(!reader.EndOfStream) {
            noteString = reader.ReadLine();
            string[] subStrings = noteString.Split(':');
            yCoord = startY + (noteSpeed * float.Parse(subStrings[1]));
            print("Ycoord" + yCoord + " " + noteSpeed + " " + float.Parse(subStrings[1]));
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
		int score = Mathf.Max (PlayerPrefs.GetInt ("Score"), PlayerPrefs.GetInt("HighScore"));
		PlayerPrefs.SetInt ("HighScore", score);

		int streak = Mathf.Max (PlayerPrefs.GetInt ("MaxStreak"), PlayerPrefs.GetInt ("HighStreak"));
		PlayerPrefs.SetInt ("HighStreak", streak);

		int mult = Mathf.Max (PlayerPrefs.GetInt ("MaxMult"), PlayerPrefs.GetInt ("HighMult"));
		PlayerPrefs.SetInt ("HighMult", mult);

		SceneManager.LoadScene (2);	
	}

	public void Lose() {
		// Load the lose screen
		Win();
	}
}
