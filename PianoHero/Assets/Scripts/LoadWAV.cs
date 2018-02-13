using System.IO;
using UnityEngine;
using UnityEditor;

public class LoadWAV : EditorWindow {

	public static void ShowWindow() {
		GetWindow<LoadWAV> ("Load WAV File");
	}

	void OnGUI() {

	}

	[MenuItem("Example/Load Textures To Folder")]
	static void Apply()
	{
		string path = EditorUtility.OpenFolderPanel("Load png Textures", "", "");
		string[] files = Directory.GetFiles(path);

		foreach (string file in files)
			if (file.EndsWith(".png"))
				File.Copy(file, EditorApplication.currentScene);
	}

}