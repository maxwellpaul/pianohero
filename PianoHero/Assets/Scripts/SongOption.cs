using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SongOption : MonoBehaviour {

    public string SongTitle;

    private void OnMouseDown()
    {
		print("Song Selected");
        Utility.songChoiceToken = SongTitle;
		SceneManager.LoadScene(Const.DifficultySelectScene);
    }
}
