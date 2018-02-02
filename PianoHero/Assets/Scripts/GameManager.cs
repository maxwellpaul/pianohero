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
    float noteSpeed;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("Score", 0);
		PlayerPrefs.SetInt ("RockMeter", 25);

		UpdateGUI();
        noteSpeed = note.GetComponent<Note>().speed;
        ReadString();
	}
	
	// Update is called once per frame
	void Update () {
		
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
		RockMeterUp ();

		streak += 1;
		multiplier = 1 + streak / mult_length;
	}

	private void ResetStreak() {
		RockMeterDown ();

		streak = 0;
		multiplier = 1;
	}

	private void RockMeterUp() {
		PlayerPrefs.SetInt("RockMeter", Mathf.Min(100, PlayerPrefs.GetInt("RockMeter") + 1));
	}

	private void RockMeterDown() {
		PlayerPrefs.SetInt ("RockMeter", Mathf.Max (0, PlayerPrefs.GetInt ("RockMeter") - 1));
	}

	void UpdateGUI() {
		PlayerPrefs.SetInt ("Streak", streak);
		PlayerPrefs.SetInt ("Mult", multiplier);
	}

	void OnTriggerEnter2D(Collider2D col) {
		ResetStreak ();
		Destroy (col.gameObject);
	}

	void ReadString()
    {
        float noteOneX = -1.5f;
        float noteTwoX = -.5f;
		float noteThreeX = .5f;
		float noteFourX = 1.5f;
        float startY = -3;
        string path = "Assets/Songs/MyFile.txt";

		//Read the text from directly from the test.txt file
		StreamReader reader = new StreamReader(path);
        string noteString;
        while(!reader.EndOfStream) {
            noteString = reader.ReadLine();
            string[] subStrings = noteString.Split(':');
            float yCoord = startY + (noteSpeed * float.Parse(subStrings[1]));
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
		reader.Close();
	}

	public void Win() {
		SceneManager.LoadScene (2);	
	}

	public void Lose() {
		// Load the lose screen
	}
}
