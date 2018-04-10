using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Crosstales.FB;

public class Settings : MonoBehaviour {

    public GameObject text;
    private string path;

	public void choosePathButton() {

        path = FileBrowser.OpenSingleFolder("Please Choose a Resource Folder");

        if (path.Length != 0)
        {
            text.GetComponent<UnityEngine.UI.Text>().text = path;
            text.GetComponent<UnityEngine.UI.Text>().color = Color.white;
            print(path);
        }
	}

    public void setPathButton() {
        text.GetComponent<UnityEngine.UI.Text>().text = "Path successfully set!";
        PlayerPrefs.SetString(Const.resourcePathKey, path);
    }

	public void MainMenuButton() {
		SceneManager.LoadScene (Const.MainMenuScene);
	}

    //private bool CheckValidity(string path) {
	//	print ("Checking " + path);
	//	return 	System.IO.Directory.Exists (inputField.text) && 
	//			inputField.text.EndsWith ("PianoHeroResources/");
	//}
}
