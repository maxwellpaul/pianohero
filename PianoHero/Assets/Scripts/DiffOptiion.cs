using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiffOptiion : MonoBehaviour {

    public string Difficulty;

	private void OnMouseDown()
	{
		print("Difficulty Selected");
        Utility.difficultyLevel = Difficulty;
        SceneManager.LoadScene(Const.GamePlayScene);
	}
}
