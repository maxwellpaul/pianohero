﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SongSelectManager : MonoBehaviour {

    public GameObject SongOption;
//	public Button nextButton;
//	public Button prevButton;

	List<GameObject> currSongs;

	int pageNum;
	int maxPageNum;
	int pageSize;
    public GameObject pageText;

	// Use this for initialization
	void Start () {
		Utility.LocalNotePath = PlayerPrefs.GetString(Const.resourcePathKey) + "NoteFiles/";
        pageSize = 3;
		pageNum = 1;
		currSongs = new List<GameObject> ();
        print(Utility.songTokens.Count);
        maxPageNum = NumPages();
        print(maxPageNum);
		SetButtons ();
		PopulateSongs(pageNum-1);
        pageText.GetComponentInChildren<UnityEngine.UI.Text>().text = "Page " + pageNum.ToString() + "/" + maxPageNum.ToString();
	}

    private int NumPages() {
        int currentCounter = 0;
        int numPages = 0;
        while (currentCounter < Utility.songTokens.Count) {
            currentCounter += 3;
            numPages += 1;
        }
        return numPages;
    }

	void Update() {
        if (Input.GetKeyDown (KeyCode.RightArrow)) {
			NextPageButton ();
        } else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			PrevPageButton ();
		}
	}

	public void MainMenuButtonPress()
	{
        print("here");
		SceneManager.LoadScene(Const.MainMenuScene);
	}

	public void NextPageButton() {
		if (pageNum == maxPageNum)
			return;

		++pageNum;
		SetButtons ();
		ClearPage ();
		PopulateSongs (pageNum-1);
        pageText.GetComponentInChildren<UnityEngine.UI.Text>().text = "Page " + pageNum.ToString() + "/" + maxPageNum.ToString();
	}

	public void PrevPageButton() {
		if (pageNum == 1)
			return;

		--pageNum;
		SetButtons ();
		ClearPage ();
		PopulateSongs (pageNum-1);
        pageText.GetComponentInChildren<UnityEngine.UI.Text>().text = "Page " + pageNum.ToString() + "/" + maxPageNum.ToString();
	}

	public void SetButtons() {
//		if (pageNum > 0) {
//			prevButton.gameObject.SetActive (true);
//		} else {
//			prevButton.gameObject.SetActive (false);
//		}
//
//		if (pageNum < maxPageNum) {
//			nextButton.gameObject.SetActive (true);
//		} else {
//			nextButton.gameObject.SetActive (false);
//		}
	}

	void ClearPage() {
		foreach (GameObject obj in currSongs)
			Destroy(obj);
		currSongs.Clear ();
	}


	void PopulateSongs(int page) {

		print ("Populating scene at page " + page);

        //Create Options For All Songs
        float SongOptionX = 0.0f;
        float SongOptionY = -2.2f;

		int endIndex = Mathf.Min ((page + 1) * pageSize, Utility.songTokens.Count);

		for (int i = page * pageSize; i < endIndex; ++i) {
			string filename = Utility.songTokens [i];

            GameObject song = Instantiate(SongOption, new Vector3(SongOptionX, SongOptionY, 0), Quaternion.identity);
			song.GetComponentInChildren<UnityEngine.UI.Text>().text = Utility.tokenToDisplay(filename);
            song.GetComponentInChildren<SongOption>().SongTitle = filename;
            SongOptionY -= 2.5f;
			currSongs.Add (song);
        }
    }
}
