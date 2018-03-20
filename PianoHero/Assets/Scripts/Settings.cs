using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour {

	public InputField inputField;

	void Start () {
		inputField.text = PlayerPrefs.GetString(Const.resourcePathKey);
	}

	public void SetPathButton() {
		if (!CheckValidity ()) {
			print ("Error: check validity failed in Settings.cs");
			return;
		}

		PlayerPrefs.SetString (Const.resourcePathKey, inputField.text);
	}

	public void MainMenuButton() {
		SceneManager.LoadScene (Const.MainMenuScene);
	}

	private bool CheckValidity() {
		print ("Checking " + inputField.text);
		return 	System.IO.Directory.Exists (inputField.text) && 
				inputField.text.EndsWith ("PianoHeroResources/");
	}
}
