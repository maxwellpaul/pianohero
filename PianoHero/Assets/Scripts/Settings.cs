using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour {

	public InputField inputField;

	// Use this for initialization
	void Start () {
		inputField.text = PlayerPrefs.GetString("ResourcePath");
	}

	public void SetPathButton() {
		string path = inputField.text;
		PlayerPrefs.SetString ("ResourcePath", path);
		Const.ResourcePath = path;
	}

	public void MainMenuButton() {
		SceneManager.LoadScene (Const.MainMenuScene);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
