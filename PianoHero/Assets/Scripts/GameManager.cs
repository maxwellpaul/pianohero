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
    public float runningNoteSpeed;
    public float noteSpeed;
    public GameObject waitMenu;
    public GameObject waitMenuText;
    public GameObject oneText;
    public GameObject twoText;
    public GameObject threeText;
    public GameObject fourText;
    public GameObject fiveText;
    public GameObject pauseMenu;
    public GameObject pauseButton;

	public bool ready = false;

	/// ----------
	/// Init and Base funcs
	/// ----------

    void Start () {
		PlayerPrefs.SetInt (Const.scoreKey, 0);
		PlayerPrefs.SetInt (Const.maxMultKey, 1);
		PlayerPrefs.SetInt (Const.maxStreakKey, 0);
		Utility.amountOfRock = 0;
        waitMenu.SetActive(true);
        oneText.SetActive(false);
        twoText.SetActive(false);
        threeText.SetActive(false);
        fourText.SetActive(false);
        fiveText.SetActive(false);
        pauseMenu.SetActive(false);
        pauseButton.SetActive(false);
        ReadString();
		UpdateGUI();
	}

	IEnumerator Countdown()
	{
        pauseMenu.SetActive(false);
        waitMenu.SetActive(true);
        fiveText.SetActive(true);
        yield return new WaitForSeconds(1);
        fiveText.SetActive(false);
        fourText.SetActive(true);
        yield return new WaitForSeconds(1);
        fourText.SetActive(false);
        threeText.SetActive(true);
		yield return new WaitForSeconds(1);
        threeText.SetActive(false);
        twoText.SetActive(true);
        yield return new WaitForSeconds(1);
        twoText.SetActive(false);
        oneText.SetActive(true);
        yield return new WaitForSeconds(1);
        oneText.SetActive(false);
        waitMenu.SetActive(false);
        pauseButton.SetActive(true);
		noteSpeed = runningNoteSpeed;
	}

	void OnLevelWasLoaded(int level) {
		Utility.amountOfRock = 0;
		rockMeter = GameObject.Find (Const.RockMeterObj);
	}

	void Update () {
        if(waitMenu.activeInHierarchy && Input.GetKeyDown(KeyCode.F)) {
            waitMenu.SetActive(false);
            waitMenuText.SetActive(false);
            noteSpeed = runningNoteSpeed;
            ready = true;
            StartCoroutine(wait());
            //ReadString();
        }
        if (ready && GameObject.FindGameObjectsWithTag (Const.NoteObj).Length == 0) {
            Win();
        }
	}

    IEnumerator wait() {
        yield return new WaitForSeconds(2.2f);
        pauseButton.SetActive(true);
    }

	void OnTriggerEnter2D(Collider2D col) {
		Destroy (col.gameObject);
		MissedNote ();
	}

	/// ----------
	/// Buttons
	/// ----------

	public void QuitButton() {
		MainMenu ();
	}

    public void PauseButton() {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        noteSpeed = 0f;
    }

    public void ResumeButton() {
        StartCoroutine(Countdown());
    }

    public void RestartButton() {
        SceneManager.LoadScene(Const.GamePlayScene);
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

	/// ----------
	/// Helpers
	/// ----------

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

	private void UpdateGUI() {
		PlayerPrefs.SetInt (Const.streakKey, streak);
		PlayerPrefs.SetInt (Const.multKey, multiplier);
	}

	private void ReadString() {
        float noteOneX = -1.5f;
        float noteTwoX = -.5f;
		float noteThreeX = .5f;
		float noteFourX = 1.5f;
		string path = Utility.LocalNotePath + Utility.songChoiceToken + "-" + Utility.difficultyLevel + ".txt";

		//Read the text from directly from the test.txt file
		StreamReader reader = new StreamReader(path);
        noteSpeed = 1 / float.Parse(reader.ReadLine());
        runningNoteSpeed = noteSpeed;
        noteSpeed = 0;
        float startY = -3 + runningNoteSpeed * 2.2f;
        string noteString;
		float yCoord = 0;
		while(!reader.EndOfStream) {
            noteString = reader.ReadLine();
            string[] subStrings = noteString.Split(':');
            yCoord = startY + (runningNoteSpeed * float.Parse(subStrings[1]));
            switch(subStrings[0]) {
                case "1":
                    GameObject newNote = Instantiate(note, new Vector3(noteOneX, yCoord, 0), Quaternion.identity);
                    newNote.GetComponent<SpriteRenderer>().color = new Color32(255,0,0,255);
                    break;
                case "2":
					newNote = Instantiate(note, new Vector3(noteTwoX, yCoord, 0), Quaternion.identity);
                    newNote.GetComponent<SpriteRenderer>().color = new Color32(19, 0, 255, 255);
                    break;
                case "3":
					newNote = Instantiate(note, new Vector3(noteThreeX, yCoord, 0), Quaternion.identity);
                    newNote.GetComponent<SpriteRenderer>().color = new Color32(83, 255, 0, 255);
                    break;
                case "4":
					newNote = Instantiate(note, new Vector3(noteFourX, yCoord, 0), Quaternion.identity);
                    newNote.GetComponent<SpriteRenderer>().color = new Color32(241, 0, 255, 255);
                    break;
                default:
                    print("Error: Invalid Note Type");
                    break;
            }
        }



		
        print("finished reading file");
		reader.Close();
	}

	private void Win() {
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
}