using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SongSelectManager : MonoBehaviour {

    public GameObject SongOption;

	// Use this for initialization
	void Start () {
		Utility.LocalNotePath = PlayerPrefs.GetString(Const.resourcePathKey) + "NoteFiles/";
        PopulateSongs();
	}
	
    void PopulateSongs() {
		Utility.songTokens.Clear();

		// Get the text songs in the given directory
		if (!System.IO.Directory.Exists(Utility.LocalNotePath))
		{
			print("Error: path dne - " + Utility.LocalNotePath);
			return;
		}

		foreach (string file in System.IO.Directory.GetFiles(Utility.LocalNotePath))
		{
			string[] filenameArr = file.Split('/');
			string filename = filenameArr[filenameArr.Length - 1];
			if (filename.EndsWith(".txt"))
			{
				filenameArr = filename.Split('-');
				filename = filenameArr[0];
				if (!Utility.songTokens.Contains(filename))
					Utility.songTokens.Add(Utility.textToToken(filename));
			}
		}

        //Create Options For All Songs
        float SongOptionX = 0.0f;
        float SongOptionY = -2.2f;
        foreach (string filename in Utility.songTokens) {
            GameObject song = Instantiate(SongOption, new Vector3(SongOptionX, SongOptionY, 0), Quaternion.identity);
            song.GetComponentInChildren<UnityEngine.UI.Text>().text = filename;
            song.GetComponentInChildren<SongOption>().SongTitle = filename;
            SongOptionY -= 2.5f;
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
