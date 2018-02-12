using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Text.RegularExpressions;

public class Menu : MonoBehaviour {

	public Dropdown dropdown;
	Regex r = new Regex (@"(\.wav)", RegexOptions.IgnoreCase);

	List<string> WAVFiles = new List<string> () { "Paul", "Maxwell" };
	public string selectedWAV;

	// Use this for initialization
	void Start () {
		PopulateList ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DropDownIndexChanged (int index) {
		selectedWAV = WAVFiles [index];
		print ("here");
		print (selectedWAV);
	}

	public void PopulateList() {
		WAVFiles.Clear ();
		dropdown.ClearOptions ();

		string currPath = Application.dataPath + "/Songs/";
		foreach (string file in System.IO.Directory.GetFiles(currPath)) {
			string[] filenameArr = file.Split('/');
			string filename = filenameArr [filenameArr.Length - 1];
			if (r.IsMatch (filename)) {
				WAVFiles.Add (filename);
			}
		}

		List<string> songs = new List<string> () {};
		foreach (string filename in WAVFiles) {
			songs.Add (splitFilename (filename));
		}
		dropdown.AddOptions (songs);
	}

	string splitFilename(string filename) {
		return filename;
	}
}
