using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Crosstales.FB;

public class Settings : MonoBehaviour {

    public GameObject text;
    private string path;
    public GameObject errorText;
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
        string folderName = "";
        if(path.Length > 19) {
            folderName = path.Substring(path.Length-19, 18);
            print(folderName);
            if (folderName != "PianoHeroResources") {
                text.GetComponent<UnityEngine.UI.Text>().text = "Error: Folder must be named \"PianoHeroResources\"";
                text.GetComponent<UnityEngine.UI.Text>().color = Color.red;
            }
            else {
				text.GetComponent<UnityEngine.UI.Text>().text = "Success: Resource folder successfully set";
                text.GetComponent<UnityEngine.UI.Text>().color = Color.white;
				PlayerPrefs.SetString(Const.resourcePathKey, path);
            }
        }
        else {
			text.GetComponent<UnityEngine.UI.Text>().text = "Error: Folder must be named \"PianoHeroResources\"";
			text.GetComponent<UnityEngine.UI.Text>().color = Color.red;
        }
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
