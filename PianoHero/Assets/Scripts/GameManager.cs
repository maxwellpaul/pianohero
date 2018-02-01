using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour {

	public int multiplier = 1;
	public int streak = 0;
	public int mult_length = 10;
    public GameObject note;
    float noteSpeed;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("Score", 0);
		UpdateGUI();
        noteSpeed = note.GetComponent<Note>().speed;
        ReadString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddStreak() {
		++streak;
		multiplier = 1 + streak / mult_length;
		UpdateGUI();
	}

	public void ResetStreak() {
		streak = 0;
		multiplier = 1;
		UpdateGUI();
	}

	void UpdateGUI() {
		PlayerPrefs.SetInt ("Streak", streak);
		PlayerPrefs.SetInt ("Mult", multiplier);
	}

	void OnTriggerEnter2D(Collider2D col) {
			
	}

	public int GetScore() {
		return 100 * multiplier;
	}

	void ReadString()
    {
        float noteOneX = -1.5f;
        float noteTwoX = -.5f;
		float noteThreeX = .5f;
		float noteFourX = 1.5f;
        float startY = -3;
        string path = "Assets/Songs/test.txt";

		//Read the text from directly from the test.txt file
		StreamReader reader = new StreamReader(path);
        string noteString;
        while(!reader.EndOfStream) {
            noteString = reader.ReadLine();
            string[] subStrings = noteString.Split(':');
            float yCoord = startY + (noteSpeed * float.Parse(subStrings[1]));
            print("Ycoord" + yCoord);
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
}
