//C# Example
using UnityEditor;
using UnityEngine;
using System.IO;
using System;

public class LoadWAV : EditorWindow
{
	string WAVPath;
	string songName;
	string targetPath;

	// Add menu item named "My Window" to the Window menu
	public static void ShowWindow() {
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(LoadWAV));
	}

	void OnGUI() {
		GUILayout.Label ("Enter the full path to the WAV file", EditorStyles.boldLabel);
		WAVPath = EditorGUILayout.TextField ("Full Path", WAVPath);
		songName = EditorGUILayout.TextField ("Song Name", songName);
		if (GUILayout.Button ("Load"))
			LoadFile ();
	}

	void LoadFile() {
		// To copy all the files in one directory to another directory. 
		// Get the files in the source folder. (To recursively iterate through 
		// all subfolders under the current directory, see 
		// "How to: Iterate Through a Directory Tree.")
		// Note: Check for target path was performed previously 
		//       in this code example.

//		string[] temp = WAVPath.Split ('/');
//		string origFileName = temp [temp.Length - 1];
//		string sourcePath = WAVPath.Remove(WAVPath.LastIndexOf ('/'));
//
////		if (System.IO.Directory.Exists(sourcePath)) {
////			string[] files = System.IO.Directory.GetFiles(sourcePath);
////
////			// Copy the files and overwrite destination files if they already exist. 
////			foreach (string s in files) {
////				// Use static Path methods to extract only the file name from the path.
////				string fileName = System.IO.Path.GetFileName(s);
////				string destFile = System.IO.Path.Combine(targetPath, fileName);
////				System.IO.File.Copy(s, destFile, true);
////			}
////		} else {
////			Console.WriteLine("Source path does not exist!");
//		}
	}
}