using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour {

	public int multiplier = 1;
	public int streak = 0;
	public int mult_length = 10;
    public GameObject note;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetInt ("Score", 0);
		UpdateGUI();
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
        float currentY = 5.7f;
        float noteDiameter = 1;
        string path = "Assets/Songs/test.txt";

		//Read the text from directly from the test.txt file
		StreamReader reader = new StreamReader(path);
        int noteType;
        while(!reader.EndOfStream) {
            if(int.TryParse(reader.ReadLine(), out noteType)) {
                print("successfully read note" + noteType);
            }
            switch(noteType) {
                case 1:
                    Instantiate(note, new Vector3(noteOneX, currentY, 0), Quaternion.identity);
                    currentY += noteDiameter;
                    break;
                case 2:
					Instantiate(note, new Vector3(noteTwoX, currentY, 0), Quaternion.identity);
                    currentY += noteDiameter;
                    break;
                case 3:
					Instantiate(note, new Vector3(noteThreeX, currentY, 0), Quaternion.identity);
                    currentY += noteDiameter;
                    break;
                case 4:
					Instantiate(note, new Vector3(noteFourX, currentY, 0), Quaternion.identity);
                    currentY += noteDiameter;
                    break;
                default:
                    print("Error: Invalid Note Type");
                    break;
            }
        }
        //reader.ReadLine();
		//Debug.Log(reader.ReadToEnd());
		reader.Close();
	}
}
